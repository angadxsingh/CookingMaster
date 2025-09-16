using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Image[] orderSlots;                                         //ui slots above customer
    public List<Sprite> availableVegetables;                           //manually made list of all veg sprites
    public List<Sprite> requestedOrder = new List<Sprite>();           //stores the specif list customer is requesting

    private void Start()
    {
        GenerateNewOrder();
    }

    public void GenerateNewOrder()
    {
        requestedOrder.Clear();                                         //clear previous order if there

        for (int i = 0; i < orderSlots.Length; i++)                     //loop through each ui slot
        {
            Sprite randomVeg = availableVegetables[Random.Range(0, availableVegetables.Count)];                   //picks a random veg from the list
            requestedOrder.Add(randomVeg);                              //adds to the requested order

            orderSlots[i].sprite = randomVeg;                           //displays the chosen veg in the ui slot
            orderSlots[i].enabled = true;
        }
    }

    public bool CheckOrder(List<Sprite> servedSalad)                     //called when dish delivered to check if it matches
    {
        if (servedSalad.Count != requestedOrder.Count)                   //if no. of veg do not match, false
            return false;

        for (int i = 0; i < requestedOrder.Count; i++)                   //for each veg in the order, this will compare the served one to the req one by index
        {
            if (servedSalad[i] != requestedOrder[i])                     //if does not match
                return false;
        }
        return true;
    }
}
