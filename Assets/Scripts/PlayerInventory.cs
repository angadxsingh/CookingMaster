using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int maxItems = 2;                                              //max items to be carried by a player
    public List<Sprite> carriedVegetables = new List<Sprite>();           //List to store vegetables as sprites
    public Image[] carryDisplaySlots;                                     //for 2 blank slots that show inventory vegetables

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
}
