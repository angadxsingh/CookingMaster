using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;                                //globally accessible 

    [Header("Timer Settings")]                                         //each player starting time
    public float player1Time = 120f;
    public float player2Time = 120f;

    [Header("UI")]                                      
    public Text player1TimerText;
    public Text player2TimerText;
    public GameObject gameOverPanel;                                   //game over panel
    public Text finalScoreP1Text;                                      //display of player 1 final score on screen
    public Text finalScoreP2Text;                                      //for player 2

    private bool gameOver = false;

    private void Awake()                                               //singleton initialization, making sure there is only one game manager to avoid duplication
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()                               
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Update()                                             
    {
        if (gameOver) return;                                             //if game is already over, do nothing 

        if (player1Time > 0) player1Time -= Time.deltaTime;               //does the countdown in the timer
        if (player2Time > 0) player2Time -= Time.deltaTime;

        player1Time = Mathf.Max(player1Time, 0);                          //to just make sure timer countdown does not go to negative 
        player2Time = Mathf.Max(player2Time, 0);

        UpdateUI();                                                       //updates the timer display

        if (player1Time <= 0 && player2Time <= 0)                         //if both player times have reached 0, EndGame() is applied
        {
            EndGame();
        }
    }

    private void UpdateUI()               
    {
        if (player1TimerText != null)                                                                              //to display the current time on the screen as an integer        
            player1TimerText.text = "P1 Time: " + Mathf.CeilToInt(player1Time).ToString();

        if (player2TimerText != null)
            player2TimerText.text = "P2 Time: " + Mathf.CeilToInt(player2Time).ToString();
    }

    private void EndGame()
    {
        gameOver = true;                                                               //marks game as ended

        PlayerController[] players = FindObjectsOfType<PlayerController>();                        //fetches both player controllers, saves it and then disables it
        foreach (PlayerController pc in players)
        {
            pc.enabled = false;
        }

        if (ScoreManager.Instance != null)                                                            //to display final score
        {
            if (finalScoreP1Text != null)
                finalScoreP1Text.text = "P1 Final Score: " + ScoreManager.Instance.GetScore(1);

            if (finalScoreP2Text != null)
                finalScoreP2Text.text = "P2 Final Score: " + ScoreManager.Instance.GetScore(2);
        }

        if (gameOverPanel != null)                                             //displays game over panel
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;                                                   //freezes time
    }

    public void RestartGame()                                                 //restart the game and resume the time
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()                                                    //quits the game and the editor playing
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}