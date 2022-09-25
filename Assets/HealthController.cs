using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // The max health.
    [SerializeField]
    float _maxHealth;
    // The current health.
    float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// Reduces this health controller's current health value by set damage.
    /// </summary>
    /// <param name="__damageToTake"> The amount of damage dealt.</param>
    public void TakeDamage(float __damageToTake)
    {
        // Subtracts the current health by the damage to take.
        _currentHealth -= __damageToTake;
        // If the current health is less than or equal to...
        if(_currentHealth <= 0)
        {
            // Call the die function.
            Die();
        }
    }
    /// <summary>
    /// Handles the death functionality.
    /// </summary>
    void Die()
    {
        // Destroys this game object.
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Adds to the current health by set amount but does not surpase the maximum.
    /// </summary>
    /// <param name="__healthToHeal">The amount of health to heal.</param>
    public void Heal(float __healthToHeal)
    {
        // Adds the health to heal to the current health.
        _currentHealth += __healthToHeal;
        // Clamp the current health so it's value stays between 0 and the set max health. Prevents overhealing.
        _currentHealth = Mathf.Clamp(__healthToHeal, 0, _maxHealth);
    }
}
