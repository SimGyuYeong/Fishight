using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    [Header("최대이동거리 UI")]
    [SerializeField]
    private Text textMaxMove = null;
    [Header("현재이동거리 UI")]
    [SerializeField]
    private Text textMove = null;
    [Header("내구도 슬라이더")]
    [SerializeField]
    private Slider sliderDB = null;
    [Header("흰동가리 프리팹")]
    [SerializeField]
    private GameObject enemyCrownfishPrefab = null;
    [Header("이동거리 추가수치")]
    [SerializeField]
    private float addmove = 1f;

    private float Delay = 0.1f;

    private float durability = 100f;
    private float maxMove = 0f;
    private float move = 0f;

    void Start()
    {
        MinPosition = new Vector2(-9.5f, -4f);
        MaxPosition = new Vector2(9.5f, 4f);
        maxMove = PlayerPrefs.GetFloat("MAXMOVE", 0);
        StartCoroutine(Move());
        StartCoroutine(SpawnCrownfish());
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        sliderDB.maxValue = 100;
        sliderDB.value = durability;
    }

    public void UpdateUI()
    {
        textMaxMove.text = string.Format("최대이동거리: {0}m", maxMove);
        textMove.text = string.Format("현재이동거리: {0}m", move);
    }

    private IEnumerator Move()
    {
        while (true)
        {
            RemoveDurability(0.1f);
            Move(addmove);
            yield return new WaitForSeconds(Delay);
        }
    }

    public void RemoveDurability(float remove)
    {
        durability -= remove;
        if(durability<=0)
        {
            SceneManager.LoadScene("GameOver");
        }
        UpdateUI();
    }

    public void AddDurability(float add)
    {
        durability += add;
        if (durability > 100) durability = 100;
        UpdateUI();
    }

    public void Move(float addmove)
    {
        move += addmove;
        if (move > maxMove)
        {
            maxMove = move;
            PlayerPrefs.SetFloat("MAXMOVE", maxMove);
        }
        UpdateUI();
    }

    private IEnumerator SpawnCrownfish()
    {
        if (move <= 5000)
        {
            float delay = 0f;
            float positionY = 0f;
            int spawncount = 0;

            while (true)
            {
                delay = Random.Range(2f, 5f);
                positionY = Random.Range(3.5f, -3.5f);
                spawncount = Random.Range(1, 6);
                for (int i = 0; i < spawncount; i++)
                {
                    Instantiate(enemyCrownfishPrefab, new Vector2(11f, positionY), Quaternion.identity);
                    yield return new WaitForSeconds(0.25f);
                }

                yield return new WaitForSeconds(delay);
            }
        }
    }
}
