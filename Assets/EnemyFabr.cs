using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFabr : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AntiGravitySwitch _antiGravitySwitch;
    [SerializeField] private RocketTuner _rocketTuner;
    [SerializeField] private ElectricBall _electricBall;
    [SerializeField] private EnemyDrone _enemyDrone;
    [SerializeField] private EnemyShooter _enemyShooter;
    [SerializeField] private EnemyMissileSystem _enemyMissileSystem;

    private void Start()
    {
/*        var rocketTuner = Instantiate(_antiGravitySwitch, new Vector3(16, -3, 0), Quaternion.identity);
        rocketTuner.Init(_player);
        var rocketTuner2 = Instantiate(_rocketTuner, new Vector3(0, 0, 0), Quaternion.identity);
        rocketTuner2.Init(_player);

*/        /*        var electricBall = Instantiate(_electricBall, transform.position, Quaternion.identity);
                electricBall.Init(_player);
        */
        /*        var enemyDrone = Instantiate(_enemyDrone, new Vector3(22,0,0), Quaternion.identity);
                enemyDrone.Init(_player);

        */     /*   var enemyShooter = Instantiate(_enemyShooter, new Vector3(25, 0, 0), Quaternion.identity);
                enemyShooter.Init(_player);*/

        /*        var enemyMissileSystem = Instantiate(_enemyMissileSystem, new Vector3(25, -3.5f, 0), Quaternion.identity);
                enemyMissileSystem.Init(_player);
        */
    }
}