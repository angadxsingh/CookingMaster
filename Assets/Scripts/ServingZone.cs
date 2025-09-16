using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServingZone : MonoBehaviour
{
    public GameObject servePrompt;                   //serving prompt

    [Header("Player Input")]
    public KeyCode player1Key = KeyCode.F;
    public KeyCode player2Key = KeyCode.G;

    private GameObject currentPlayer = null;
    private PlayerInventory currentPlayerInventory = null;
    private bool playerInZone = false;

    public ChoppingBoardZone choppingBoard;           //reference to chopping board

    private void Start()
    {
        servePrompt.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D other)                             //my simple trigger entery, exit logic
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            currentPlayer = other.gameObject;
            currentPlayerInventory = other.GetComponent<PlayerInventory>();
            servePrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            currentPlayer = null;
            currentPlayerInventory = null;
            servePrompt.SetActive(false);
        }
    }

    private void Update() 
    {
        if (playerInZone && choppingBoard != null && choppingBoard.choppedSalad.Count > 0)                    //if the player is in the zone and there is something on the chopping board
        {
            bool servePressed = false;
            int id = currentPlayerInventory.playerID;

            if (id == 1 && Input.GetKeyDown(player1Key))
            {
                servePressed = true;
            }
            else if (id == 2 && Input.GetKeyDown(player2Key))
            {
                servePressed = true;
            }

            if (servePressed)                                            
            {
                List<Sprite> servedSalad = choppingBoard.TakeSalad();                                     //takes the entire list of stored vegetables and stores it in servedSalad
                currentPlayerInventory.TakeDish(servedSalad);                                             //to store the servedSalad in the player inventory

                Debug.Log($"Player{id} is serving a salad");

                servePrompt.SetActive(false);
            }
        }
    }
}