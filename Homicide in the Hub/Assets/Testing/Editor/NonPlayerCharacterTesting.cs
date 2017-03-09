using System;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
using UnityTest;

public class NonPlayerCharacterTesting
{
    private NonPlayerCharacter npc;
    private bool isMurderer;
    private VerbalClue verbalClue;
    private GameObject prefab;
    private List<string> weaknesses = new List<string> {"test1","test2","test3"};

    private List<string> responses = new List<string>() {
        "Don’t try and force me to tell you anything. I’ve got more money than you.",
        "Don’t patronise me you cretin. I’ve got more money than you.",
        "How dare you threaten me you lunatic, I’ve got more money than you.",
        "No my dear fellow for you see I have more money than you.",
        "Ha ha ha. Not that funny dear fellow, you’ll need more money to make it funnier.",
        "My good man, I know that time is money, but you can’t rush magnificence!",
        "My good man, there isn’t enough money around here to warrant seeing anything.",
        "I thank you for your kindness, but it would be better with some patronage!",
        "My good man, you don’t need my help to solve this. Not to mention there’s no money involved."
    };
    private string[] questioningStyles = new string[9] {
        "Forceful",
        "Condescending",
        "Intimidating",
        "Coaxing",
        "Wisecracking",
        "Rushed",
        "Inquisitive",
        "Kind",
        "Inspiring"
    };

    //Adding in TestFixture setup to clean up code
    [TestFixtureSetUp]
    public void TestSetup()
    {
        prefab = new GameObject ();
        verbalClue = new VerbalClue (null, null);
        npc = new NonPlayerCharacter(null,null,null,prefab,weaknesses,null);
        isMurderer = npc.IsMurderer ();
        //set verbal clue to empty so all tests work
        npc.setVerbalClue(null);
    }


    [Test]
	public void GetMurdererTest()
	{
	    //Check is false
		Assert.IsFalse(npc.IsMurderer ());
		//Act
		npc.SetAsMurderer ();

		Assert.IsTrue(npc.IsMurderer ());
	}

	[Test]
	public void SetMurdererTest()
	{
		//Act
	    npc.SetAsMurderer();
		//Assert
		Assert.AreNotEqual(isMurderer,npc.IsMurderer ());
	}

	[Test]
	public void SetVerbalClueTest()
	{
		//Act
		npc.setVerbalClue (verbalClue);

		//Assert
		Assert.AreSame(verbalClue,npc.getVerbalClue ());
	}

	[Test]
	public void GetVerbalClueTest()
	{
		Assert.IsNull (npc.getVerbalClue ());
		//Act
		npc.setVerbalClue (verbalClue);

		//Assert
		Assert.AreSame(verbalClue,npc.getVerbalClue ());
	}

	[Test]
	public void GetPrefabTest()
	{
		//Assert
		Assert.AreSame (npc.GetPrefab (),prefab);

	}

	[Test]
	public void GetWeaknessTest()
	{
		//Assert
		Assert.AreSame (npc.GetWeaknesses (),weaknesses);
	}

	[Test]
	public void GetResponseTest()
	{
		var npc = new NonPlayerCharacter (null, null, null, null, null, responses);

		//Assert
		for (int i = 0; i < questioningStyles.Length; i++) {
			Assert.AreSame (npc.GetResponse (questioningStyles [i]), responses [i]);
		}
	}




}
