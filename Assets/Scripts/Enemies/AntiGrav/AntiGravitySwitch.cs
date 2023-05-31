
public class AntiGravitySwitch : Enemy
{
    private PlayerMover _playerMover;

    private void Start()
    {
        _playerMover = Player.GetComponent<PlayerMover>();
    }

    public override void Die()
    {
        base.Die();
        _playerMover.TurnOnGravityChanger();
    }
}