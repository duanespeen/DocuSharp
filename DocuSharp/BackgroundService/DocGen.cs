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
        //change this to for loop
        var files = _fileService.ReadFile(Console.ReadLine());
        for (var i = 0; i < files.Count; i++)
        {
            var file = files.ElementAt(i);
            _fileService.ReplaceFileContent(file.Key, await _aiService.GenerateDocs(string.Concat(file.Value)));
        }

        foreach (var file in _fileService.ReadFile(Console.ReadLine()))
            _fileService.ReplaceFileContent(file.Key, await _aiService.GenerateDocs(string.Concat(file.Value)));
    }
}