using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkMove : MonoBehaviour
{
    [Header("아귀 이동속도")]
    [SerializeField]
    protected float speed = 0.2f;
    [Header("아귀 추가이동거리")]
    [SerializeField]
    private float addmove = 455f;
    [Header("아귀 체력")]
    [SerializeField]
    private int hp = 10;

    protected GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;

    private bool isDamaged = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        EenemyMove();
        CheckLimit();
    }

    private void CheckLimit()
    {
        if (transform.position.x < gameManager.MinPosition.x)
        {
            Destroy(gameObject);
        }
    }

    private void EenemyMove()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            collision.transform.SetParent(gameManager.Pool.transform, false);
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            if (hp > 1)
            {
                if (isDamaged) return;
                isDamaged = true;
                StartCoroutine(Damaged());
                return;
            }
            gameManager.AddDurability(35f);
            gameManager.Move(addmove);
            Destroy(gameObject);
        }
    }

    private IEnumerator Damaged()
    {
        hp--;
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        isDamaged = false;
    }
}
