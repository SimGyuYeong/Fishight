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
    [Header("청새치 프리팹")]
    [SerializeField]
    private GameObject enemyMarlinPrefab = null;
    [Header("가오리 프리팹")]
    [SerializeField]
    private GameObject enemyStingrayPrefab = null;
    [Header("연어 프리팹")]
    [SerializeField]
    private GameObject enemySalmonPrefab = null;
    [Header("아귀 프리팹")]
    [SerializeField]
    private GameObject enemyMonkPrefab = null;
    [Header("이동거리 추가수치")]
    [SerializeField]
    private float addmove = 1f;

    private float Delay = 0.1f;

    private float durability = 100f;
    private float maxMove = 0f;
    private float move = 0f;

    public BulletPoolManager Pool { get; private set; }

    float delay = 0f;
    float positionY = 0f;
    int spawncount = 0;

    private void Awake()
    {
        Pool = FindObjectOfType<BulletPoolManager>();
    }

    void Start()
    {
        MinPosition = new Vector2(-9.5f, -4f);
        MaxPosition = new Vector2(9.5f, 4f);
        maxMove = PlayerPrefs.GetFloat("MAXMOVE", 0);
        StartCoroutine(Move());
        StartCoroutine(SpawnMarlin());
        StartCoroutine(SpawnCrownfish());
        StartCoroutine(SpawnStingray());
        StartCoroutine(SpawnSalmon());
        StartCoroutine(SpawnMonk());
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
            RemoveDurability(0.2f);
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
            while (true)
            {
                if(move>=3500) delay = Random.Range(0.6f, 2.5f);
                else if (move>=1000) delay = Random.Range(1f, 3f);
                else delay = Random.Range(1.5f, 4f);
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

    private IEnumerator SpawnMarlin()
    {
            while (true)
            {
                if (move >= 500)
                {
                    if(move>=1500) delay = Random.Range(1.5f, 4f);
                    else if (move>=2700) delay = Random.Range(1f, 3f);
                    else delay = Random.Range(1.5f, 6f);
                    positionY = Random.Range(3.5f, -3.5f);
                    spawncount = Random.Range(1, 3);
                    for (int i = 0; i < spawncount; i++)
                    {
                        Instantiate(enemyMarlinPrefab, new Vector2(11f, positionY), Quaternion.identity);
                        yield return new WaitForSeconds(0.25f);
                    }

                    
                }
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator SpawnStingray()
    {
        while (true)
        {
            if (move >= 800)
            {
                if(move>=2000) delay = Random.Range(3f, 15f);
                else if (move>=4500) delay = Random.Range(1.5f, 10f);
                else delay = Random.Range(5f, 20f);
                positionY = Random.Range(3.5f, -3.5f);
                spawncount = 1;
                for(int i = 0; i < spawncount; i++)
                {
                    Instantiate(enemyStingrayPrefab, new Vector2(11f, positionY), Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }
                
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator SpawnSalmon()
    {
        while (true)
        {
            if (move >= 1300)
            {
                if(move>=3200) delay = Random.Range(2f, 5.5f);
                else delay = Random.Range(3.5f, 8f);
                positionY = Random.Range(3.5f, -3.5f);
                Instantiate(enemySalmonPrefab, new Vector2(-11f, positionY), Quaternion.identity);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator SpawnMonk()
    {
        while (true)
        {
            if (move >= 3000)
            {
                if(move>=9000) delay = Random.Range(3f, 10f);
                else delay = Random.Range(5f, 15f);
                positionY = Random.Range(3.5f, -3.5f);
                Instantiate(enemyMonkPrefab, new Vector2(11f, positionY), Quaternion.identity);
            }
            yield return new WaitForSeconds(delay);
        }
    }

}
