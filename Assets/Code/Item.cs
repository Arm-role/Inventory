using UnityEngine;
[CreateAssetMenu(fileName = "new item", menuName = "Inventory/new item")]
public class Item : ScriptableObject
{
    public GameObject model;
    public string Name;
    public string Description;
    public Sprite icon;
    public int Count;
    public bool Usable;
    public bool Dropable;

    private void OnValidate()
    {
        Name = name;
    }
}
