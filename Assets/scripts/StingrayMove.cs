using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingrayMove : MonoBehaviour
{
    [Header("가오리 이동속도")]
    [SerializeField]
    protected float speed = 0.5f;
    [Header("가오리 추가이동거리")]
    [SerializeField]
    private float addmove = 259f;
    [Header("가오리 체력")]
    [SerializeField]
    private int hp = 5;

    private Collider2D col = null;
    protected GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;
    private BulletMove bulletMove = null;

    private bool isDamaged = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletMove = FindObjectOfType<BulletMove>();
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

    protected virtual void EenemyMove()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            collision.transform.SetParent(gameManager.Pool.transform, false);
            collision.gameObject.SetActive(false);
            if (hp > 1)
            {
                if (isDamaged) return;
                isDamaged = true;
                StartCoroutine(Damaged());
                return;
            }
            gameManager.AddDurability(10f);
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
