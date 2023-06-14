using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerForceField _playerForceField;

    private PlayerMover _playerMover;
    private Player _player;
    private bool _inForceField;

    private Coroutine _turnOnForceField;

    public event UnityAction<int> CoinCollected;
    public event UnityAction<int> BatteryTaken;

    private void Awake()
    {
        _inForceField = false;
        _playerMover = GetComponent<PlayerMover>();
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AntiGravField antiGravityField))
        {
            _playerMover.TurnOffGravityChanger();
        }

        if (collision.TryGetComponent(out ForceField forceField))
        {
            _inForceField = true;
            TurnOnForceField();
            Destroy(forceField.gameObject);
        }

        if (collision.TryGetComponent(out Enemy enemy))
        {
            if(!_inForceField)
            {
                _player.Die();
            }
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

    private void TurnOnForceField()
    {
        if(_turnOnForceField != null)
        {
            StopCoroutine(_turnOnForceField);
        }

        _turnOnForceField = StartCoroutine(ForceFieldAction());
    }

    private IEnumerator ForceFieldAction()
    {
        var waitForSeconds = new WaitForSeconds(_playerForceField.GetDuration());
        _playerForceField.gameObject.SetActive(true);
        yield return waitForSeconds;
        Debug.Log("end");
        _inForceField = false;
        _playerForceField.gameObject.SetActive(false);
    }
}