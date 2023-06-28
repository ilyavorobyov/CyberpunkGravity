using UnityEngine;

public class ElectricBall : Enemy
{
    [SerializeField] private int _additionalSpeed;
    [SerializeField] private AudioSource _flySound;

    private void Update()
    {
        transform.Translate(Vector3.left * (Speed + _additionalSpeed) * Time.deltaTime);
    }

    public override void SetStartInfo()
    {
        gameObject.SetActive(true);
        PlayerPosition = PlayerObject.transform.position;
        StartPosition = PlayerPosition + new Vector3(PlayerPosition.x + AddToXPosition, 0, 0);
        transform.position = StartPosition;
        _flySound.PlayDelayed(0);
    }
}