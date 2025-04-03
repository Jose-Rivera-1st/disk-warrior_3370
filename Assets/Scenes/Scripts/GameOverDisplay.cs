using UnityEngine;
using TMPro;

public class GameOverDisplay : MonoBehaviour
{
    public TextMeshProUGUI winnerText;

    void Start()
    {
        if (GameSession.Instance != null && winnerText != null)
        {
            string result = GameSession.Instance.winnerName;

            if (result == "")
                winnerText.text = "It's a Tie!";
            else
                winnerText.text = result + " Wins!";
        }
    }
}
