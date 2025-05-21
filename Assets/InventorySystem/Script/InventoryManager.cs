using UnityEngine;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
    public List<Item> Inventory = new List<Item>();

    public List<Item> ConnectionInventory = new List<Item>();

    public int InventorySpace;
    public static InventoryManager instance;
    public ItemList Itemdata;
    void Awake()
    {
        
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        AddItem(1, 4);
        AddItem(0, 2);
        AddItem(12, 1);
    }

    public List<Item> GetCardItemList()
    {
        List<Item> temp = new List<Item>(); 
        for(int i=0; i < Inventory.Count; i++)
        {
            try {
                if (Itemdata.Sheet1[Inventory[i].ItemID].ItemType.ToString().Contains("card"))
                {
                    print("yay");
                    //im a card!
                    bool pass = false;
                    //first check if this has same card in temps
                    for (int j = 0; j < temp.Count; j++)
                    {
                        if (temp[j].ItemID == Inventory[i].ItemID)
                        {
                            temp[j].ItemCount += Inventory[i].ItemCount;
                            pass = true;
                            break;
                        }
                    }
                    //no? ahh i guess youare the newbie
                    if (!pass)
                    {
                        Item gem = new Item();
                        gem.ItemID = Inventory[i].ItemID;
                        gem.ItemCount = Inventory[i].ItemCount;
                        temp.Add(gem);
                    }
                }
            }
            catch
            {
                print("nope");
            }
        }
        print(temp);
        return temp;
    }
    public bool AddItem(int ID,int Count = 1)
    {
        if (Count > 0)
        {
            bool added = false;
            ItemData temp = GetItem(ID);
            if (temp != null)
            {

                for (int i = 0; i < InventorySpace; i++)
                {
                    if (Inventory.Count - 1 < i)
                    {
                        Inventory.Add(new Item());
                    }
                    if (Inventory[i] != null)
                    {
                        if (Inventory[i].ItemID == ID)
                        {
                            if (Inventory[i].ItemCount >= temp.MaxCount)
                            {
                                continue;
                            }
                            else
                            {
                                added = true;
                                Inventory[i].ItemCount += i;
                                break;
                            }
                        }
                    }
                }
                if (!added)
                {
                    for (int i = 0; i < InventorySpace; i++)
                    {
                        if (Inventory.Count - 1 < i)
                        {
                            Inventory.Add(new Item());
                        }
                        if (Inventory[i].ItemID == -1)
                        {
                            Inventory[i] = new Item();
                            Inventory[i].ItemID = ID;
                            Inventory[i].ItemCount = Count;
                            added = true;
                            break;
                        }
                    }
                }
            }
            return added;
        }
        else
        {
            return (false);
        }
    }

    public bool RemoveItem(int ID,int Count = 1)
    {
        return true;
    }
    public void ChangePlace(int origin,int Amount,int moved)
    {

    }
    public ItemData GetItem(int ID)
    {
        for(int i=0; i< Itemdata.Sheet1.Count; i++)
        {
            if (Itemdata.Sheet1[i].ID == ID)
            {
                return Itemdata.Sheet1[i];
            }
        }
        return null;
    }
    public Item GetItemAtPlace(int index)
    {
        if (Inventory[index] != null)
        {
            return Inventory[index];
        }
        else
        {
            return null;
        }
    }
}
