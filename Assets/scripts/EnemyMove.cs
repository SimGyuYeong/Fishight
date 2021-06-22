using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("�򵿰��� �̵��ӵ�")]
    [SerializeField]
    private float speed = 3f;
    [Header("�򵿰��� �߰��̵��Ÿ�")]
    [SerializeField]
    private float addmove = 10f;

    private Collider2D col = null;
    private GameManager gameManager = null;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < gameManager.MinPosition.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            gameManager.Move(addmove);
        }
    }
}
