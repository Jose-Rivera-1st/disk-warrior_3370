using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public float returnDelay = 3f;

    void Start()
    {
        Invoke("ReturnToMainMenu", returnDelay);
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("TitleScene"); // Make sure this matches your title scene name
    }
}
