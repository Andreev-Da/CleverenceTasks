namespace CleverenceTasks.Task1.Tests;

public class DataDrivenTests
{
    [Test]
    [Arguments("aaabbcccdde", "a3b2c3d2e")]
    [Arguments("e", "e")]
    [Arguments("dec", "dec")]
    public async Task Compressing(string input, string expectedResult)
    {
        await Assert.That(input.Compress())
            .IsEquivalentTo(expectedResult);
    }
    
    [Test]
    [Arguments("aaabbcccdde")]
    [Arguments("e")]
    [Arguments("dec")]
    public async Task CompressThenDecompress(string input)
    {
        var str = string.Join("", input.Compress().Decompress());
        
        await Assert.That(input.Compress().Decompress())
            .IsEquivalentTo(input);
    }
}