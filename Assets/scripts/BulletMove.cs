using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    [SerializeField]
    private float speed = 10f;

    private GameManager gameManagaer = null;

    private void Start()
    {
        gameManagaer = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x > gameManagaer.MaxPosition.x)
        {
            Destroy(gameObject);
        }
    }
}
