using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    // The damage value of this projectile.
    public float Damage = 1f;
    // The projectile force applied when this projectile is fired.
    [SerializeField]
    float _projectileForce = 100f;
    // The rigidbody component.
    Rigidbody2D _rigidbody;

    protected virtual void Awake()
    {
        // Grab the rigidbody component and save a reference to it.
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        // Apply the projectile force to this in the right direction.
        _rigidbody.AddForce(transform.right * _projectileForce);
    }
}
