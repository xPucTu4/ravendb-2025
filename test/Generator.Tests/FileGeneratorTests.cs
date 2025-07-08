using Generator;
using NSubstitute;
using System.IO.Abstractions;
using Xunit;

namespace Generator.Tests;

public class FileGeneratorTests
{
    [Fact]
    public void WriteText_CallsFileSystem()
    {
        var file = Substitute.For<IFile>();
        var fs = Substitute.For<IFileSystem>();
        fs.File.Returns(file);
        var generator = new FileGenerator(fs);

        generator.WriteText("path.txt", "data");

        file.Received().WriteAllText("path.txt", "data");
    }

    [Fact]
    public void ReadText_ReadsFromFileSystem()
    {
        var file = Substitute.For<IFile>();
        file.ReadAllText("file.txt").Returns("hello");
        var fs = Substitute.For<IFileSystem>();
        fs.File.Returns(file);
        var generator = new FileGenerator(fs);

        var result = generator.ReadText("file.txt");

        Assert.Equal("hello", result);
    }
}
