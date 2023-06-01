using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RollingSaw : Enemy
{
    private Rigidbody2D _rigidbody;
    private float _normalGravity = 2;
    private float _reversedGravity = -2;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        int gravityNumber = Random.Range(0, 2);

        if(gravityNumber == 0 )
        {
            _rigidbody.gravityScale = _normalGravity;
        }
        else
        {
            _rigidbody.gravityScale = _reversedGravity;
        }
    }

    private void Update()
    {
        transform.position += Vector3.left * _speed* Time.deltaTime;
    }
}
