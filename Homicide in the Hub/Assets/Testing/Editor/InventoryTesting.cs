using NUnit.Framework;

public class InventoryTesting
{
    private Inventory inventory;
    private Item item;

    [TestFixtureSetUp]
    public void TestSetup()
    {
        inventory = new Inventory();
        item = new Item(null,null,null,null);
        inventory.AddItemToInventory (item);
    }

	[Test]
	public void AddItemToInventoryTest()
	{
		//Assert
		//The inventory contains the the item
		Assert.IsTrue (inventory.GetInventory ().Contains (item));
	}

	[Test]
	public void ResetInventoryTest()
	{
		inventory.Reset ();
		//Assert
		//The inventory is empty
		Assert.IsEmpty (inventory.GetInventory ());
	}

	[Test]
	public void GetLengthOfInventoryTest()
	{
		//Assert
		//The inventory is has length one
		Assert.AreEqual (inventory.GetSize (),1);
	}

    [TestFixtureTearDown]
    public void TestCeleanup()
    {
        inventory.Reset();
    }


}
