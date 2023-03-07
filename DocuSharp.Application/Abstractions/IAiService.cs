namespace DocuSharp.Application.Abstractions;

public interface IAiService
{
    Task<string> GenerateDocs(string text);
}