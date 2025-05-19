using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUIDrag : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public TextMeshProUGUI count;
    [HideInInspector] public Item item;
      
    public Action<Item> OnClickItem;

    public void ItemSetUp(Item item)
    {
        this.item = item;
        icon.sprite = item.icon;
        count.text = item.Count.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickItem?.Invoke(item);
    }
}
