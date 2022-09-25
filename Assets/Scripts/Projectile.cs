using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    // The damage value of this projectile.
    public float Damage = 1f;
    // The projectile force applied when this projectile is fired.
    [SerializeField]
    float _movementSpeed = 1f;
    // How long the bullet exists in the scene before it despawns.
    [SerializeField]
    float _lifeTime;
    [HideInInspector]
    public Vector3 MovementDirection;

    //The time when the bullet respawns.
    float _timeToDie;

    protected virtual void Start()
    {
        // Set the time to despawn.
        _timeToDie = Time.time + _lifeTime;
    }

    private void Update()
    {
        transform.position += MovementDirection * _movementSpeed * Time.deltaTime;

        // If the time since the start of the game is greater than or equal to the death time...
        if (Time.time >= _timeToDie)
        {
            // Destroy this object.
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the thing we collided with has a health controller component...
        if(collision.gameObject.TryGetComponent<HealthController>(out HealthController healthController))
        {
            // Tell it to take damage.
            healthController.TakeDamage(Damage);
        }
        // Destroy this object.
        Destroy(this.gameObject);
    }
}
