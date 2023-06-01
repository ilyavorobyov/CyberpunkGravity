using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Enemy
{
    private Vector3 _direction = Vector3.left;

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    public void SetYDirection(float yDirection)
    {
        _direction.y = yDirection;
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
