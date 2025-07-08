namespace Generator;

using System.IO.Abstractions;

public class FileGenerator
{
    private readonly IFileSystem _fileSystem;

    public FileGenerator(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void WriteText(string path, string content)
    {
        _fileSystem.File.WriteAllText(path, content);
    }

    public string ReadText(string path)
    {
        return _fileSystem.File.ReadAllText(path);
    }
}
