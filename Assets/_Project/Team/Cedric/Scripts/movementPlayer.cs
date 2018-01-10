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

    public Camera m_cameraPlayer;

    public float m_moveSpeed = 3.0f;
    public float m_cameraSpeed = 100.0f;
    public float m_minCameraAngle = -50f;
    public float m_maxCameraAngle = 50f;

    void Awake()
    {
        // Get the character controller
        m_rigidbody = GetComponent<Rigidbody>();

        // Instance UseItem class
        m_useItem = new UseItem();

        // Instance FlashLight class
        m_useLight = new FlashLight();
    }

    void Start()
    {
        // Disappear the mouse on play
        Cursor.visible = false;
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
        switch (m_playerName)
        {
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
        m_cameraVector.y = m_player.GetAxis("CameraHorizontal");
        m_cameraVector.x += m_player.GetAxis("CameraVertical") * m_cameraSpeed * Time.deltaTime;

        if (m_player.GetButtonDown("Use"))
        {
            Debug.Log("Use : movementPlayer.cs/66");
            m_useItem.Activate();
        }

        if (m_player.GetButtonDown("UseLight"))
        {
            Debug.Log("Use Light : movementPlayer.cs/72");
            m_useLight.Switch();
        }
    }

    private void ProcessInput()
    {
        // Process movement
        m_rigidbody.transform.Translate(m_moveVector * m_moveSpeed * Time.deltaTime);
        m_rigidbody.transform.Rotate(0, m_cameraVector.y * m_cameraSpeed * Time.deltaTime, 0, Space.World);
        float vectX = m_cameraVector.x;
        vectX = Mathf.Clamp(vectX, m_minCameraAngle, m_maxCameraAngle);
        m_cameraPlayer.transform.localEulerAngles = new Vector3(vectX, 0,0);
    }

    private Player m_player; // The Rewired Player
    private Rigidbody m_rigidbody;
    private Vector3 m_moveVector;
    private Vector3 m_cameraVector;
    private UseItem m_useItem;
    private FlashLight m_useLight;
}
