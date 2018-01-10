    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    [Header("Main Cam Attribute")]
    [SerializeField]
    private bool m_withUpView;
    [SerializeField]
    private bool m_withFaceView;
    [SerializeField]
    private float m_CamHeight;
    [SerializeField]
    private float m_CamRecoil;
    [SerializeField]
    private float m_CamYAngle;
    [SerializeField]
    private float m_CamXAngle;
    [SerializeField]
    private float m_CamZAngle;
    [Header("Players attribute")]
    [SerializeField]
    private GameObject m_mainCam;
    [SerializeField]
    private GameObject m_player1;
    [SerializeField]
    private GameObject m_player2;
    [SerializeField]
    private float angleP1 ;
    [SerializeField]
    private float angleP2 ;

    
   
    [SerializeField]
    private float m_maxdistance = 15f;
    private Camera _player1Cam;
    private Camera _player2Cam;
    private Camera _main_cam;
    private Transform _mainCamTr;
    private Transform _player1Tr;
    private Transform _player2Tr;
    private bool testChangeCam=true;

    private void Start()
    {
        InitiateCamera();
        if (m_withFaceView) { CamNormal(); };
        
    }
    // Update is called once per frame
    void Update() {

        /*  if (CalculateDistance() && m_withFaceView)
          {
             // if (m_withFaceView) { CamNormal(); }
          }
          else
          {
              //Debug.Log("split");
              CamSplit();
          };*/
        CamSplit();
    }

  
    private void CamSplit()
    {
        
       // _player1Cam.transform.LookAt(_player1Tr);
      //  _player2Cam.transform.LookAt(_player2Tr);
        _player1Cam.enabled = true;
        _player2Cam.enabled = true;
        _main_cam.enabled = false;
        testChangeCam = true;
    }

    private void CamPlayerLookNormal()
    {
        CenterView(_player1Tr, _player2Tr);
        Quaternion playerViews = _player1Cam.transform.rotation * Quaternion.Inverse(_player2Cam.transform.rotation);
        //float angle = Vector3.SignedAngle(_mainCamTr.position, playerViews , DistanceBetweenPlayer() / 2);
        RotateCam(_mainCamTr, playerViews);
        _player1Cam.enabled = false;
        _player2Cam.enabled = false;
        _main_cam.enabled = true;
        //Debug.Log("normal"); 
    }
    private void CamNormal()
    {
        CenterView(_player1Tr, _player2Tr);
       // Quaternion playerViews = _player1Cam.transform.rotation * Quaternion.Inverse(_player2Cam.transform.rotation);
        //float angle = Vector3.SignedAngle(_mainCamTr.position, playerViews , DistanceBetweenPlayer() / 2);
       // RotateCam(_mainCamTr, playerViews);
        _player1Cam.enabled = false;
        _player2Cam.enabled = false;
        _main_cam.enabled = true;
       // Debug.Log("normal");
    }


    private void CenterView(Transform t1, Transform t2)
    {
        angleP1 = Vector3.Angle(_player1Cam.transform.position, Vector3.up);
        angleP2 = Vector3.Angle(_player1Cam.transform.position, Vector3.up);
        Vector3 camDist = t1.position-(t1.position - t2.position) / 2;
        Vector3 withObj;
        withObj = new Vector3(camDist.x,m_CamHeight, camDist.z+m_CamRecoil);
        _mainCamTr.position = withObj;
        Quaternion angleCameraPos=  
            Quaternion.AngleAxis(m_CamYAngle, Vector3.right)
            * (Quaternion.AngleAxis(m_CamXAngle, Vector3.up))
            * (Quaternion.AngleAxis(m_CamZAngle, Vector3.forward));
        _mainCamTr.rotation = angleCameraPos;
        t1 = null;
        t2 = null;
    }



    private Vector3 DistanceBetweenPlayer()
    {
        return _player1Tr.position - _player2Tr.position;
    }

    private void RotateCam(Transform objToRotate,Quaternion rot) {
        // Debug.Log(angle + "-----" + objToRotate.position);
        // objToRotate.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        objToRotate.rotation = rot;
    }
    private bool CalculateDistance()
    {
        
        float maxRange = m_maxdistance;
        bool result = false;
        Vector3 distanceVect = DistanceBetweenPlayer();
        Debug.DrawRay(_player2Tr.position,distanceVect, Color.green);
        result = Physics.Raycast(_player2Tr.position, (_player1Tr.position - _player2Tr.position), maxRange);
        if (result && testChangeCam && m_withUpView)
        {
            testChangeCam = false;
            CamNormal();
            return true;
        }
        if(m_withFaceView && result && ! m_withUpView && PlayerLookSameDirection()) {
            CamPlayerLookNormal();
            return true;
        }
        return false; 
    }
    private bool PlayerLookSameDirection()
    {
        /* float angleP1 = Vector3.SignedAngle(Vector3.zero,_player1Cam.transform.position, Vector3.left);
         float angleP2 = Vector3.SignedAngle(DistanceBetweenPlayer() / 2, Vector3.zero, Vector3.up);*/
        float angleP1 = Quaternion.Angle(_player1Cam.transform.rotation, Quaternion.identity);
        float angleP2 = Quaternion.Angle( _player2Cam.transform.rotation, Quaternion.identity);
        Debug.Log(angleP1 + "=" + angleP2);
        if (angleP1 == angleP2)
        { return true;
    }

        return false;
    }
    private void InitiateCamera()
    {
        _player1Tr = m_player1.GetComponent<Transform>();
        _player2Tr = m_player2.GetComponent<Transform>();
        _player1Cam = m_player1.GetComponentInChildren<Camera>();
        _player2Cam = m_player2.GetComponentInChildren<Camera>();
        _main_cam = m_mainCam.GetComponent<Camera>();
        _mainCamTr = m_mainCam.transform;
        _mainCamTr.rotation = Quaternion.AngleAxis(0f, Vector3.down);
        _player1Tr.rotation = Quaternion.AngleAxis(0f, Vector3.up);
        _player2Tr.rotation = Quaternion.AngleAxis(0f, Vector3.up);


        //_mainCamTr.position = new Vector3(0f,0f,0f);
        //Demander pourquoi cela bug ???
        //_player1Cam.rect = new Rect(0, 0, 0.5f, 1f);
        //_player2Cam.rect = new Rect(0.5f, 0, 0.5f, 0);
        //_main_cam.rect = new Rect(0, 0, 1f, 1f);
        Debug.Log(_player2Cam.name);
    }

}
