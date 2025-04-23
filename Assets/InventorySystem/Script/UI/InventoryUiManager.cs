using UnityEngine;
using System.Collections.Generic;
public class InventoryUiManager : MonoBehaviour
{
    public Transform SelectedParent;
    public static InventoryUiManager instance;
    public GameObject SlotPrefab;
    public Transform Layout;
    public List<InventorySlot> slotInstance = new List<InventorySlot>();
    List<Item> currentSlot = new List<Item>();
    List<Item> AdditionalSlot = new List<Item>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        OpenInventory(InventoryManager.instance.Inventory, InventoryManager.instance.ConnectionInventory);
    }
    public bool ChangeItemLoc(int OriginalLoc,int NewLoc,bool Primary1 = true, bool Primary2 = true)
    {
        if (Primary1)
        {
            Item temp;
            if (Primary1)
            {
                temp = currentSlot[OriginalLoc];
            }
            else
            {
                temp = AdditionalSlot[OriginalLoc];
            }

            if (Primary2)
            {
                if (Primary1)
                {
                    currentSlot[OriginalLoc] = currentSlot[NewLoc];
                }
                else
                {
                    AdditionalSlot[OriginalLoc] = currentSlot[NewLoc];
                }
                currentSlot[NewLoc] = temp;
            }
            else
            {
                if (Primary1)
                {
                    currentSlot[OriginalLoc] = AdditionalSlot[NewLoc];
                }
                else
                {
                    AdditionalSlot[OriginalLoc] = AdditionalSlot[NewLoc];
                }
                AdditionalSlot[NewLoc] = temp;
            }
        }
        OpenInventory(currentSlot, AdditionalSlot);
        return true;
    }
    public void OpenInventory(List<Item> Inventory, List<Item> ConnectionInventory = null)
    {
        RemoveOldData();
        currentSlot = Inventory;

        for (int i=0;i<currentSlot.Count ; i++)
        {
            slotInstance.Add(Instantiate(SlotPrefab, Layout).GetComponent<InventorySlot>());
            slotInstance[i].SetSlot(currentSlot[i].ItemID, i, currentSlot[i].ItemCount,true);
        }
        AdditionalSlot = ConnectionInventory;

        for (int i = 0; i < AdditionalSlot.Count; i++)
        {
            slotInstance.Add(Instantiate(SlotPrefab, Layout).GetComponent<InventorySlot>());
            slotInstance[i].SetSlot(AdditionalSlot[i].ItemID, i, AdditionalSlot[i].ItemCount,false);
        }
    }
    void RemoveOldData()
    {
        for (int i = 0; i < slotInstance.Count; i++)
        {
            Destroy(slotInstance[i].gameObject);
        }
       slotInstance.Clear();
        //AdditionalSlot.Clear();
        //currentSlot.Clear();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
