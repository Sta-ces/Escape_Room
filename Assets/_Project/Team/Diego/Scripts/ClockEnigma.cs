using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockEnigma : MonoBehaviour {
    [SerializeField]
    private GameObject m_hourTime;
    [SerializeField]
    private GameObject m_minuteTime;
    [SerializeField]
    private GameObject m_door;
    [SerializeField]
    private GameObject m_anchor;

    private const float
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f,
        secondsToDegrees = 360f / 60f;
    // Use this for initialization
    void Start () {
        
	}

    private void OnCollisionEnter(Collision collision)
    {
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
        DragHour();
        DragMinute();
    }



    private void DragHour()
    {
       // Debug.Log(Time.fixedDeltaTime * 29999);
      
        m_hourTime.transform.RotateAround(m_anchor.transform.position, Vector3.up, 0.6f );

    }

    private void DragMinute()
    {
        m_minuteTime.transform.RotateAround(m_anchor.transform.position, Vector3.up, 0.1f );

    }





}
