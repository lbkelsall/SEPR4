using NUnit.Framework;

public class PlayerCharacterTesting {

    private string[] questioningStyles = {"style1","style2","style3"};
    private string questioningStyle = "My Questioning Style";
    private string description = "My Description";
    private PlayerCharacter player;

    [TestFixtureSetUp]
    public void TestSetup()
    {
        player = new PlayerCharacter(null,null, null,questioningStyle,questioningStyles,description);
    }

    [Test]
	public void GetQuestioningStylesTest()
	{
		//Assert
		//The object has a new name
		Assert.AreSame(questioningStyles, player.GetQuestioningStyles ());
	}

	[Test]
	public void GetOverallQuestioningStyleTest()
	{
		//Assert
		//The object has a new name
		Assert.AreSame(questioningStyle, player.GetOverallQuestioningStyle ());
	}

	[Test]
	public void GetDescriptionTest()
	{
		//Assert
		//The object has a new name
		Assert.AreSame(description, player.GetDescription ());
	}
}
