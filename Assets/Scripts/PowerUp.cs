using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Speed, Score, Time }           //enumerator to define types of power ups
    public PowerUpType type;                                 //to assign specific power up to prefabs

    public float speedDuration = 7f;                         //duration of speed boost
    public float speedMultiplier = 1.5f;                     //speed multiplier for boost, 1.5x speed
    public int scoreBonus = 3;                               //score bonus on score powerup
    public float timeBonus = 7f;                             //time addition on time power up

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))                             //if player is in contact with power up
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();             //get their inventory and controller
            PlayerController controller = other.GetComponent<PlayerController>();

            if (inventory != null && controller != null)
            {
                int id = inventory.playerID;                          //fetches id

                switch (type)                                         //using a switch case for simplicity to decide what happens depending on power up type
                {
                    case PowerUpType.Speed:
                        controller.StartCoroutine(controller.SpeedBoost(speedMultiplier, speedDuration));               //starts a coroutine in the players controller that makes them faster
                        break;

                    case PowerUpType.Score:
                        ScoreManager.Instance.AddScore(id, scoreBonus);                                  //adds score bonus
                        break;

                    case PowerUpType.Time:                                                           //adds extra time to the player timer
                        if (id == 1)
                            GameManager.Instance.player1Time += timeBonus;
                        else if (id == 2)
                            GameManager.Instance.player2Time += timeBonus;
                        break;
                }

                Destroy(gameObject);                                                     //removes power up after pickup
            }
        }
    }
}