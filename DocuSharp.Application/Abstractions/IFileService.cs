namespace doc_gen.Services;

public interface IFileService
{
    List<KeyValuePair<string, string[]>> ReadFile(string path);
    void ReplaceFileContent(string path, string content);
}