    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    [SerializeField]
    private Camera m_player1_cam;
    [SerializeField]
    private Camera m_player2_cam;
    [SerializeField]
    private Camera m_main_cam;
    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private GameObject normalView;
    private Transform _mainCamTr;
    private void Start()
    {
        _mainCamTr = m_main_cam.transform;
        _mainCamTr.rotation = Quaternion.AngleAxis(0f, Vector3.up);
        m_player1_cam.transform.rotation = Quaternion.AngleAxis(0f, Vector3.up);
        m_player2_cam.transform.rotation = Quaternion.AngleAxis(0f, Vector3.up);
        CamNormal();
    }
    // Update is called once per frame
    void Update() {
        if (CalculateDistance())
        {
            CamNormal();

        }
        else {

            CamSplit();
        };
    }

    private void CamSplit()
    {
        m_main_cam.enabled = false;
        m_player1_cam.rect = new Rect(0, 0, 0.5f, 1f);
        m_player2_cam.rect = new Rect(0.5f, 0, 0, 0);
        
        m_player1_cam.enabled = true;
        m_player2_cam.enabled = true;
    }

    private void CamNormal() {

        
        Vector3 camDist = (player1.position - player2.position)/2;
        
        _mainCamTr.position = player1.position - camDist;
        float angle = Vector3.Angle( _mainCamTr.position, Vector3.up);
        
        Debug.Log(angle);
        _mainCamTr.rotation = Quaternion.AngleAxis(0f, _mainCamTr.position);
        m_player1_cam.enabled = false;
        m_player2_cam.enabled = false;
        m_main_cam.enabled = true;
        m_main_cam.rect = new Rect(0, 0, 1f, 1f);

    }

    private bool CalculateDistance()
    {

        Debug.DrawRay(player2.position, player1.position - player2.position, Color.green);



        float maxRange = 15;
        RaycastHit hit;
        if (Vector3.Distance(player2.position, player1.position) < maxRange)
        {
            if (Physics.Raycast(player2.position, (player1.position - player2.position), out hit, maxRange))
            {
                if (hit.transform == player1)
                {
                    // Debug.Log(hit.distance);

                    return true;
                }
            }
        }

        return false;


    }

}
