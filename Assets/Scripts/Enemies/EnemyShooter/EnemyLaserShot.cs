using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserShot : MonoBehaviour
{
    private float _speed = 6;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _speed = 0;
            player.Die();
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out BackFrame backFrame))
        {
            Destroy(gameObject);
        }
    }
}
