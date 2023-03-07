using doc_gen.Services;

namespace doc_gen.BackgroundService
{
    public class DocGen : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly IFileService _fileService;
        private readonly IAiService _aiService;
        
        public DocGen(IFileService fileService, IAiService aiService)
        {
            _fileService = fileService;
            _aiService = aiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var file in _fileService.ReadFile(Console.ReadLine()))
                _fileService.ReplaceFileContent(file.Key, await _aiService.GenerateDocs(string.Concat(file.Value)));
        }
    }
}