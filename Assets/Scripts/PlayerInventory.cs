using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int playerID = 1;                                              //sets a player id that we'll use going forward instead of names
    public int maxItems = 2;                                              //max items to be carried by a player
    public List<Sprite> carriedVegetables = new List<Sprite>();           //List to store vegetables as sprites
    public Image[] carryDisplaySlots;                                     //for 2 blank slots that show inventory vegetables
    public List<Sprite> carriedDish = new List<Sprite>();

    private void Start()
    {
        foreach (var slot in carryDisplaySlots)
        {
            slot.enabled = false;                                          //setting all slots invisble at start
        }
    }

    public bool CanPickup()
    {
        return carriedVegetables.Count < maxItems;                        //checking if there is a slot available 
    }

    public void PickupVegetable(Sprite vegetableSprite)
    {
        if (CanPickup())
        {
            carriedVegetables.Add(vegetableSprite);                       //adds vegetable
            UpdateCarryDisplay();
        }
    }

    private void UpdateCarryDisplay()
    {
        for (int i = 0; i < carryDisplaySlots.Length; i++)                //running a loop through both slots to check if there is a vegetable or sprite enabled
        {
            if (i < carriedVegetables.Count)
            {
                carryDisplaySlots[i].sprite = carriedVegetables[i];             //sets the image to the ui slot based on index
                carryDisplaySlots[i].enabled = true;                            //displays it
            }
            else
            {
                carryDisplaySlots[i].sprite = null;                             
                carryDisplaySlots[i].enabled = false;                           //makes invisible when nothing
            }
        }
    }
 
    public void DiscardFirstVegetable()                                    //function for dustbin action
    {
        if (carriedVegetables.Count > 0)                         
        {
            carriedVegetables.RemoveAt(0);                                 //if there is any veg, removes the first index of it, FIFO principle 
            UpdateCarryDisplay();                                          
        }
    }

    public bool HasDish()                                                  //to check if the player is carrying salad
    {
        return carriedDish.Count > 0;               
    }

    public void TakeDish(List<Sprite> salad)                               //clears any dish list and then copies the salad into player list
    {                                                                      // i am using this in the ServingZone.cs
        carriedDish.Clear();
        carriedDish.AddRange(salad);
    }

    public List<Sprite> DropDish()                                         //makes a copy of the carriedDish and clears it from the inventory
    {                                                                      //I am using this copy now to validate against the customers order in CustomerZone.cs
        List<Sprite> dishCopy = new List<Sprite>(carriedDish);             //had to do this otherwise if cleared, the reference would be empty against customer
        carriedDish.Clear();
        return dishCopy;
    }
}
