using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyGun : MonoBehaviour
{
    // The projectile prefab to shoot.
    [SerializeField]
    Projectile _projectilePrefab;
    // The spot to spawn our bullets to shoot from.
    [SerializeField]
    Transform _firePoint;
    // The time delay between shots.
    [SerializeField]
    float _fireDelay;
    // Stores whether we can shoot or not.
    bool _canShoot;
    // The delay timer routine.
    Coroutine _delayTimerRoutine;

    private void Start()
    {
        _canShoot = true;
    }

    // Shoots the projectile prefab and starts the delay timer.
    public void Shoot(Vector2 shootDirection)
    {
        // If we can shoot...
        if (_canShoot == true)
        {

            // Instaniate the prefab at the firepoint position.
            var newProjectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);

            newProjectile.MovementDirection = shootDirection.normalized;

            // Set can shoot to false.
            _canShoot = false;

            // If the delay timer routine is already running, stop it.
            if (_delayTimerRoutine != null)
                StopCoroutine(_delayTimerRoutine);

            // Start the delay timer routine.
            _delayTimerRoutine = StartCoroutine( DelayTimer(_fireDelay));
        }
    }

    /// <summary>
    /// The delay timer routine.
    /// </summary>
    /// <param name="__delayTime">The amount of time to delay.</param>
    IEnumerator DelayTimer(float __delayTime)
    {
        // Set the starting time to our timer variable.
        var __timer = __delayTime;

        // Do this while timer is greater than zero.
        do
        {
            // Subtract the timer by delta time.
            __timer -= Time.deltaTime;
            // Wait till next frame.
            yield return null;
            // Check to see if the timer is still greater than zero.
        } while (__timer > 0);

        // Once it's done, set can shoot to true.
        _canShoot = true;
    }

}
