using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerCollisionHandler : MonoBehaviour
{
    public event UnityAction<int> CoinCollected;
    public event UnityAction<int> BatteryTaken;

    private PlayerMover _playerMover;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AntiGravField antiGravityField))
        {
            _playerMover.TurnOffGravityChanger();
        }

        if (collision.TryGetComponent(out Coin coin))
        {
            CoinCollected?.Invoke(coin.GetVolume());
            coin.Die();
        }

        if (collision.TryGetComponent(out Battery battery))
        {
            BatteryTaken?.Invoke(battery.GetVolume());
            battery.Die();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AntiGravField antiGravityField))
        {
            _playerMover.TurnOnGravityChanger();
        }
    }
}