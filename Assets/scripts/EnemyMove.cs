using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("흰동가리 이동속도")]
    [SerializeField]
    protected float speed = 3f;
    [Header("흰동가리 추가이동거리")]
    [SerializeField]
    private float addmove = 10f;
    [Header("청새치 추가이동거리")]
    [SerializeField]
    private float addmovem = 100f;

    private Collider2D col = null;
    protected GameManager gameManager = null;
    protected BulletMove bulletMove = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();
        bulletMove = FindObjectOfType<BulletMove>();
    }

    // Update is called once per frame
    void Update()
    {
        EenemyMove();
        CheckLimit();
    }

    protected virtual void CheckLimit()
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
            if (gameObject.CompareTag("Marlin"))
            {
                gameManager.Move(addmovem);
                gameManager.AddDurability(0.2f);
            }
            else
            {
                gameManager.Move(addmove);
            }
            collision.transform.SetParent(gameManager.Pool.transform, false);
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
