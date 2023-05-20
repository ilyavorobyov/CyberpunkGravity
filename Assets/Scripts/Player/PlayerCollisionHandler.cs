using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerCollisionHandler : MonoBehaviour
{
    public event UnityAction AntiGravitySwitchEnabled;
    public event UnityAction AntiGravitySwitchOffed;
    public event UnityAction<int> CoinCollected;
    public event UnityAction<int> BatteryTaken;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void InvokeAntiGravitySwitchOff()
    {
        AntiGravitySwitchOffed.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AntiGravField antiGravityField))
        {
            AntiGravitySwitch antiGravitySwitch = antiGravityField.GetComponentInParent<AntiGravitySwitch>();
            antiGravitySwitch.SetCollisionHandler(this);
            AntiGravitySwitchEnabled.Invoke();
        }

        if (collision.TryGetComponent(out Enemy enemy))
        {
            _player.Die();
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
            AntiGravitySwitchOffed.Invoke();
        }
    }
}