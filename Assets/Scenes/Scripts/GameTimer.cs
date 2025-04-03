using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    public bool isTimerRunning = true;

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                timerText.text = "00:00";
                EndGame();
            }
        }
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

void EndGame()
{
    // Big red text that says TIME'S UP!
    timerText.text = "TIME'S UP!";
    timerText.fontSize = 80;
    timerText.color = new Color32(255, 50, 50, 255);

    // Get player lives
    int p1Lives = GameObject.Find("player_1_0").GetComponent<PlayerHealth>().currentLives;
    int p2Lives = GameObject.Find("player_2_0").GetComponent<PlayerHealth>().currentLives;

    // Determine winner or tie
    if (GameSession.Instance != null)
    {
        if (p1Lives > p2Lives)
            GameSession.Instance.winnerName = "Player 1";
        else if (p2Lives > p1Lives)
            GameSession.Instance.winnerName = "Player 2";
        else
            GameSession.Instance.winnerName = ""; // Tie
    }

    // Load GameOverScene after short delay
    Invoke("LoadGameOverScene", 2f);
}

void LoadGameOverScene()
{
    SceneManager.LoadScene("GameOverScene");
}

}
