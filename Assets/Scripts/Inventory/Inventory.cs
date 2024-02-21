using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slots> slots = new List<Slots>();

    public GameObject SlotParent;

    public static Inventory instance;

    private void Start()
    {
        instance = this;
        GetSlots();
    }

    public  void GetSlots()
    {
        foreach (Slots s in SlotParent.GetComponentsInChildren<Slots>())
        {
            slots.Add(s);
        }

        //CreaterItem();
    }

    public void CreaterItem(Items item)
    {

        foreach(Slots s in slots)
        {
            if(s.transform.childCount == 0)
            {
                GameObject CurrentItem = Instantiate(GameController.Instance.itemPrefabs,s.transform);
                CurrentItem.GetComponent<DragItem>().item = item;

                return;
            }
        }

     
    }

}
