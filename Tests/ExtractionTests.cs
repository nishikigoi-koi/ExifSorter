namespace Extraction;

public class ExtractionTests : IDisposable
{
    private Extractor extractor;
    public ExtractionTests() { extractor = new Extractor(); }
    

    [InlineData("Data\\Tui-JPG.JPG")]
    [InlineData("Data\\Tui-CR2.CR2")]
    [Theory]
    public void CanReadExifData(string path)
    {
        var exifData = extractor.ReadExifData(path);
        Assert.NotEmpty(exifData);
    }

    [Fact]
    public void FileNotFoundExceptionThrown()
    {
        Assert.Throws<FileNotFoundException>(() => extractor.ReadExifData("Data\\NonExistantFile.jpg"));
    }

    [Fact]
    public void InvalidFileFormatExceptionThrown()
    {
        Assert.Throws<InvalidDataException>(() => extractor.ReadExifData("Data\\NotAnImage.txt"));
    }

    public void Dispose() { }
}
