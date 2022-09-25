using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    // Desired movement velocity based on the inputs recieved.
    Vector2 _desiredVelocity;
    MovementController _movementController;
    ButterflyGun _gun;

    // Awake is called when the object is first created in the scene and before the first frame it appears in the game. Good for setting variables needed at the start of this script's life time.
    void Awake()
    {
        // Instaniate the desiredVelocity Vector 2.
        _desiredVelocity = new Vector2();
        // If the movement controller is on this object, set a reference to it.
        TryGetComponent<MovementController>(out _movementController);
        // If the butterfly gun is on this object, set a reference to it.
        TryGetComponent<ButterflyGun>(out _gun);
    }

    // Update is Unity callback function that is called once per frame. Great for grabbing inputs from the player or managing animations that are needed to be check on a frame to frame basis.
    void Update()
    {
        // Grab the inputs and store them into desired velocity variable.
        // Grab the horizontal axis inputs (Left & Right arrow, A & D, or left joystick tilted left or right).
        _desiredVelocity.x = Input.GetAxis("Horizontal");
        // Grab the vertical axis inputs (Up & down arrow, W & S, or left joystick tiled up or down).
        _desiredVelocity.y = Input.GetAxis("Vertical");

        // If our desired velocity's magnitude is > 0.1 (meaning we're putting inputs) and if we have a movement controller saved...
        if(_desiredVelocity.magnitude > 0.1f && _movementController != null)
        {
            // Move us in our desired direction.
            _movementController.Move(_desiredVelocity.normalized);
        }
        // If we press the space bar or we recieve input from the Fire1 axis and if we have a reference to our gun...
        if ((Input.GetAxis("Fire1")) > 0.1f && _gun != null)
        {
            Debug.Log("Shoot");
            _gun.Shoot(transform.right);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.Player = null;
    }
}
