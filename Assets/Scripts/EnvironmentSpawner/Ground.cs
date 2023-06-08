using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        transform.position = new Vector3(_player.transform.position.x + _xOffset, transform.position.y, transform.position.z);
    }
}