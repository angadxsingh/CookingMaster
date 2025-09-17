using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerZone : MonoBehaviour
{
    public GameObject deliverPrompt;
    public Customer customer;                                  //reference to the customer to validate orders
    public int allowedPlayerID = 1;                            //added an id check so only desginated player can serve to designated customer

    [Header("Player Input")]
    public KeyCode player1Key = KeyCode.F;
    public KeyCode player2Key = KeyCode.G;

    private GameObject currentPlayer = null;                     //same logic
    private PlayerInventory currentPlayerInventory = null;
    private bool playerInZone = false;

    private void Start()
    {
        deliverPrompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            currentPlayer = other.gameObject;
            currentPlayerInventory = other.GetComponent<PlayerInventory>();
            deliverPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            currentPlayer = null;
            currentPlayerInventory = null;
            deliverPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInZone && currentPlayerInventory != null && currentPlayerInventory.HasDish() && currentPlayerInventory.playerID == allowedPlayerID)             //checking if player is carrying a dish and also checking id
        {
            bool deliverPressed = false;
            int id = currentPlayerInventory.playerID;

            if (id == 1 && Input.GetKeyDown(player1Key))
                deliverPressed = true;
            else if (id == 2 && Input.GetKeyDown(player2Key))
                deliverPressed = true;

            if (deliverPressed)
            {
                List<Sprite> deliveredDish = currentPlayerInventory.DropDish();               //takes dish from player, copies and clears like explained in PlayerInventory.cs

                if (customer.CheckOrder(deliveredDish))                       //checks if delivered dish, matches order
                {
                    Debug.Log($"Player{id} is correct");
                    ScoreManager.Instance.AddScore(id, 2);                    //score added
                }
                else
                {
                    Debug.Log($"Player{id} is wrong");
                    ScoreManager.Instance.AddScore(id, -1);                   //score deducted
                }   

                customer.GenerateNewOrder();                                  //generates a new order
                deliverPrompt.SetActive(false);
            }
        }
    }
}
