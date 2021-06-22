[System.Serializable]
public class ItemObject
{
    public int itemID;
    public int count;

    public static ItemObject CopyByValue(ItemObject item)
    {
        var io = new ItemObject();
        io.itemID = item.itemID;
        io.count = item.count;
        return io;
    }
}
