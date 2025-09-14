using UnityEngine;
using UnityEngine.UI;

public class PickUpZone : MonoBehaviour
{
    public GameObject pickupPrompt;
    public Sprite vegetableSprite;

    private bool playerInZone = false;

    private void Start()
    {
        pickupPrompt.SetActive(false);                   //using prompt when player in zone to let them pick up
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))                  //using a collider and a zone to let player pick up vegetable
        {
            playerInZone = true;
            pickupPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            pickupPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();                   
            if (inventory != null)
            {
                if (inventory.CanPickup())                                       //fetching the PlayerInventory.cs to check if new CanPickup() condition is met which includes checking if there is a slot in inventory
                {
                    inventory.PickupVegetable(vegetableSprite);      
                }
                else
                {
                    Debug.Log("inventory full");                            //for now a debug log, will change to a prompt later
                }
            }
        }
    }
}
