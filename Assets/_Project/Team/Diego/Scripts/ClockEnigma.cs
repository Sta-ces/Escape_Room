using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockEnigma : MonoBehaviour {
    [SerializeField]
    private GameObject m_anchorNeedles;
    [SerializeField]
    private GameObject m_hourTime;
    [SerializeField]
    private GameObject m_minuteTime;
    [SerializeField]
    private GameObject m_door;
    [SerializeField]
    private int codeHours = 3;
    private int codeMinutes = 45;
    private Vector3 originHourPos;

    [Header("Debug (Don't touch)")]
    public float m_hourAngles;
    public float m_minuteAngles;


    private const float
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f,
        secondsToDegrees = 360f / 60f;
    // Use this for initialization
    void Start () {
        originHourPos = m_hourTime.transform.position ;
	}

    private void OnCollisionEnter(Collision collision)
    {
        int test;
        if (collision.transform.CompareTag("clock_enigma") && m_hourTime.transform.CompareTag("clock_hour")) {

            //compare le chiffre des heures données
            //valeur à trouver 4
            
            
            
        }
        if (collision.transform.CompareTag("clock_enigma") && m_hourTime.transform.CompareTag("clock_minute"))
        {
            //Quaternion.sig
            //compare le chiffre des minute données
            //valeur à trouver 38
        }
        

    }



    // Update is called once per frame
    void Update () {
       Debug.Log (TestCode());
       // MoveMinute(1.2f);
        //DragMinute();
        m_hourAngles = (RotationAngle(m_hourTime.transform));
      //  m_minuteAngles = (RotationAngle(m_minuteTime.transform));
    }



    private void MoveHour(float vitesse)
    {
       // Debug.Log(Time.fixedDeltaTime * 29999);
      
        m_hourTime.transform.RotateAround(m_anchorNeedles.transform.position, Vector3.forward, vitesse );

    }

    private void MoveMinute(float vitesse)
    {
        m_minuteTime.transform.RotateAround(m_anchorNeedles.transform.position, Vector3.forward,vitesse );

    }

    private float RotationAngle(Transform needle )
    {

        Vector3 rotationRef = m_anchorNeedles.transform.up;
        Vector3 axeRotation = m_anchorNeedles.transform.forward;
        Vector3 hoursRef = needle.transform.up;
        
        float angle = Mathf.Abs( -180 + Vector3.SignedAngle(rotationRef, hoursRef, axeRotation));
        //Debug.Log(">[ClockEnigma], Angle:" + angle);

        return angle;
    }

    private bool TestCode() {
        float rotHoursToNum = RotationAngle(m_hourTime.transform) / 30f;
        float rotMinutesToNum = RotationAngle(m_minuteTime.transform) / 6f;
        Debug.Log("Hours rot:" + Mathf.Round(rotHoursToNum) + "  Hours code:" + codeHours + "");
        Debug.Log("Minutes rot:" + Mathf.Round(rotMinutesToNum) + "  Minutes code:" + codeHours + "");
        if (Mathf.Round(rotHoursToNum)== codeHours && Mathf.Round(rotMinutesToNum) == codeMinutes) {
            return true;
        }
        return false;



    }

    private void OpenClock() {
        //test code degré
        //1
        GetComponent<Animator>().SetBool("Open", true);
        
        //afficher animation

    }
    

}
