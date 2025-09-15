using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustbinZone : MonoBehaviour                                          //similar script to other zone scripts for dustbin       
{
    public GameObject discardPrompt;

    [Header("Player Input")]
    public KeyCode player1Key = KeyCode.F;
    public KeyCode player2Key = KeyCode.G;  

    private PlayerInventory currentPlayerInventory = null;
    private GameObject currentPlayer = null;
    private bool playerInZone = false;

    private void Start()
    {
        discardPrompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            currentPlayer = other.gameObject;
            currentPlayerInventory = other.GetComponent<PlayerInventory>();
            discardPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)             
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            currentPlayer = null;
            currentPlayerInventory = null;
            discardPrompt.SetActive(false);
        }
    }

    private void Update()                                                                    //using same new 2 player logic as changed in PickUpZone.cs by locally storing info on whatever player is in zone
    {                                                                                        
        if (playerInZone && currentPlayerInventory != null)
        {
            bool discardPressed = false;

            if (currentPlayer.name.Contains("Player1") && Input.GetKeyDown(player1Key))
            {
                discardPressed = true;
            }
            else if (currentPlayer.name.Contains("Player2") && Input.GetKeyDown(player2Key))
            {
                discardPressed = true;
            }

            if (discardPressed)
            {
                if (currentPlayerInventory.carriedVegetables.Count > 0)
                {
                    currentPlayerInventory.DiscardFirstVegetable();
                }
                else
                {
                    Debug.Log("nothing");
                }
            }
        }
    }
}
