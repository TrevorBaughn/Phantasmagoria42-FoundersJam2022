using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform PlayerTransform;

    public enum AIState { Idle, Divebombing, Died, Charging, Shooting };

    AIState _currentState = AIState.Idle;
    AIState _previousState;

    [SerializeField]
    float _shootingAttackRange;

    [SerializeField]
    float _diveBombRange;

    [SerializeField]
    float _diveBombSpeed;

    [SerializeField]
    float _diveBombDamage;
    [SerializeField]
    AudioClip diveBombSound;

    

    MovementController _movementController;
    ButterflyGun _gun;
    AudioSource _audioSource;
    HealthController _healthController;
    Vector3 _diveToPosition;
    Animator _animator;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<MovementController>(out _movementController);
        TryGetComponent<ButterflyGun>(out _gun);
        TryGetComponent<AudioSource>(out _audioSource);
        TryGetComponent<HealthController>(out _healthController);
        TryGetComponent<Animator>(out _animator);
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is to the left of the crow...
        if (PlayerTransform.position.x < transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        // If the player is to the right of the crow...
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
        HandleFSM();
    }


    void HandleFSM()
    {
        // If we're in the idle state...
        if (_currentState == AIState.Idle)
        {
            // Do nothing.
            // If the distance from the player to this crow is within the divebomb range...
            if (Vector2.Distance(PlayerTransform.position, transform.position) <= _diveBombRange)
            {
                _diveToPosition = PlayerTransform.position;
                // Change our state to the dive bombing.
                ChangeState(AIState.Divebombing);
            }
            // Else, if our distance from the player to this crow is within the shooting range...
            else if (Vector2.Distance(PlayerTransform.position, transform.position) <= _shootingAttackRange)
            {
                // Change our state to the shooting state.
                ChangeState(AIState.Shooting);
            }
            else if (_healthController != null)
            {
                if (_healthController.CurrentHealth <= 0)
                {
                    ChangeState(AIState.Died);
                }
            }
            else
            {
                ChangeState(AIState.Charging);
            }
        }
        // If we're in the Charging state...
        else if (_currentState == AIState.Charging)
        {
            // If we have a movement controller...
            if (_movementController != null)
            {
                // Calculate the movement direction.
                var __moveToDirection = PlayerTransform.position - transform.position;
                // Move in that direction.
                _movementController.Move((Vector2)__moveToDirection);
            }
            // If the distance from the player to this crow is within the divebomb range...
            if (Vector2.Distance(PlayerTransform.position, transform.position) <= _diveBombRange)
            {
                _diveToPosition = PlayerTransform.position;
                // Change our state to the dive bombing.
                ChangeState(AIState.Divebombing);
            }
            // Else, if our distance from the player to this crow is within the shooting range...
            else if (Vector2.Distance(PlayerTransform.position, transform.position) <= _shootingAttackRange)
            {
                // Change our state to the shooting state.
                ChangeState(AIState.Shooting);
            }
            else if (_healthController != null)
            {
                if (_healthController.CurrentHealth <= 0)
                {
                    ChangeState(AIState.Died);
                }
            }

        }
        // If we're Divebombing...
        else if (_currentState == AIState.Divebombing)
        {
            if(_animator != null)
            {
                _animator.SetBool("Diving", true);
            }
            // If we have an audio source...
            if (_audioSource != null)
            {
                // Set the divebomb sound in the audio source.
                if (_audioSource.clip != diveBombSound)
                    _audioSource.clip = diveBombSound;
                // Play the sound.
                if (_audioSource.isPlaying == false)
                    _audioSource.Play();
            }
            // Calculate the movement direction.
            var __moveToDirection = _diveToPosition - transform.position;
            __moveToDirection.Normalize();
            // Move to the dive to position.
            transform.position += __moveToDirection * _diveBombSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, _diveToPosition) < .8f)
            {
                // If our distance from the player to this crow is within the shooting range...
                if (Vector2.Distance(PlayerTransform.position, transform.position) <= _shootingAttackRange)
                {
                    // Change our state to the shooting state.
                    ChangeState(AIState.Shooting);
                }
               
                else
                {
                    ChangeState(AIState.Charging);
                }

                if (_audioSource != null)
                    _audioSource.Stop();
                if (_animator != null)
                {
                    _animator.SetBool("Diving", false);
                }
            }
             else if (_healthController != null)
            {
                if (_healthController.CurrentHealth <= 0)
                {
                    ChangeState(AIState.Died);
                }
            }
        }
        // If we're shooting...
        else if (_currentState == AIState.Shooting)
        {
            if (_gun != null)
            {
                // Calculate the fire direction.
                var __fireDirection = PlayerTransform.position - _gun.Firepoint.position;
                // Shoot at the player's direction.
                _gun.Shoot(__fireDirection);
            }
            // If our distance from the player to this crow is within the dive bomb range...
            if (Vector2.Distance(PlayerTransform.position, transform.position) <= _diveBombRange)
            {
                _diveToPosition = PlayerTransform.position;
                // Change our state to the shooting state.
                ChangeState(AIState.Divebombing);
            }
            else if(Vector2.Distance(PlayerTransform.position, transform.position) >= _shootingAttackRange)
            {
                ChangeState(AIState.Charging);
            }
            else if(_healthController!= null)
            {
                if(_healthController.CurrentHealth <= 0)
                {
                    ChangeState(AIState.Died);
                }
            }
        }
        // If we're dying...
        else if (_currentState == AIState.Died)
        {
            if(TryGetComponent<CircleCollider2D>(out CircleCollider2D circleCollider2D))
            {
                circleCollider2D.enabled = false;
            }
            if (_animator != null)
            {
                _animator.SetTrigger("Die");
            }
            _movementController.Move(-transform.up);
        }
        // If we don't know what state we're in...
        else
        {
            Debug.LogError(this.gameObject.name + " does not know what state they are in!");
        }
    }

    void ChangeState(AIState __newState)
    {
        // Save the current state to be the previous state.
        _previousState = _currentState;
        // Set the current state to the new state.
        _currentState = __newState;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _diveBombRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _shootingAttackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the thing we collided with has a player input component...
        if(collision.gameObject.GetComponent<PlayerInput>() != null)
        {
            // If the player has a health controller...
            if(collision.gameObject.TryGetComponent<HealthController>(out HealthController __playerHealthController))
            {
                // Tell them to take the dive bomb damge.
                __playerHealthController.TakeDamage(_diveBombDamage);
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.Crows.Remove(this);
    }
}
