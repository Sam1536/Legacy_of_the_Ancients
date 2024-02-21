using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator anim;
    public float coliderRadius;
    private bool isOpened;
  
    public List<Items> Item = new List<Items>();

 

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayer();   
    }


    //private void OnMouseDown() // jeito simples de fazer o bau abrir 
    //{
    //    OpenChest();
    //}

    public void GetPlayer()
    {
        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * coliderRadius), coliderRadius))
        {
            if (!isOpened)
            {
                if (c.gameObject.CompareTag("Player"))
                {
                    //detecta o Player
                    if (Input.GetKeyDown(KeyCode.E))
                        OpenChest();

                }
            }    

           
        }
    }


    // executa quando pega os itens dentro do baú  
    public  void OpenChest()
    {
        foreach (Items i in Item)
        {
            Inventory.instance.CreaterItem(i);
        }
       
        anim.SetTrigger("open");
        isOpened = true;

    }
  
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, coliderRadius);
    }

}
