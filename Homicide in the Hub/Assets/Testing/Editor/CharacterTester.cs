using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.UI;

public class CharacterClassForTesting : Character {

	public CharacterClassForTesting (string characterID, Sprite sprite, string nickname) :  base(characterID, sprite, nickname){

	}
}

public class CharacterTester
{
    //changed this testing class to use TestFixtureSetup instead of initialising everything in each test
    //Added: TestSetup method
    //Removed: The "Arrange" section from each test

    private string characterName, characterNickname;
    private Character character;
    private Sprite characterSprite;

    [TestFixtureSetUp]
    public void TestSetup()
    {
        //Arrange
        characterName = "My Character";
        characterNickname = "My Nickname";
        character = new CharacterClassForTesting (characterName,characterSprite, characterNickname);
    }

	[Test]
	public void GetCharacterNameTest()
	{
		//Assert
		//Can get correct name
		Assert.AreEqual(characterName, character.getCharacterID ());
	}

	[Test]
	public void GetCharacterSpriteTest()
	{
		//Assert
		//Can get correct sprite
		Assert.AreEqual(character.getSprite (), characterSprite);
	}

	[Test]
	public void GetCharacterNicknameTest()
	{
		//Assert
		//Can get correct nickname
		Assert.AreEqual(characterNickname, character.getNickname ());
	}

}
