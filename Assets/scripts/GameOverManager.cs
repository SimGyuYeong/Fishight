using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("최대이동거리 UI")]
    [SerializeField]
    private Text textMaxMove = null;
    // Start is called before the first frame update
    void Start()
    {
        textMaxMove.text = string.Format("최대 이동거리: {0}m", PlayerPrefs.GetFloat("MAXMOVE", 0));
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene("GameStart");
    }
}
