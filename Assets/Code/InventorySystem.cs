using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;
    public Dictionary<string, Item> inventory = new();

    public float pickupRange = 3f;
    public Transform dropPoint;

    private Item _item;
    public Item currentItem
    {
        get => _item;
        set
        {
            _item = value;
            if (isFirst)
            {
                itemView.ItemSetUp(_item);
                isFirst = false;
            }
            else
            {
                itemView.ItemUpdate(_item);
            }
        }
    }
    public Item selectItem = null;
    public ItemView itemView = null;

    public bool isFirst = true;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickup();
        }

        if (Input.GetMouseButtonDown(1))
        {
            DropItem();
        }
    }

    public Item FirstItem()
    {
        Item item = null;
        if (inventory.Count > 0)
        {
            item = inventory.Values.FirstOrDefault();
        }

        return item;
    }
    public void ItemUpdate(Item item)
    {
        currentItem = item;
    }
    void TryPickup()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            ItemPickup pickup = hit.collider.GetComponent<ItemPickup>();
            if (pickup != null)
            {
                if (!inventory.ContainsKey(pickup.itemData.Name))
                {
                    inventory[pickup.itemData.Name] = Instantiate(pickup.itemData);
                }
                else
                {
                    inventory[pickup.itemData.Name].Count++;
                }
                if (currentItem != null)
                {
                    foreach (var item in inventory)
                    {
                        if (currentItem.Name == item.Key)
                        {
                            currentItem = item.Value;
                        }
                    }
                }
                else
                {
                    currentItem = Instantiate(pickup.itemData);
                }

                Debug.Log("Picked up: " + pickup.itemData.Name + " " + inventory[pickup.itemData.Name].Count);
               
                Destroy(pickup.gameObject);
            }
        }
    }

    void DropItem()
    {
        if (inventory.Count == 0 && currentItem == null) return;
        if (currentItem == null) currentItem = FirstItem();
        string itemName = currentItem.Name;

        if (inventory.TryGetValue(itemName, out Item items))
        {
            if (items.Count <= 0)
            {
                inventory.Remove(itemName);
            }
            else
            {
                Item itemToDrop = items;
                items.Count--;

                if (itemToDrop.model != null)
                {
                    Instantiate(itemToDrop.model, dropPoint.position, Quaternion.identity);
                    Debug.Log("Dropped: " + itemToDrop.Name);
                    inventory[itemName] = items;
                    if (currentItem != null)
                    {
                        foreach (var item in inventory)
                        {
                            if (currentItem.Name == item.Key)
                            {
                                currentItem = item.Value;
                            }
                        }
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Gizmos.color = Color.green;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        Gizmos.DrawRay(ray.origin, ray.direction * pickupRange);
    }
}
