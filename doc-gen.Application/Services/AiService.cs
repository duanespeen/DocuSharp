using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace doc_gen.Services;

public class AiService : IAiService
{
    private readonly OpenAIService _openAiService;

    public AiService()
    {
        _openAiService = new OpenAIService(new OpenAiOptions()
        {
            ApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
        });
    }

    public async Task<string> GenerateDocs(string text)
    {
        var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem("You are a C# Annex D Documentation comments writer"),
                ChatMessage.FromUser(
                    @"Add C# Annex D Documentation comments to the following code: Console.WriteLine(""Hello World!"");"),
                ChatMessage.FromAssistance(
                    @"
                        /// <summary>
                        /// Writes the message ""Hello World!"" to the console.
                        /// </summary>
                        Console.WriteLine(""Hello World!"");
                    "),
                ChatMessage.FromUser(
                    $"Add C# Annex D Documentation comments only on the Class Methods, this means you can ignore the Constructor too, of the following code: {text}"),
            },
            Model = Models.ChatGpt3_5Turbo
        });

        if (!completionResult.Successful)
        {
            throw new Exception(completionResult.Error.Message);
        }

        return completionResult.Choices
            .Select(x => x.Message.Content)
            .Aggregate((x, y) => x + y)
            .Replace("```", "");
    }
}