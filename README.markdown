[![No Maintenance Intended](http://unmaintained.tech/badge.svg)](http://unmaintained.tech/)

This is a .Net Core fork of the Podio.NET client for accessing the Podio API. Most of this documentation is copied from there.
https://github.com/podio/podio-dotnet

Installation
-------

This is different than podio-dotnet
The client library requires .NET Core 2.0 (or compatible) and [Json.NET](http://www.nuget.org/packages/Newtonsoft.Json/) as its dependency.


Constructing API client instance
-------------

This is different than podio-dotnet
Before you can make any API calls you must initialize the `Podio` class with an IAccessTokenProvider
There are default implementations for username & password and token. You can implement your own to handle other scenarios.

using PodioCore.Auth;
var usernamePasswordProvider = new PasswordAuthTokenProvider("ClientId", "ClientSecret", "Username", "Password");
var podio = new PodioCore.Podio(usernamePasswordProvider);

You then use this initialized podio object for all further operations.

Authentication
--------------

The client supports any implementation of IAccessTokenProvider.

### Web Server Flow

This scenario is no longer supported, but a new implementation of IAccessTokenProvider could easily recreate it. PodioCore was adapted for an integrated OAuth2 managment technology so this responsibility is separated by default.


### Username and Password Flow

If you're writing a batch job or are just playing around with the API, this is the easiest to get started. Do not use this for authenticating users other than yourself, the web server flow is meant for that.

```csharp
using PodioCore.Auth;
var usernamePasswordProvider = new PasswordAuthTokenProvider("ClientId", "ClientSecret", "Username", "Password");
var podio = new PodioCore.Podio(usernamePasswordProvider);

```

Basic Usage
-----------

After constructing `Podio` object  you can use it to make API requests. A big change from the podio-dotnet structure is that the services were broken into their own projects and extension methods were added to expose methods directly from the podio object without creating a service. This transition is in progress though, so remember that the underlying services have the same core functionality as in podio-dotnet by design. If you are using nuget these separate projects are offered as separate nuget packages

If you 

The same underlying functions as designed in  are organized into services, each service corresponds to an Area in official [API documentation](https://developers.podio.com/doc). 

You can access the services right from podio class. For example:

```csharp

// Getting an item
var item = podio.ItemService.GetItem(123456);

//Filtering items with a date field with external_id 'deadline' and limit the results by 10
  var filters = new Dictionary<string,object>
  {
    {"deadline",new  { from = new DateTime(2013, 10, 1), to = DateTime.Now }}
  };

await podio.ItemService.FilterItems(AppId, 10, null, filters);
```

All the wrapped methods either return a strongly typed model, or a collection of models.

Error Handling
--------------

All unsuccessful responses returned by the API (everything that has a 4xx or 5xx HTTP status code) will throw exceptions. All exceptions inherit from `PodioCore.Exceptions.PodioException` and and it has an `Error` property that represents the strongly typed version of response from the API:

```csharp
try
{
    var uploadedFile = await podio.FileService.UploadFile(filePath,"image.jpg")
}
catch (PodioException exception)
{
    Response.Write(exception.Status); // Status code of the response
    Response.Write(exception.Error.Error); // Error
    Response.Write(exception.Error.ErrorDescription); // Error description -> You need this in most cases
    Response.Write(exception.Error.ErrorDetail); // Error detail
}
```

Full Example
------------
Adding a new item with a file on an application with id 5678.

```csharp
using PodioCore;
using PodioCore.Models;
using PodioCore.Utils.ItemFields;
using PodioCore.Exceptions;

//get an IAccessTokenProvider somewhere
IAccessTokenProvider accessTokenProvider;
var podio = new Podio(accessTokenProvider);

Item myNewItem = new Item();

//A Text field with external_id 'title'
var textfield = myNewItem.Field<TextItemField>("title");
textfield.Value = "This is a text field";

//A Date field with external_id 'deadline-date'
var dateField = myNewItem.Field<DateItemField>("deadline-date");
dateField.Start = DateTime.Now;
dateField.End = DateTime.Now.AddMonths(2);

//A Location field with external_id 'location'
var locationField = myNewItem.Field<LocationItemField>("location");
locationField.Locations = new List<string> 
{ 
 "Copenhagen, Denmark"
};

//A Money field with external_id 'money'
var moneyField = myNewItem.Field<MoneyItemField>("money");
moneyField.Currency = "EUR";
moneyField.Value = 250;

//An App reference field with external_id 'app-reference'
var appReferenceField = myNewItem.Field<AppItemField>("app-reference");
//Item id's to reference
appReferenceField.ItemIds = new List<int>
{
    1234, 4568
};

// Embed/Link field with with external_id 'link'
var embedField = myNewItem.Field<EmbedItemField>("link");
var embed = await podio.EmbedService.AddAnEmbed("https://www.google.com/"); // Creating an embed
embedField.AddEmbed(embed.EmbedId);

//Uploading a file and attaching it to new item
var filePath = Server.MapPath("\\files\\report.pdf");
var uploadedFile = await podio.FileService.UploadFile(filePath, "report.pdf");
myNewItem.FileIds = new List<int> { uploadedFile.FileId }; //Attach the uploaded file's id to item

var itemId = await podio.ItemService.AddNewItem(5678, myNewItem);
```

Contribution guideline
-----------------

Your contribution to PodioCore client would be very welcome.
