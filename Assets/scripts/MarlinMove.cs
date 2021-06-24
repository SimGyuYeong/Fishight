using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarlinMove : EnemyMove
{
    [Header("û��ġ �̵��ӵ�")]
    [SerializeField]
    private float mspeed = 10f;

    protected override void EenemyMove()
    {
        transform.Translate(Vector2.left * mspeed * Time.deltaTime);
    }
}
