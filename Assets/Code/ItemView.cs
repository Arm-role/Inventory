using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI count;

    private void Start()
    {
        InventorySystem.instance.itemView = this;
    }
    public void ItemSetUp(Item item)
    {
        Debug.Log(item != null);
        if (item != null)
        {
            icon.gameObject.SetActive(true);
            icon.sprite = item.icon;
            count.text = item.Count.ToString();
        }
        else
        {
            icon.gameObject.SetActive(false);
        }
    }
    public void ItemUpdate(Item item)
    {
        if (item != null)
        {
            icon.sprite = item.icon;
            count.text = item.Count.ToString();
        }
    
    }
}