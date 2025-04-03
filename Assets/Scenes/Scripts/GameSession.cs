using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    public string winnerName = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scenes
        }
        else
        {
            Destroy(gameObject); // Only keep one
        }
    }
}
