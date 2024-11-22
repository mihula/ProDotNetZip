namespace Ionic.Zip.Tests;

public class BasicTests
{
    [Fact]
    public void TestCreate()
    {
        var result = new ZipFile();
        Assert.NotNull(result);
    }
}