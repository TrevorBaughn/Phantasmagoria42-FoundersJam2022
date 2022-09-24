using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementController : MonoBehaviour
{
    // Header attribute will show a header with the name inputted onto the component when the script is attached to a game object. Helps keep the script organized in the inspector.
    [Header("Stats:")]
    // Serialized field attribute will expose this variable to designers to manipulate even though it is a private variable.
    // Tool tip attribute will show a tool tip window, containing the message inputted, when the mouse is hovered over the varaible name in the inspector.
    [SerializeField, Tooltip("The speed of the player.")]
    float _movementSpeed = 1f;

   /// <summary>
   /// Moves the gameobject in the desired velocity with the set movement speed.
   /// </summary>
   /// <param name="__desiredVelocity">The desired direction to go.</param>
    public void Move(Vector2 __desiredVelocity)
    {
        if (__desiredVelocity.magnitude > 0.1f)
        {
            // Determine our new position based on the desired velocity times our movement speed and then adding our current position;
            // Use delta time so because this is called every frame and it makes it work in real time instead of frame time.
            var __moveToPosition = (__desiredVelocity * _movementSpeed * Time.deltaTime) + (Vector2)transform.position;

            // Set out position to be the move to position.
            transform.position = __moveToPosition;
        }
    }
}
