using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ClueClassForTesting : Clue {

	public ClueClassForTesting(string clueID, string description) : base(clueID, description) {

	}
}

public class ClueTesting
{

    private string clueID;
    private string clueDescription;
    private Clue clue;

    [TestFixtureSetUp]
    public void TestSetup()
    {
        clueID = "My Clue ID";
        clueDescription = "My Clue Description";
        clue = new ClueClassForTesting(clueID,clueDescription);
    }

    [Test]
	public void GetClueIDTest()
	{
		//Assert
		//Can get correct clue id
		Assert.AreEqual(clueID, clue.getID ());
	}

	[Test]
	public void GetClueDescriptionTest()
	{
		//Assert
		//Can get correct description
		Assert.AreEqual(clueDescription, clue.getDescription ());
	}
}
