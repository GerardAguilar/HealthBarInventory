using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {
    public int slotID;
    private Inventory inv;

    void Start() {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData){//knows which gameobject is being dragged
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();//pointerDrag is the gameobject being dragged
        //Debug.Log(inv.items[slotID].ID);
        if (inv.items[slotID].ID == -1){//if this slot is empty
            inv.items[droppedItem.slot] = new Item();//resets slot in the inventory array
            inv.items[slotID] = droppedItem.item;//insert droppedItem into inventory array
            droppedItem.slot = slotID;//updates the id, the actual movement is in ItemData.cs
        }
        else if(droppedItem.slot != slotID) {//if this slot has something, and it's not the same place it came from
            
            Transform item = this.transform.GetChild(0);//grab the other item this slot has
            item.GetComponent<ItemData>().slot = droppedItem.slot;//change that other item's slot to the original slot of the current item being held
            item.transform.SetParent(inv.slots[droppedItem.slot].transform);//set that other item's parent as the original slot of the current item being held
            item.transform.position = inv.slots[droppedItem.slot].transform.position;//change that other item's position to the original slot of the current item being held

            //Now that the slot is free
            droppedItem.slot = slotID;//assign the current item's slot to this one
            droppedItem.transform.SetParent(this.transform);//set the current item's parent to this one
            droppedItem.transform.position = this.transform.position;//change the current item's position to this one

            inv.items[droppedItem.slot] = item.GetComponent<ItemData>().item;//update the inventory with other item's new index
            inv.items[slotID] = droppedItem.item;//update the inventory with the current item's new index
        }
    }

}
