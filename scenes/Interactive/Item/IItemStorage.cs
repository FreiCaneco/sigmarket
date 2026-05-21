namespace sigmarket.Scenes.Interactive.Item;

public interface IItemStorage
{
    ItemData GetItem(int index);
    void SetItem(int index, ItemData item);
    void SwapItems(int fromIndex, int toIndex);
}
