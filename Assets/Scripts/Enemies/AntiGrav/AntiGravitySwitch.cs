using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AntiGravitySwitch : Enemy
{
    private PlayerCollisionHandler _playerCollisionHandler;

    private void Start()
    {
        _playerCollisionHandler = _player.GetComponent<PlayerCollisionHandler>();
    }

    public void SetCollisionHandler(PlayerCollisionHandler playerCollisionHandler)
    {
        _playerCollisionHandler = playerCollisionHandler;
    }

    private void OnDestroy()
    {
        UseSpecialAbility();
    }

    protected override void UseSpecialAbility()
    {
        _playerCollisionHandler.InvokeAntiGravitySwitchOff();
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}