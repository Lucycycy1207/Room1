using System;
using UnityEngine;

public class Health
{
    private float currentHealth;
    private float maxHealth;
    private float healthRegenRate;

    public Health()
    {

    }

    public Health(float _currentHealth, float _maxHealth, float _healthRegenRate)
    {
        this.currentHealth = _currentHealth;
        this.maxHealth = _maxHealth;
        this.healthRegenRate = _healthRegenRate;
    }

    public Health(float _maxHealth)
    {
        maxHealth = _maxHealth;
    }

    public float GetHealth()
    {
        return this.currentHealth;
    }
    /// <summary>
    /// Regenerate health when health is less than half.
    /// </summary>
    public void RegenerateHealth()
    {
        if (currentHealth < maxHealth / 2)
        {
            AddHealth(healthRegenRate * Time.deltaTime);
        }
    }

    public void AddHealth(float valueToAdd)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth+valueToAdd);
    }

    public void DeductHealth(float valueToDeduct)
    {
        currentHealth = Mathf.Max(0, currentHealth-valueToDeduct);
    }


    public void SetHealth(float value)
    {
        if (value > maxHealth || value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Valid range for health is between 0 and {maxHealth}");
        }

        currentHealth = value;
        
    }


}
