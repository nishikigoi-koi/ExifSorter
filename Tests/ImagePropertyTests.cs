namespace Extraction;

public class ImagePropertyTests : IDisposable
{
    private Extractor extractor;
    public ImagePropertyTests() { extractor = new Extractor(); }

    [InlineData("Data\\Tui-JPG.JPG")]
    [InlineData("Data\\Tui-CR2.CR2")]
    [Theory]
    public void CanExtractImageProperties(string path)
    {
        var exifData = extractor.ReadExifData(path);
        var imageProperties = extractor.ExtractProperties(exifData);
        
        Assert.NotEmpty(imageProperties);
    }

    [InlineData("Data\\Tui-JPG.JPG")]
    [InlineData("Data\\Tui-CR2.CR2")]
    [Theory]
    public void CanExtractSpecificProperties(string path)
    {
        var exifData = extractor.ReadExifData(path);
        var imageProperties = extractor.ExtractProperties(exifData);
        
        Assert.Contains(imageProperties, prop => prop.Name == "CameraModel");
        Assert.Contains(imageProperties, prop => prop.Name == "DateTaken");
    }

    [Fact]
    public void ConsistentDataBetweenFormats()
    {
        var jpgExifData = extractor.ReadExifData("Data\\Tui-JPG.JPG");
        var cr2ExifData = extractor.ReadExifData("Data\\Tui-CR2.CR2");
        
        var jpgProperties = extractor.ExtractProperties(jpgExifData);
        var cr2Properties = extractor.ExtractProperties(cr2ExifData);
        
        Assert.Equal(jpgProperties.First(p => p.Name == "CameraModel").Value, 
                     cr2Properties.First(p => p.Name == "CameraModel").Value);
    }

    public void Dispose() { }
}
