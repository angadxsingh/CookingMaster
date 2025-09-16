using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoppingBoardZone : MonoBehaviour                                  //a script to simulate chopping working on similar zone logic used through out
{
    public GameObject chopPrompt;                                      
    public float chopTime = 2f;                                            

    [Header("Player Input")]
    public KeyCode player1Key = KeyCode.F;                                   
    public KeyCode player2Key = KeyCode.G;

    private PlayerInventory currentPlayerInventory = null;                      //internal references to use like did before
    private PlayerController currentPlayerController = null;
    private GameObject currentPlayer = null;
    private bool playerInZone = false;                                         
    private bool isChopping = false;

    public List<Sprite> choppedSalad = new List<Sprite>();                      //salad storing on chopping board
    public Image[] boardDisplaySlots;                                           //UI slots to show vegetables already chopped

    private void Start()
    {
        chopPrompt.SetActive(false); 

        foreach (var slot in boardDisplaySlots)
        {
            slot.enabled = false;                                               //disabling slots initially 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)                                      //stores whichever player data on entry to use here
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            currentPlayer = other.gameObject;
            currentPlayerInventory = other.GetComponent<PlayerInventory>();
            currentPlayerController = other.GetComponent<PlayerController>();
            chopPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            currentPlayer = null;
            currentPlayerInventory = null;
            currentPlayerController = null;
            chopPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInZone && currentPlayerInventory != null && !isChopping)                   //if player is in zone with a valid inventory and is not chopping yet
        {
            bool chopPressed = false;
            int id = currentPlayerInventory.playerID;

            if (id == 1 && Input.GetKeyDown(player1Key))        //action for chopPressed = true
            {
                chopPressed = true;
            }
            else if (id == 2 && Input.GetKeyDown(player2Key))
            {
                chopPressed = true;
            }

            if (chopPressed && currentPlayerInventory.carriedVegetables.Count > 0)           //if the player has pressed the key and is carrying at least 1 veg, it starts chopping process
            {
                StartCoroutine(ChopVegetable());
            }
        }
    }

    private IEnumerator ChopVegetable()                   //chopping vegetable coroutine
    {
        isChopping = true;                                //starts chopping

        if (currentPlayerController != null)                    //disable movement
            currentPlayerController.enabled = false;

        Debug.Log($"Player{currentPlayerInventory.playerID} is chopping...");      
        yield return new WaitForSeconds(chopTime);              //makes player wait for chopping time

        Sprite veg = currentPlayerInventory.carriedVegetables[0];           //takes the first veg from inventory and removes it
        currentPlayerInventory.DiscardFirstVegetable();

        choppedSalad.Add(veg);                                              //adds that veg to the chopped display
        UpdateBoardDisplay();
 
        if (currentPlayerController != null)                                //restarts player movement
            currentPlayerController.enabled = true;                         

        isChopping = false;                                                
    }

    private void UpdateBoardDisplay()                                        //simple display logic we have used before 
    {
        for (int i = 0; i < boardDisplaySlots.Length; i++)
        {
            if (i < choppedSalad.Count)
            {
                boardDisplaySlots[i].sprite = choppedSalad[i];
                boardDisplaySlots[i].enabled = true;
            }
            else
            {
                boardDisplaySlots[i].sprite = null;
                boardDisplaySlots[i].enabled = false;
            }
        }
    }

    public List<Sprite> TakeSalad()                                      //used by my serving script to take the salad for serving and remove it from the chopped display
    {                                                                    //always taking a copy and then clearing
        List<Sprite> saladCopy = new List<Sprite>(choppedSalad);     
        choppedSalad.Clear();
        UpdateBoardDisplay();
        return saladCopy;
    }
}