using MetadataExtractor;
namespace Extraction;

public class Extractor
{
    public IEnumerable<MetadataExtractor.Directory> ReadExifData(string filepath)
    {
        if (!File.Exists(filepath))
        {
            throw new FileNotFoundException($"The file '{filepath}' was not found.");
        }

        if (!IsValidImageFormat(filepath))
        {
            throw new InvalidDataException($"The file '{filepath}' is not a valid image format.");
        }

        var exifData = ImageMetadataReader.ReadMetadata(filepath);
        return exifData;
    }

    private bool IsValidImageFormat(string filepath)
    {
        var validExtensions = new[]
        {
            // Standard image formats
            ".avif", ".bmp", ".dng", ".eps", ".gif",
            ".heif", ".heic", ".ico",
            ".jpg", ".jpeg", ".jfif",
            ".pbm", ".pgm", ".ppm", ".pnm",
            ".pcx", ".png", ".psd", ".tga",
            ".tiff", ".tif", ".webp",

            // Camera RAW formats
            ".3fr",          // Hasselblad
            ".arw",          // Sony
            ".crw", ".cr2", ".cr3", ".crx", // Canon
            ".gpr",          // GoPro
            ".kdc",          // Kodak
            ".nef",          // Nikon
            ".orf",          // Olympus
            ".pef",          // Pentax
            ".raf",          // Fujifilm
            ".rw2",          // Panasonic
            ".rwl",          // Leica
            ".srw",          // Samsung
        };

        var fileExtension = Path.GetExtension(filepath);
        return validExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
    }
}
