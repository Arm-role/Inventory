using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class InventoryView : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public Image icon;

    public GameObject ItemUI_Prefab;
    public Transform Layout;
    [HideInInspector] public Item item;

    public GameObject InvenRoot;
    private bool isActiveOB = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isActiveOB)
            {
                Cursor.lockState = CursorLockMode.None;
                isActiveOB = true;
                Populate();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                isActiveOB = false;
            }
            InvenRoot.SetActive(isActiveOB);
        }
    }
    void Populate()
    {
        if (Layout.childCount > 0)
        {
            foreach (Transform child in Layout)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var item in InventorySystem.instance.inventory.Values)
        {
            GameObject slotGO = Instantiate(ItemUI_Prefab, Layout);
            ItemUIDrag slot = slotGO.GetComponent<ItemUIDrag>();

            slot.ItemSetUp(item);
            slot.OnClickItem = OnSelectItem;
        }
    }

    void OnSelectItem(Item item)
    {
        InventorySystem.instance.ItemUpdate(item);

        icon.sprite = item.icon;
        Name.text = item.name;
        Description.text = item.Description;
    }
}
