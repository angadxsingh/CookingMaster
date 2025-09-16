using UnityEngine;
using UnityEngine.UI;   

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;     

    private int player1Score = 0;                  //starting scores
    private int player2Score = 0;

    [Header("UI References")]
    public Text player1ScoreText;
    public Text player2ScoreText;

    private void Awake()                        //singleton initialization, making sure there is only one score manager to avoid duplication
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int playerID, int amount)                          //simply to add score to the correct player based on their id
    {
        if (playerID == 1)
            player1Score += amount;
        else if (playerID == 2)
            player2Score += amount;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (player1ScoreText != null)
            player1ScoreText.text = "P1: " + player1Score.ToString();

        if (player2ScoreText != null)
            player2ScoreText.text = "P2: " + player2Score.ToString();
    }
}
