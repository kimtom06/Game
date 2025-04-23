using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int Location = -1;
    public int ItemCount = 1;
    public int ID = -1;
    public Text AmountText;
    public Image IconImage;
    bool PrimarySlot = true;
    ItemData item;
    public void SetSlot(int _ID,int _Location, int _ItemCount, bool _PrimarySlot)
    {
        PrimarySlot = _PrimarySlot;
        item = InventoryManager.instance.GetItem(_ID);
        if (item != null)
        {
            IconImage.gameObject.SetActive(true);
            ID = _ID;
            Location = _Location;
            ItemCount = _ItemCount;
            IconImage.sprite = item.Icon;
            AmountText.text = ItemCount.ToString();
        }
        else
        {
            IconImage.gameObject.SetActive(false);
        }
    }
    private float pressTime = 0f;
    private bool isPressed = false;
    public float requiredPressTime = 1f;

    void Update()
    {
        if (isPressed&& IconImage.gameObject.activeSelf)
        {
            pressTime += Time.deltaTime;
            if (pressTime >= requiredPressTime)
            {
                OnLongPress();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        pressTime = 0f;

        
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IconImage.transform.parent = transform;
        isPressed = false;
        pressTime = 0f;

        float CloseDistance = 9999;
        int SelectedSquare = Location;
        for (int i = 0; i < InventoryUiManager.instance.slotInstance.Count; i++)
        {
            float thisDis = Vector3.Distance(InventoryUiManager.instance.slotInstance[i].transform.position, IconImage.transform.position);
            if (CloseDistance > thisDis)
            {
                SelectedSquare = i;
                CloseDistance = thisDis;
            }
        }
        if (CloseDistance < 100)
        {
            InventoryUiManager.instance.ChangeItemLoc(Location, SelectedSquare);
            //change fundimental of script and go home
        }

    }
    RectTransform Canvas;

    private void OnLongPress()
    {
        if (!Canvas)
        {
            Canvas = gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        }
        IconImage.transform.parent = InventoryUiManager.instance.SelectedParent;
        IconImage.transform.position = Input.mousePosition;
    }
}
