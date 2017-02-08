using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

/*
Will handle stacking and hovering
*/
public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public int amount = 1;
    public int slot;

    private Transform originalParent;
    private Inventory inv;
    private Tooltip tooltip;
    private Vector2 offset;

    void Start() {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();//Tooltip script has an instance in Inventory
    }

    public void OnPointerDown(PointerEventData eventData) {
        offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);//allows cursor to drag the item from elsewhere aside from the center
        //change the position of the item to be the same as the mouse
        this.transform.position = eventData.position - offset;
    }

    public void OnBeginDrag(PointerEventData eventData)//Handles what happens when you start to drag the item
    {
        if (item != null) {//if there's an item          
            originalParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);//let the item render in front of the other panels, parent reset would be in OnEndDrag         
            GetComponent<CanvasGroup>().blocksRaycasts = false;//turns off raycast to allow movement from one slot to another   
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null){//if there's an item
            //change the position of the item to be the same as the mouse
            this.transform.position = eventData.position - offset;            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inv.slots[slot].transform);//the slot value gets changed when item is dropped onto a different slot (via ItemSlot.cs)
        this.transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);//pass this item's data into the tooltip data
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }


}
