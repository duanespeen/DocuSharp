namespace DocuSharp.Application.Abstractions;

public interface IFileService
{
    KeyValuePair<string, string[]> ReadFile(string path);
    void ReplaceFileContent(string path, string content);
}