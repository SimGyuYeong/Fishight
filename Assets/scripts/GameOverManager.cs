using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("�ִ��̵��Ÿ� UI")]
    [SerializeField]
    private Text textMaxMove = null;
    // Start is called before the first frame update
    void Start()
    {
        textMaxMove.text = string.Format("�ִ� �̵��Ÿ�: {0}m", PlayerPrefs.GetFloat("MAXMOVE", 0));
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene("GameStart");
    }
}
