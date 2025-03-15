using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Play button clicked!");
        SceneManager.LoadScene("SampleScene");
    }
}
