using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class CardSelectionUI : MonoBehaviour
{
    public List <Image> CardPlace = new List<Image>();
    public Transform UIContentHolder;
    public GameObject CardSelection;
    public bool CardSelectionActive = false;
    void Start()
    {
        CardSelection.SetActive(false);
    }
    void GetCards()
    {
        //InventoryManager.instance.Itemdata.Sheet1[Cards[0].ItemID].Icon;
        InventoryManager.instance.GetCardItemList();
    }
    public List<Item> TemList = new List<Item>();
    IEnumerator CardViewCorutine()
    {
        while (true)
        {
            yield return null;
            if (CardSelectionActive)
            {
                //get keys
            }
        }
    }
    /*
      public class Item
{
    public int ItemID = -1;
    public int ItemCount = 0;
}
    */

    public int pageind = 0;
    public void RefreshCardPage()
    {
        
        //TemList
        for (int i=0; i < CardPlace.Count; i++)
        {
            if (TemList[pageind * CardPlace.Count + i] != null)
            {
                CardPlace[i].gameObject.SetActive(true);
                CardPlace[i].sprite = InventoryManager.instance.Itemdata.Sheet1[TemList[pageind * CardPlace.Count + i].ItemID].Icon;
            }
            else
            {
                CardPlace[i].gameObject.SetActive(false);
            }
        }
    }
    void Shuffle(int i)
    {
        if (i == 1)
        {
            if((pageind+i) * CardPlace.Count< TemList.Count)
            {
                pageind++;
                RefreshCardPage();
            }
            else
            {
                pageind = 0;
            }
        }
        else
        {
            if (pageind + i == -1)
            {
                int a = TemList.Count / CardPlace.Count;
                pageind = a;
            }
            else
            {
                pageind--;
            }
            
            RefreshCardPage();
        }
    }
    public void CardAppearAndDisapear(bool appear)
    {
        if (appear)
        {
            TemList = InventoryManager.instance.GetCardItemList();
            CardSelection.SetActive(true);
            RefreshCardPage();
        }
        else
        {
            CardSelection.SetActive(false);
        }
    }
    void Update()
    {
        if (CardSelectionActive)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Shuffle(1);
            }

            // Check if the left arrow key is pressed
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Shuffle(-1);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CardSelectionActive = true;
            CardAppearAndDisapear(CardSelectionActive);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            CardSelectionActive = false;
            CardAppearAndDisapear(CardSelectionActive);
        }
        /*if (!CardSelectionActive && Input.GetAxis("OpenCardSelection") == 1)
        {
            //show anim of the wheel
            CardSelectionActive = true;
            CardAppearAndDisapear(CardSelectionActive);
        }
        else if (CardSelectionActive){
            CardSelectionActive = false;
            CardAppearAndDisapear(CardSelectionActive);
            //Disable anim of the wheel
        }*/
    }
    
}
