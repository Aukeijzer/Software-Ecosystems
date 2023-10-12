namespace BackendTests;

public class ExampleTests
{
    Ecosystem projectsService;
    [SetUp]
    public void Setup()
    {
        //Test fails if this is commented out
        projectsService = new Ecosystem();
    }

    // Example test
    [Test]
    public void ExampleTest()
    {
        Assert.DoesNotThrow(() => projectsService.GetHashCode());
    }
}