using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour {

    #region Public Members
    public GameObject m_playerCharacter;
    public GameObject m_handHeldObj;
    public Inventory m_myInventory;
    public float m_maxDistanceInteraction = 2f;
    #endregion


    #region Public Void
    //les fonctions que cédric appellera avec ses controles
    public void Activate()
    {
        if (!m_actionStarted)
        {
            ItemInteractionStart();
        }
    }
    public void Deactivate()
    {
        ItemInteractionStop();
    }
    //-----
    #endregion


    #region System

    void Awake()
    {
        if(m_playerCharacter==null)
        {
            m_playerCharacter = this.gameObject;
        }
    }

    //temporaire jusqu'a ce qu'on ai géré les inputs, alors cédric utilisera ItemInteractionStart(); et ItemInteractionStop(); depuis ses fonctions
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Activate();
        }
        else
        {
            if(m_actionStarted)
            {
                Deactivate();
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
            Debug.Log(hit.transform.gameObject.name);
            if (hit.distance < m_maxDistanceInteraction)//if the object is close enough to be interacted with
            {
                if (hit.transform.gameObject.tag == "Movable")
                {
                    m_isPullingObject = true;
                    m_pulledObject = hit.transform.gameObject;
                    //il faudra un son ici pour quand on commence a tirer un objet autrement sans bouger on se rend pas compte qu'il est tiré
                    hit.transform.parent = m_playerCharacter.transform;
                }
                if (hit.transform.gameObject.tag == "Pickable")
                {
                    AddAndEditItemAsEquipement(hit.transform.gameObject);
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

    private void AddAndEditItemAsEquipement(GameObject obj)
    {

        obj.transform.position = m_handHeldObj.transform.position;
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        obj.transform.parent = m_handHeldObj.transform;
        
        if (obj.name.Contains("FireAxe"))//la rotation vas varier selon le type d'item, exemple, hache et clé ont rotation différentes...
        {
            obj.transform.localRotation = Quaternion.Euler(-180f, 0f, 0f);
        }
        m_myInventory.AddToInventoryAndEquip(obj,0,true);
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
