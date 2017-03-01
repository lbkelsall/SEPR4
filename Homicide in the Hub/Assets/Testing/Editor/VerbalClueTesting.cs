using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class VerbalClueTesting
{

    private VerbalClue verbalClue;
    private NonPlayerCharacter owner;

    [TestFixtureSetUp]
    public void TestSetup()
    {
        verbalClue = new VerbalClue(null,null);
        owner = new NonPlayerCharacter (null,null,null,null,null,null);
        verbalClue.SetOwner (owner);
    }

	[Test]
	public void SetOwnerTest()
	{
		//Assert
		Assert.AreSame(verbalClue.GetOwner (), owner);
	}

	[Test]
	public void GetOwnerTest()
	{
		//Act
		//Try to rename the GameObject
		var fetchedOwner = verbalClue.GetOwner ();

		//Assert
		//The object has a new name
		Assert.AreSame(fetchedOwner, owner);
	}
}
