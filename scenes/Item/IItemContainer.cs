namespace sigmarket.Scenes.Item;

public interface IItemContainer
{
    Scenes.Item.ItemData GetItem(int index);
    void SetItem(int index, ItemData item);
    void SwapItems(int fromIndex, int toIndex);
}
