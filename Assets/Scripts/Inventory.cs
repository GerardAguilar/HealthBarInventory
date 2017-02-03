using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    [SerializeField]
    GameObject inventoryPanel;
    [SerializeField]
    GameObject slotPanel;

    private int slotAmount;
    private ItemDatabase database;

    //[SerializeField]
    public GameObject inventorySlot;
    //[SerializeField]
    public GameObject inventoryItem;

    
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();



    void Start() {
        slotAmount = 28;
        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;

        database = GetComponent<ItemDatabase>();

        for (int i = 0; i < slotAmount; i++) {
            //add a blank inventory slot into the slots collective
            items.Add(new Item());//back-end, initialize items
            slots.Add(Instantiate(inventorySlot));//back-end, setup slots
            slots[i].transform.SetParent(slotPanel.transform);//front-end, show slots
        }

        AddItem(0);
        AddItem(1);
        AddItem(2);
    }

    /*
    Add item into items list and slots list.
        */
    public void AddItem(int id) {
        Item itemToAdd = database.FetchItemByID(id);//Grab the item from our item library
        for (int i = 0; i < items.Count; i++) {
            //if empty slot (-1 id), then add the itemTo Add to that slot
            if (items[i].ID == -1) {
                items[i] = itemToAdd;//add the item into the items list
                GameObject itemObj = Instantiate(inventoryItem);//just the item template, no icon yet
                itemObj.transform.SetParent(slots[i].transform);//attach item to corresponding slot
                itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;//the itemToAdd from the library should have everything in it.
                itemObj.transform.position = Vector2.zero;//center the gameobject to its parent slot
                itemObj.name = itemToAdd.Title;

                break;
            }
        }
    }
}
