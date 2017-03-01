using UnityEngine;
using NUnit.Framework;

public class ItemTesting
{

    private Sprite itemSprite;
    private Item item;
    private GameObject itemPrefab;

    [TestFixtureSetUp]
    public void TestSetup()
    {
        itemPrefab = new GameObject();
        itemSprite = new Sprite ();
        item = new Item(itemPrefab, null, null, itemSprite);
    }

	[Test]
	public void GetItemSpriteTest()
	{
		//Assert
		//The object has a new name
		Assert.AreSame(itemSprite, item.GetSprite ());
	}

	[Test]
	public void GetItemPrefabTest()
	{
		//Assert
		//The object has a new name
		Assert.AreSame(itemPrefab, item.GetPrefab ());
	}
}
