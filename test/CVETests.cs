using System.Runtime.InteropServices;

namespace Ionic.Zip.Tests;

public class CVETests
{
    private readonly ITestOutputHelper _output;

    public CVETests(ITestOutputHelper output)
    {
        _output = output;
    }

    private bool IsOsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    private void Extract_ZipFile(string fileName)
    {
        var sourceDir = Directory.GetCurrentDirectory();
        _output.WriteLine("Current Dir: {0}", sourceDir);

        var fqFileName = Path.Combine(sourceDir, "zips", fileName);

        _output.WriteLine("Reading zip file: '{0}'", fqFileName);
        using var zip = ZipFile.Read(fqFileName);
        const string extractDir = "extract";
        if (Directory.Exists(extractDir))
        {
            Directory.Delete(extractDir, true);
        }
        foreach (ZipEntry e in zip)
        {
            _output.WriteLine("{1,-22} {2,9} {3,5:F0}%   {4,9}  {5,3} {6:X8} {0}",
                e.FileName,
                e.LastModified.ToString("yyyy-MM-dd HH:mm:ss"),
                e.UncompressedSize,
                e.CompressionRatio,
                e.CompressedSize,
                (e.UsesEncryption) ? "Y" : "N",
                e.Crc);
            e.Extract(extractDir);
        }
    }

    [Fact]
    public void Extract_ZipWithAbsolutePathsOutside()
    {
        if (IsOsWindows)
        {
            Assert.Throws<IOException>(() => Extract_ZipFile("absolute-path-traversal.zip"));
        }
        else
        {
            Extract_ZipFile("absolute-path-traversal.zip");
            Assert.False(File.Exists(@"C:\Windows\Temp\foo"));
            Assert.True(File.Exists(@"./extract/C:/Windows/Temp/foo"));
        }
    }

    //[Fact]
    //public void Extract_ZipWithZipSlip()
    //{
    //    var zipFile = IsOsWindows ? "zip-slip-win.zip" : "zip-slip.zip";
    //    Assert.Throws<IOException>(() => Extract_ZipFile(zipFile));
    //}
}