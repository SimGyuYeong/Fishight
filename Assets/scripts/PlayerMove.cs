using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("ÀÌµ¿¼Óµµ")]
    [SerializeField]
    private float speed = 2f;
    [Header("ÃÑ¾Ë")]
    [SerializeField]
    private GameObject bulletPrefab = null;
    [Header("ÃÑ¾Ë¹ß»ç¼Óµµ")]
    [SerializeField]
    private float bulletDelay = 0.5f;
    [Header("ÃÑ¾Ë¹ß»çÁÂÇ¥")]
    [SerializeField]
    private Transform bulletPosition = null;

    private Vector2 targetPosition = Vector2.zero;
    private GameManager gameManager = null;
    private BulletPool bulletPool = null;

    private SpriteRenderer spriteRenderer = null;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        bulletPool = FindObjectOfType<BulletPool>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(SpawnBullet());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.x = Mathf.Clamp(targetPosition.x, gameManager.MinPosition.x, gameManager.MaxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, gameManager.MinPosition.y, gameManager.MaxPosition.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        
    }
    
    private IEnumerator SpawnBullet()
    {
        while (true)
        {
            SpawnOrInstantiate();
            yield return new WaitForSeconds(bulletDelay);
        }
    }

    private void SpawnOrInstantiate()
    {
        if (bulletPrefab == null) return;
        GameObject bullet = null;
        if (gameManager.Pool.transform.childCount > 0)
        {
            bullet = gameManager.Pool.transform.GetChild(0).gameObject;
            bullet.transform.SetParent(bulletPosition, false);
            bullet.transform.position = bulletPosition.position;
            bullet.SetActive(true);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, bulletPosition);
            bullet.transform.position = bulletPosition.position;
        }
        bullet.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        if (collision.gameObject.CompareTag("Marlin")) gameManager.RemoveDurability(1f);
        else if (collision.gameObject.CompareTag("Stingray")) gameManager.RemoveDurability(1.8f);
        else gameManager.RemoveDurability(1f);
        Destroy(collision.gameObject);
        StartCoroutine(Dead());
        isDead = true;
    }

    private IEnumerator Dead()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        isDead = false;
    }

    
}
