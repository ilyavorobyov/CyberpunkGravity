using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private int _maxHealth;
    private Vector3 _healthBarScale;

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void HealthChange(int health)
    {
        _healthBarScale = transform.localScale;
        float newHealthBarScaleX = (float) health / _maxHealth;
        _healthBarScale.x = newHealthBarScaleX;
        transform.localScale = _healthBarScale;
    }
}