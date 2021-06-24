using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [Header("이동 속도")]
    [SerializeField]
    private float speed = 10f;

    private GameManager gameManager = null;
    private PlayerMove playerMove = null;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerMove = FindObjectOfType<PlayerMove>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x > gameManager.MaxPosition.x)
        {
            Despawn();
        }
    }
    public void Despawn()
    {
        transform.SetParent(gameManager.Pool.transform, false);
        gameObject.SetActive(false);
    }
}
