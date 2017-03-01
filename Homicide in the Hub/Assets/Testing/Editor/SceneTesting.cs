using NUnit.Framework;

public class SceneTesting
{

    private string sceneName = "My Scene Name";
    private Scene scene;
    private NonPlayerCharacter npc;
    private Item item;

    [TestFixtureSetUp]
    public void TestSetup()
    {
        scene = new Scene(sceneName);
        item = new Item(null,null,null,null);
        npc = new NonPlayerCharacter(null,null,null,null,null,null);
    }

    [Test]
	public void GetSceneNameTest()
	{
		//Assert
		Assert.AreSame (scene.GetName (),sceneName);
	}

	public void AddNPCToArrayTest()
	{
	    //Act
		scene.AddNPCToArray (npc);

		//Assert
		Assert.IsTrue (scene.GetCharacters ().Contains (npc));
	}

	[Test]
	public void AddItemToArrayTest()
    {
		//Act
		scene.AddItemToArray (item);

		//Assert
		Assert.IsTrue (scene.GetItems ().Contains (item));
	}

	[Test]
	public void ResetTest()
	{
		//Act
		scene.AddItemToArray (item);
		scene.AddNPCToArray (npc);
		scene.ResetScene ();

		//Assert
		Assert.IsEmpty (scene.GetCharacters ());
		Assert.IsEmpty (scene.GetItems ());
	}
}
