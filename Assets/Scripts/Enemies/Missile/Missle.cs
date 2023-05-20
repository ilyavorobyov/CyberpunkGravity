using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : Enemy
{
    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
    protected override void UseSpecialAbility()
    {
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
