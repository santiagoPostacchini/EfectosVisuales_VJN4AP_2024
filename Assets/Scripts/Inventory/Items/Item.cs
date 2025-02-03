using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public Transform positionInHand;
    public GameObject prefab;
    public ItemType type;
    public ActionType actionType;

    [Header("Only UI")]
    public Sprite image;
    public bool stackable = false;
    public bool usable = false;
}

public enum ItemType
{
    Food,
    RawFood,
    Note,
    Photo,
    Gem,
    Tool,
    Bottle,
    Misc
}

public enum ActionType
{
    Cook,
    Eat,
    Read,
    Inspect,
    Interact,
    Use,
    Drink,
    Custom
}

