using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustbinZone : MonoBehaviour                                          //similar script to other zone scripts for dustbin       
{
    public GameObject discardPrompt;  

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
            discardPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)             
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            discardPrompt.SetActive(false);
        }
    }

    private void Update()                                                                    //prompts to drop the first vegetable in inventory via a new function in PlayerInventory
    {                                                                                        //can use a similar function to put vegetable on chopping tray later on
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
            if (inventory != null)
            {
                inventory.DiscardFirstVegetable();
            }
        }
    }
}
