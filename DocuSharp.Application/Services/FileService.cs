namespace doc_gen.Services
{
    public class FileService : IFileService
    {
        public List<KeyValuePair<string, string[]>> ReadFile(string path)
        {
            var readText = File.ReadAllLines(path);
            var splitText = readText
                .Select((s, i) => new { Index = i, Value = s })
                .GroupBy(x => x.Index / 200)
                .Select(x => x.Select(v => v.Value).ToArray())
                .ToArray();
            return splitText
                .Select((s, i) => new KeyValuePair<string, string[]>($"{path}-{i}", s))
                .ToList();
        }
        
        public void ReplaceFileContent(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
