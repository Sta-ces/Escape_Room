using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(Rigidbody))]
public class movementPlayer : MonoBehaviour
{
    public enum e_Player
    {
        Player1,
        Player2
    }

    public e_Player m_playerName = e_Player.Player1; // The Rewired player id of this character

    public float m_moveSpeed = 3.0f;

    private Player m_player; // The Rewired Player
    private Rigidbody m_rigidbody;
    private Vector3 m_moveVector;

    void Awake()
    {
        // Get the character controller
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetPlayers();
        GetInput();
        ProcessInput();
    }

    private void GetPlayers()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        switch (m_playerName) {
            case e_Player.Player1:
                m_player = ReInput.players.GetPlayer("Player 1");
                break;
            case e_Player.Player2:
                m_player = ReInput.players.GetPlayer("Player 2");
                break;
        }
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        m_moveVector.x = m_player.GetAxis("MoveHorizontal"); // get input by name or action id
        m_moveVector.z = m_player.GetAxis("MoveVertical");
    }

    private void ProcessInput()
    {
        // Process movement
        m_rigidbody.transform.Translate(m_moveVector * m_moveSpeed * Time.deltaTime);

        if (m_player.GetButtonDown("Use"))
        {
            Debug.Log("Use");
        }
    }
}
