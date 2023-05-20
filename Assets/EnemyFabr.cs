using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFabr : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AntiGravitySwitch _antiGravitySwitch;

    private void Start()
    {
        var antigr = Instantiate(_antiGravitySwitch, new Vector3(23,-3,0), Quaternion.identity);
        antigr.Init(_player);
    }
}