using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(Rigidbody))]
public class movementPlayer : MonoBehaviour
{

    public string playerName = "Player 1"; // The Rewired player id of this character

    public float moveSpeed = 3.0f;

    private Player player; // The Rewired Player
    private Rigidbody cc;
    private Vector3 moveVector;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerName);

        // Get the character controller
        cc = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("MoveHorizontal"); // get input by name or action id
        moveVector.z = player.GetAxis("MoveVertical");
    }

    private void ProcessInput()
    {
        // Process movement
        cc.transform.Translate(moveVector * moveSpeed * Time.deltaTime);

        /*if (player.GetAnyButtonDown("Use"))
        {
            Debug.Log("Use");
        }*/
    }
}
