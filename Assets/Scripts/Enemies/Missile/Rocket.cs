using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Enemy
{
    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
