using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem: MonoBehaviour , IBeginDragHandler, IDragHandler,IEndDragHandler
{
    public static GameObject ItemBeginDragged;
    Vector3 startPos;
    Transform StartParent;

    public Items item;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = item.icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ItemBeginDragged = gameObject;
        startPos = transform.position;
        StartParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        ItemBeginDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == StartParent)
        {
            transform.position = startPos;
        }
    }

    public void SetParent(Transform SlotTransform, Slots slot)
    {
        if (item.SlotTypen.ToString() == slot.SlotTypen.ToString())
        {
            transform.SetParent(SlotTransform);
            item.GetAction();
        }
        else if(slot.SlotTypen.ToString() == "Inventory")
        {
            transform.SetParent(SlotTransform);
            item.RemoveStats();
        }
    }
}
