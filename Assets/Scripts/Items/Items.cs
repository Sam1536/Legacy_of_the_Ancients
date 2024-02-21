using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] 
public class Items : ScriptableObject
{

    public Sprite icon;
    public string Name;
    public float Value;


  

    [System.Serializable]
    public enum Typen
    {
        Potion,
        Elixir,
        Crystal,
        Sword
    }

    public Typen ItemTypen;

    [System.Serializable]
    public enum SlotsTypen
    {
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

    public SlotsTypen SlotTypen;


    public Player player;

    public void GetAction()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


         switch (ItemTypen)
         {

            case Typen.Potion:
                Debug.Log("Health " + Value);
                player.IncreaseStats(Value, 0f, 0f);
                break; 
            
            case Typen.Crystal:
                Debug.Log("Crystal " + Value);
                player.IncreaseStats(0f, Value, 0f);
                break;
            
            case Typen.Elixir:
                Debug.Log("Elixir " + Value);
                break;
            
            case Typen.Sword:
                Debug.Log("Sword " + Value);
                player.IncreaseStats(0f, 0f, 40f);
                break;


         }
    }



    public void RemoveStats()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


        switch (ItemTypen)
        {

            case Typen.Potion:
                Debug.Log("Health " + Value);
                player.DecreaseStats(Value, 0f, 0f);
                break;

            case Typen.Crystal:
                Debug.Log("Crystal " + Value);
                player.DecreaseStats(0f, Value, 0f);
                break;

            case Typen.Elixir:
                Debug.Log("Elixir " + Value);
                break;

            case Typen.Sword:
                Debug.Log("Sword " + Value);
                player.DecreaseStats(0f, 0f, 40f);
                break;
        }
    }
}



