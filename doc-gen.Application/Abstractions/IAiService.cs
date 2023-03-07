namespace doc_gen.Services;

public interface IAiService
{
    Task<string> GenerateDocs(string text);
}