using UnityEngine;

public class RollingSaw : Enemy
{
    [SerializeField] private float _addSpeed;

    private float _maxYPosition = 8.15f;
    private float _minYPosition = 0.78f;

    private void Update()
    {
        transform.Translate(Vector3.left * (Speed + _addSpeed) * Time.deltaTime);
    }

    public override void SetStartInfo()
    {
        gameObject.SetActive(true);

        int yPositionNumber = Random.Range(0, 2);

        if (yPositionNumber == 0)
        {
            StartPosition = new Vector3(AddToXPosition, _maxYPosition, 0f);
        }
        else
        {
            StartPosition = new Vector3(AddToXPosition, _minYPosition, 0f);
        }

        transform.position = StartPosition;
    }
}