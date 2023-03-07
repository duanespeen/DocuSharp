
using DocuSharp.Application.Abstractions;

namespace DocuSharp.Application.Services
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Reads a file at the specified path and returns the file path and contents as a key-value pair.
        /// </summary>
        /// <param name="path">The path to the file to be read.</param>
        /// <returns>A key-value pair representing the file path and contents.</returns>
        public KeyValuePair<string, string[]> ReadFile(string path)
        {
            return new KeyValuePair<string, string[]>(path, File.ReadAllLines(path));
        }

        /// <summary>
        /// Replaces the contents of a file at the specified path with the given content.
        /// </summary>
        /// <param name="path">The path to the file to be modified.</param>
        /// <param name="content">The new content to replace the file with.</param>
        public void ReplaceFileContent(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
