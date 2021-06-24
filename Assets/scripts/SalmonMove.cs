using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonMove : EnemyMove
{
    protected override void EenemyMove()
    {
        speed = 10f;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    protected override void CheckLimit()
    {
        if (transform.position.x > gameManager.MaxPosition.x)
        {
            Destroy(gameObject);
        }
    }
}
