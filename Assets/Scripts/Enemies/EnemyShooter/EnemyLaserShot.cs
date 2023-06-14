using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserShot : Enemy
{
    private float _speed = 6;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}