
using Azure;
using Azure.AI.OpenAI;
using openaiservices.Models;

namespace openaiservices.Services
{
    public class ChatService
    {

        private readonly IConfiguration _configuration;

        private readonly string systemMessage = "You are an AI assistant";

        public ChatService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Message> GetResponse(List<Message> messagechain)
        {
            OpenAIClient client = new(
                new Uri(_configuration.GetSection("AzureOpenAI")["OpenAIUrl"]!),
                new AzureKeyCredential(_configuration.GetSection("AzureOpenAI")["OpenAIKey"]!));


            ChatCompletionsOptions options = new()
            {
                Temperature = (float)0.7,
                MaxTokens = 800,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0
            };

            options.Messages.Add(new ChatMessage(ChatRole.System, systemMessage));
            foreach (var msg in messagechain)
            {
                if (msg.IsRequest)
                {
                    options.Messages.Add(new ChatMessage(ChatRole.User, msg.Body));
                }
                else
                {
                    options.Messages.Add(new ChatMessage(ChatRole.Assistant, msg.Body));
                }
            }
            string apikey = "starzopenai";
            Response<ChatCompletions> resp = await client.GetChatCompletionsAsync(apikey,options);

            ChatCompletions completions = resp.Value;

            string response = completions.Choices[0].Message.Content;
            Message responseMessage = new(response, false);
            return responseMessage;
        }
    }
}