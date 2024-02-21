using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slots : MonoBehaviour,IDropHandler
{

    [System.Serializable]
    public enum SlotsTypen
    {
        Inventory,
        Helmet,
        Armor,
        Belt,
        Pants,
        BootsRight,
        BootsLeft,
        GioveRight,
        GioveLeft,
        Shield,
        Sword,
        Cord,
        Cover,
        ShoulderPadRight,
        ShoulderPadLeft,
        bracelet,
        Ring
    }

    public SlotsTypen  SlotTypen;

    public GameObject Item
    {
        get
        {
            if(transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }

        set
        {
        }
    }

    public void OnDrop(PointerEventData eventData)
    { 

        if (!Item)
        {
            DragItem.ItemBeginDragged.GetComponent<DragItem>().SetParent(transform,this);
        }
       // transform.position = Input.mousePosition;
    }
}
