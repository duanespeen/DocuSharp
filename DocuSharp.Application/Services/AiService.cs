using DocuSharp.Application.Abstractions;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace DocuSharp.Application.Services
{
    /// <summary>
    /// A service that provides access to the OpenAI GPT-3 API for generating documentation.
    /// </summary>
    public class AiService : IAiService
    {
        private readonly OpenAIService _openAiService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AiService"/> class with the OpenAI API key retrieved from the environment variables.
        /// </summary>
        public AiService()
        {
            _openAiService = new OpenAIService(new OpenAiOptions
            {
                ApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? string.Empty
            });
        }

        /// <summary>
        /// Generates documentation based on the provided text using the OpenAI GPT-3 API.
        /// </summary>
        /// <param name="text">The text to generate documentation for.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation that returns the generated documentation.</returns>
        public async Task<string> GenerateDocs(string text)
        {
            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("You are a C# Documentation comments writer"),
                    ChatMessage.FromUser(
                        @"Add C# Documentation comments to the following code: Console.WriteLine(""Hello World!"");"),
                    ChatMessage.FromAssistance(
                        @"/// <summary>                        
                          /// Writes the message ""Hello World!"" to the console.                        
                          /// </summary> 
                          Console.WriteLine(""Hello World!"");"),
                    ChatMessage.FromUser(
                        $"Add C# Documentation comments only on the Class Methods, this means you can ignore the Constructor too, of the following code: {text}")
                },
                Model = Models.ChatGpt3_5Turbo
            });
            if (!completionResult.Successful) throw new Exception(completionResult.Error?.Message);
            return completionResult.Choices
                .Select(x => x.Message.Content)
                .Aggregate((x, y) => x + y)
                .Replace("", "");
        }
    }
}
