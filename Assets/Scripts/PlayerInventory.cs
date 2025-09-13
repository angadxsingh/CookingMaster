using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Sprite carriedVegetableSprite = null;
    public Image carryDisplaySlot;

    private void Start()
    {
        carryDisplaySlot.enabled = false;                       //a blank image slot on the top left that will show the carried vegtable
    }  

    public bool CanPickup()
    {
        return carriedVegetableSprite == null;                  //to check if player can pick up a vegetable, currently for 1 vegetable only
    }

    public void PickupVegetable(Sprite vegetableSprite)
    {
        if (CanPickup())                                           //places the vegetable sprite in the blank top left space to show what vegetabe is being carried
        {
            carriedVegetableSprite = vegetableSprite;
            carryDisplaySlot.sprite = carriedVegetableSprite;
            carryDisplaySlot.enabled = true;
        }
    }
}