namespace Shaka.Godot.OnReady.Tests;

public class OnReadyAttributeTests
{
    [Fact]
    public Task InitGeneratesOnReadyAttributeWithoutSource()
    {
        var driver = VerifyConfig.BuildDriver();

        return Verify(driver, VerifyConfig.Settings);
    }
}