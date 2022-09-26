using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // The max health.
    [SerializeField]
    float _maxHealth;
    // The current health.
    public float CurrentHealth { get; private set; }
    [SerializeField]
    AudioClip _deathSoundClip;

    [SerializeField]
    bool _delayedDeath;
    [SerializeField]
    float _deathTime;
    [SerializeField]
    ParticleSystem _particleSystem;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    /// <summary>
    /// Reduces this health controller's current health value by set damage.
    /// </summary>
    /// <param name="__damageToTake"> The amount of damage dealt.</param>
    public void TakeDamage(float __damageToTake)
    {
        // Subtracts the current health by the damage to take.
        CurrentHealth -= __damageToTake;
        // If the current health is less than or equal to...
        if(CurrentHealth <= 0)
        {
            // Call the die function.
            StartCoroutine( Die());
        }

        if(_particleSystem != null)
        {
            _particleSystem.Play();
        }

    }
    /// <summary>
    /// Handles the death functionality.
    /// </summary>
    IEnumerator Die()
    {
        var __time = _deathTime;
        if(TryGetComponent<AudioSource>(out AudioSource audioSource))
        {
            audioSource.Stop();
        }

        GameManager.instance.kills++;

        AudioSource.PlayClipAtPoint(_deathSoundClip, transform.position);

        do
        {
            __time -= Time.deltaTime;
            yield return null;
        }while (__time > 0) ;

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
        CurrentHealth += __healthToHeal;
        // Clamp the current health so it's value stays between 0 and the set max health. Prevents overhealing.
        CurrentHealth = Mathf.Clamp(__healthToHeal, 0, _maxHealth);
    }
}
