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
        var fileContent = _fileService.ReadFile(Console.ReadLine());
        _fileService.ReplaceFileContent(fileContent.Key, await _aiService.GenerateDocs(string.Concat(fileContent.Value)));
    }
}