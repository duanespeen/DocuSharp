using DocuSharp.Application.Abstractions;

namespace DocuSharp.BackgroundService;

public class DocGen : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly IAiService _aiService;
    private readonly IFileService _fileService;

    public DocGen(IFileService fileService, IAiService aiService)
    {
        _fileService = fileService;
        _aiService = aiService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Enter file path: ");
        var fileContent = _fileService.ReadFile(Console.ReadLine() ?? throw new InvalidOperationException());
        _fileService.ReplaceFileContent(fileContent.Key, await _aiService.GenerateDocs(string.Concat(fileContent.Value)));
    }
}