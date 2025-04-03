using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 5;
    public int currentLives;
    public TextMeshProUGUI healthText;
    public string playerName = "Player";

    void Start()
    {
        currentLives = maxLives;
        UpdateHealthDisplay();
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        if (currentLives < 0) currentLives = 0;
        UpdateHealthDisplay();

        if (currentLives == 0)
        {
            Debug.Log(playerName + " is defeated!");
            GetComponent<PlayerController>().enabled = false;
            
        if (GameSession.Instance != null)
        {
            GameSession.Instance.winnerName = playerName == "Player 1" ? "Player 2" : "Player 1";
        }

            SceneManager.LoadScene("GameOverScene");
        }
    }

    void UpdateHealthDisplay()
    {
        if (healthText != null)
            healthText.text = playerName + ": " + currentLives.ToString();
    }

}
