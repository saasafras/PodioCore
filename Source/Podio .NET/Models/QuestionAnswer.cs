using Newtonsoft.Json;

namespace PodioCore.Models
{
    public class QuestionAnswer
    {
        [JsonProperty("question_option_id")]
        public int QuestionOptionId { get; set; }

        [JsonProperty("user")]
        public Contact User { get; set; }
    }
}