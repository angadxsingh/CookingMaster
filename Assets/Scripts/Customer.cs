using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Image[] orderSlots;                                         //ui slots above customer
    public List<Sprite> availableVegetables;                           //manually made list of all veg sprites
    public List<Sprite> requestedOrder = new List<Sprite>();           //stores the specif list customer is requesting

    [Header("Time Reward")]
    public float maxWaitTime = 25f;                                    //total time customer will have to wait
    public float fullPointTime = 15f;                                 // time for full point
    private float waitTimer;

    public Text timerText;                                             //to display timer over customer head

    private bool hasOrder = false;                                     //to see if customer has active order

    private void Start()
    {
        GenerateNewOrder();
    }

    private void Update()
    {
        if (hasOrder)
        {
            waitTimer -= Time.deltaTime;                                  //countdown over customer

            if (timerText != null)
            {
                timerText.text = Mathf.CeilToInt(waitTimer).ToString() + "s";                  //to update the countdown on ui
            }

            if (waitTimer <= 0)                                //if timer runs out, puts penalty and new order/customer
            {
                PenalizePlayers();
                GenerateNewOrder();
            }
        }
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

        waitTimer = maxWaitTime;                                   //reset timer when customer gets order
        hasOrder = true;

        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(waitTimer).ToString() + "s";
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

    public int GetPoint()
    {
        if (waitTimer > fullPointTime)
        {
            return 5;                                 //full points if delivery within 15s
        }
        else
        {
            return 3;                                 //3 points if between 15 and 25
        }
    }

    private void PenalizePlayers()                    //function to subtract -2 if order missed
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(1, -2);
            ScoreManager.Instance.AddScore(2, -2);
        }
    }
}