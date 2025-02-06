using KevinApp.Models;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using OpenAI.Chat;
using System.Text.Json.Serialization;

namespace KevinApp.Services
{
    public interface IOpenAiService
    {
        object AnalyzeText(string text);
    }
    public class OpenAiService : IOpenAiService
    {
        private readonly string _openAiApiKey;
        public OpenAiService(IConfiguration configuration) 
        {
            _openAiApiKey = configuration["OpenAiApiKey"]!;
        }

        public object AnalyzeText(string text)
        {
            ChatClient client = new(model: "gpt-4o", _openAiApiKey);

            var promptModel = new GoogleCalendarModel
            {
                Summary = "[event title]",
                Location = "[event location]",
                Description = "[event description]",
                Start = new EventDateTime
                {
                    DateTime = "[ISO 8601 start time]",
                    TimeZone = "Asia/Jerusalem"
                },
                End = new EventDateTime
                {
                    DateTime = "[ISO 8601 end time]",
                    TimeZone = "Asia/Jerusalem"
                }
            };

            string promptJson = JsonConvert.SerializeObject(promptModel);

            string promptToAi = $"Extract the event details from the following text and return a JSON object in this format: {promptJson}\n\n and return only the json with the filled data" +
                                $"If the event date is referred to using relative terms such as 'next Wednesday' or 'this Thursday', interpret them correctly to the nearest upcoming date." +
                                $"if there is no DATE and words like tommorow or after tommorow then calculate the current date and add to it the days please" +
                                $"Also if the text is in Hebrew then use hebrew calendar days not english , you have to check if its hebrew or english to know in which" +
                                $"Language to fill the Json fields." +
                                $"If no date is provided, assume that the event is on the closest future date mentioned, based on the context of the text.\n\n" +
                                $"Also relate to Asia/Jerusalem timezone please, GMT+2 Israel time." +
                                $"Text: \"{text}\"";

            var completion = client.CompleteChat(promptToAi);

            var value = completion.Value.Content[0].Text;

            return value;
        }
    }
}
