using UnityEngine;
public enum ItemType
{
    card_wpr, card_wpm, card_sp, card_su, Item
}
[System.Serializable]
public class ItemData
{
    [SerializeField] private int _id;
    [SerializeField] private string _itemName;
    [SerializeField] private string _itemExp;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _itemPrice;
    [SerializeField] private string _dropPrefabPath;
    [SerializeField] private string _iconPath;
    [SerializeField] private int _stackCount;
    [SerializeField] private int _attack;
    [SerializeField] private int _defend;
    [SerializeField] private int _cap;
    private GameObject _dropPrefab;
    private Sprite _icon;

    public int ID { get { return _id; } }
    public string ItemName { get { return _itemName; } }
    public string ItemExplanation { get { return _itemExp; } }
    public ItemType ItemType { get { return _itemType; } }
    public int Price { get { return _itemPrice; } }
    public int MaxCount { get { return _stackCount; } }
    public GameObject DropPrefab
    {
        get
        {
            if (_dropPrefab == null)
            {
                _dropPrefab = Resources.Load(_dropPrefabPath) as GameObject;
            }

            return _dropPrefab;
        }
    }

    public Sprite Icon
    {
        get
        {
            if (_icon == null)
            {
                _icon = Resources.Load<Sprite>(_iconPath);
            }

            return _icon;
        }
    }
}