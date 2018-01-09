using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour {

    #region Public Members
    public GameObject m_playerCharacter;
    public float m_maxDistanceInteraction = 1.3f;
    #endregion


    #region Public Void
    //les fonctions que cédric appellera avec ses controles
    public void ButtonIsPressed()
    {
        if (!m_actionStarted)
        {
            ItemInteractionStart();
        }
    }
    public void ButtonIsUnPressed()
    {
        ItemInteractionStop();
    }
    //-----
    #endregion


    #region System
    /*
    void Start()
    {
    }
    void Awake()
    {
    }*/

    //temporaire jusqu'a ce qu'on ai géré les inputs, alors cédric utilisera ItemInteractionStart(); et ItemInteractionStop(); depuis ses fonctions
    void Update()
    {
        
        if(Input.GetButton("Fire1"))
        {
            ButtonIsPressed();
        }
        else
        {
            if(m_actionStarted)
            {
                ButtonIsUnPressed();
            }
        }
    }
    //----------------------------------------------------------
    #endregion

    #region Private Void

    private void ItemInteractionStart()
    {
        m_actionStarted = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.gameObject.tag);
            if (hit.distance < m_maxDistanceInteraction)//if the object is close enough to be interacted with
            {
                if (hit.transform.gameObject.tag == "Movable")
                {
                    m_isPullingObject = true;
                    m_pulledObject = hit.transform.gameObject;
                    //il faudra un son ici pour quand on commence a tirer un objet autrement sans bouger on s'ne rend pas compte
                    hit.transform.parent = m_playerCharacter.transform;
                }
                if (hit.transform.gameObject.tag == "Pickable")
                {

                }
                if (hit.transform.gameObject.tag == "Switch")
                {
                    hit.transform.gameObject.GetComponent<InteractableObject>().UseActionKeyOnObject();
                }
            }
        }
    }

    private void ItemInteractionStop()
    {
        m_actionStarted = false;
        if(m_isPullingObject)
        {
            m_pulledObject.transform.parent = null;
        }
    }

    #endregion

    #region Tools Debug And Utility


    #endregion


    #region Private And Protected Members
    private bool m_actionStarted;
    private bool m_isPullingObject;
    private GameObject m_pulledObject;
    #endregion
}
