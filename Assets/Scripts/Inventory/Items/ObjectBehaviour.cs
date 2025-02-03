using UnityEngine;

public class ObjectBehaviour : MonoBehaviour, IInteractable
{
    public Item Item;
    public virtual void GetToPosition(Transform t)
    {
        transform.position = t.position;
        transform.localRotation = Quaternion.identity;
    }

    public void ChangeLayer()
    {
        int HandLayer = LayerMask.NameToLayer("ObjectsInHand");
        gameObject.layer = HandLayer;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = HandLayer;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Implementar interaccion");
    }

    public virtual string GetDescription()
    {
        return "Insertar descripcion";
    }

    public virtual void Use()
    {
        Debug.Log("Uso default");
    }
}
