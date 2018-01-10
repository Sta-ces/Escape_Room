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
    public void ActivateUseKey()
    {
        if (!m_isPullingObject)//s'il n'est pas en train de tirer un block
        {
            if(m_timerLift==0)
            {
                StartCoroutine("TimerLiftItem");//lance un compteur après 1ere fois bouton appuyer
                //vas compter le temps ou le bouton est appuyer
            }
            m_timerLift += Time.deltaTime;
            ItemInteractionStart();
        }
        else
        {
            if (m_canDrop == true)
            {
                ItemInteractionStop();
            }
        }
    }
    public void ActivateSwitchWeaponKey()
    {
        if(!m_onTimerSwitchWeapon)//s'il n'est pas en train de switch d'un objet en main a un autre
        {
            m_onTimerSwitchWeapon = true;
            m_myInventory.SwitchToNextItemFromInventory();
            StartCoroutine("TimerSwitchW");
        }
    }
    //--------------------------------------------------------

    IEnumerator TimerSwitchW()
    {
        yield return new WaitForSeconds(0.3f);
        m_onTimerSwitchWeapon = false;
    }
    IEnumerator TimerLiftItem()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log(m_timerLift);
        if (m_timerLift>0.45f)//s'il a laisser appuyer 4/10eme de sec
        {
            m_canLift = true;
            m_timerLift = 0;
        }
    }
    IEnumerator TimerAfterLiftItem()//pour qu'on ne drop pas l'item a l'instant ou l'a ramasser, on met un timer^^
    {
        yield return new WaitForSeconds(2f);
        m_canDrop = true;
    }

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
            ActivateUseKey();
        }
        if(Input.GetButton("Fire2"))
        {
            ActivateSwitchWeaponKey();
        }
    }
    //----------------------------------------------------------
    #endregion

    #region Private Void

    private void ItemInteractionStart()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit , 100f))
        {
            //Debug.Log(hit.transform.gameObject.name);
            if (hit.distance < m_maxDistanceInteraction)//if the object is close enough to be interacted with
            {
                if (hit.transform.gameObject.tag == "Movable")
                {
                    if(m_canLift)
                    {
                        Debug.Log("lift");
                        m_pulledObject = hit.transform.gameObject;
                        //il faudra un son ici pour quand on commence a tirer un objet autrement sans bouger on se rend pas compte qu'il est tiré
                        hit.transform.parent = m_playerCharacter.transform;
                        m_isPullingObject = true;
                        m_canLift = false;
                        m_canDrop = false;
                        StartCoroutine("TimerAfterLiftItem");//pour qu'on ne drop pas l'item a l'instant ou l'a ramasser, on met un timer^^
                    }
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
        m_pulledObject.transform.parent = null;
        m_isPullingObject = false;
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
        if (obj.name.Contains("Key"))//la rotation vas varier selon le type d'item, exemple, hache et clé ont rotation différentes...
        {
            obj.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        }
        m_myInventory.AddToInventoryAndEquip(obj,0,true);
    }
    #endregion

    #region Tools Debug And Utility


    #endregion


    #region Private And Protected Members
    private bool m_onTimerSwitchWeapon;
    private bool m_canLift;
    private bool m_canDrop;
    private float m_timerLift;

    
    private bool m_isPullingObject;
    private GameObject m_pulledObject;
    #endregion
}
