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
    public float m_mouseSensibility = 100.0f;
    public float m_minCameraAngle = -50f;
    public float m_maxCameraAngle = 50f;

    void Awake()
    {
        // Get the character controller
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Start()
    {
        // Disappear the mouse on play
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
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

        // Move Player
        m_moveVector.x = m_player.GetAxis("MoveHorizontal"); // get input by name or action id
        m_moveVector.z = m_player.GetAxis("MoveVertical");
        // Move Camera
        m_cameraVector.x = Mathf.Clamp(m_player.GetAxis("CameraHorizontal"),-1,1);
        m_cameraVector.y = Mathf.Clamp(m_player.GetAxis("CameraVertical"),-1,1);

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

        if (Input.GetButtonDown("Jump"))
        {
            Cursor.visible = (!Cursor.visible);
        }
    }

    private void ProcessInput()
    {
        // Move Player
        m_rigidbody.transform.Translate(m_moveVector * m_moveSpeed * Time.deltaTime);

        // Move Camera        
        // HORIZONTAL
        float angleHorizontalOfCamera = m_cameraVector.x * m_mouseSensibility * Time.deltaTime;
        m_rigidbody.transform.Rotate(0,angleHorizontalOfCamera,0,Space.World); // Horizontal

        // VERTICAL
        float angleVerticalOfCamera = m_cameraVector.y * m_mouseSensibility * Time.deltaTime;
        m_cameraRotationState.x += angleVerticalOfCamera;
        if(m_cameraRotationState.x < m_minCameraAngle)
        {
            m_cameraRotationState.x = m_minCameraAngle;
        }
        if(m_cameraRotationState.x > m_maxCameraAngle)
        {
            m_cameraRotationState.x = m_maxCameraAngle;
        }
        m_cameraPlayer.transform.localEulerAngles = m_cameraRotationState;
    }

    private Player m_player; // The Rewired Player
    private Rigidbody m_rigidbody;
    private Vector3 m_moveVector;
    private Vector3 m_cameraVector;
    private Vector3 m_cameraRotationState;
    [SerializeField]
    private UseItem m_useItem;
    [SerializeField]
    private FlashLight m_useLight;
}