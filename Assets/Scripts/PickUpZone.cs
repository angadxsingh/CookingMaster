using UnityEngine;
using UnityEngine.UI;

public class PickUpZone : MonoBehaviour
{
    public GameObject pickupPrompt;
    public Sprite vegetableSprite;                                //local vegetable sprite

    [Header("Player Input")]                                      //seperation of both player inputs to pick up
    public KeyCode player1Key = KeyCode.F;
    public KeyCode player2Key = KeyCode.G;

    private PlayerInventory currentPlayerInventory = null;         //reference to inventory to use
    private GameObject currentPlayer = null;                       //reeference to player game object to use here
    private bool playerInZone = false;                             //player not in zone by default

    private void Start()
    {
        pickupPrompt.SetActive(false);                   //using prompt when player in zone to let them pick up
    }

    private void OnTriggerEnter2D(Collider2D other)        //whatever player enters, it will store the player object and its inventory to use for functions
    {
        if (other.CompareTag("Player"))                  //using a collider and a zone to let player pick up vegetable
        {
            playerInZone = true;
            currentPlayer = other.gameObject;                //storing the player object in currentPlayer
            currentPlayerInventory = other.GetComponent<PlayerInventory>();     //storing the players inventory system
            pickupPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)         //clears whatever stored, once player out of zone
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            currentPlayer = null;
            currentPlayerInventory = null;
            pickupPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInZone && currentPlayerInventory != null)                   
        {
            bool pickupPressed = false;                                //assuming by default no key is pressed while entering
            int id = currentPlayerInventory.playerID; 
                                                                                                                    
            if (id == 1 && Input.GetKeyDown(player1Key))       // Check which player is in zone and use their key
            {
                pickupPressed = true;
            }
            else if (id == 2 && Input.GetKeyDown(player2Key))
            {
                pickupPressed = true;
            }
            
            if (pickupPressed)
            {
                if (currentPlayerInventory.CanPickup())      //fetching the specific PlayerInventory to check if CanPickup() condition is met
                {
                    currentPlayerInventory.PickupVegetable(vegetableSprite);       //if met, will add the sprite
                }
                else
                {
                    Debug.Log("inventory full");                      //for now a debug log, will change to a prompt later
                }
            }
        }
    }
}