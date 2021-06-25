using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
