using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFabr : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AntiGravitySwitch _antiGravitySwitch;
    [SerializeField] private RocketTuner _rocketTuner;

    private void Start()
    {
        var rocketTuner = Instantiate(_antiGravitySwitch, new Vector3(16,-3,0), Quaternion.identity);
        rocketTuner.Init(_player);
        var rocketTuner2 = Instantiate(_rocketTuner, new Vector3(0, 0, 0), Quaternion.identity);
        rocketTuner2.Init(_player);
    }
}