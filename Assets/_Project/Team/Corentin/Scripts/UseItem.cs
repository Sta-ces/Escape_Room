using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour {

    #region Public Members
    public GameObject m_playerCharacter;
    public GameObject m_handHeldObj;
    public Inventory m_myInventory;
    public Camera m_mycamera;
    public float m_maxDistanceInteraction = 2f;
    public float m_timeBetweenInventoryItemSwitch = 0.3f;
    public float m_timeToKeepButtonPushedToLiftItem = 0.45f;
    public float m_timeBeforeCanDropItemJustPicked = 2f;
    public float m_timeBeforeCanDropItemEquipped = 0.5f;
    public float m_durationOfAttack = 1f;
    #endregion


    #region Public Void
    //les fonctions que cédric appellera avec ses controles
    public void Activate()
    {
        if(!m_isAttacking)//s'il n'est pas en train d'attaquer
        {
            if (m_isPullingObject)//s'il est en train de tirer un block
            {
                if (m_canDrop == true)
                {
                    ItemInteractionStop();
                }
            }
            else
            {
                ItemInteractionStart();
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
    public void Attack()
    {
        HandleAttack();
    }
    public void DropEquippedItem()
    {
        if (m_myInventory.CheckIfItemIsEquipped() && m_canDropEquipped == true)//vérifie qu'il y a au moins 1 item équipée
        {
            GameObject objToDrop = m_myInventory.GetCurrentlyEquippedItem().obj;
            objToDrop.transform.parent = null;
            m_myInventory.RemoveCurrentItem();
            objToDrop.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            StartCoroutine("TimerAfterDropEquippedItem");
            m_canDropEquipped = false;
        }
    }
    //--------------------------------------------------------

    IEnumerator TimerSwitchW()
    {
        yield return new WaitForSeconds(m_timeBetweenInventoryItemSwitch);
        m_onTimerSwitchWeapon = false;
    }
    IEnumerator TimerLiftItem()
    {
        yield return new WaitForSeconds(m_timeToKeepButtonPushedToLiftItem);
        Debug.Log(m_timerLift);
        if (m_timerLift> m_timeToKeepButtonPushedToLiftItem)//s'il a laisser appuyer 4/10eme de sec
        {
            m_canLift = true;
        }
        m_timerLift = 0;
    }
    IEnumerator TimerAfterLiftItem()//pour qu'on ne drop pas l'item a l'instant ou l'a ramasser, on met un timer^^
    {
        yield return new WaitForSeconds(m_timeBeforeCanDropItemJustPicked);
        m_canDrop = true;
    }

    IEnumerator TimerAttackDuration(Animator WeaponAnimator)
    {
        m_isAttacking = true;
        WeaponAnimator.SetBool("StartAnim", true);
        yield return new WaitForSeconds(m_durationOfAttack);
        AttackHit();
        WeaponAnimator.SetBool("StartAnim", false);
        m_isAttacking = false;
    }

    IEnumerator TimerAfterDropEquippedItem()//pour qu'on ne drop pas l'item a l'instant ou l'a ramasser, on met un timer^^
    {
        yield return new WaitForSeconds(m_timeBeforeCanDropItemEquipped);
        m_canDropEquipped = true;
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

    //temporaire jusqu'a ce qu'on ai géré les inputs, alors cédric utilisera activate(), ActivateSwitchWeaponKey() , et attack() depuis ses fonctions
    void Update()
    {

        if (Input.GetButton("Fire1"))//gachette tir
        {
            Attack();
        }
        if (Input.GetButton("Fire2"))//bouton utiliser
        {
            Activate();
        }
        if(Input.GetButton("Fire3"))//bouton change d'arme
        {
            ActivateSwitchWeaponKey();
        }
        if(Input.GetButton("Submit"))
        {
            DropEquippedItem();
        }

    }
    //----------------------------------------------------------
    #endregion

    #region Private Void

    private void ItemInteractionStart()
    {
        Ray ray = m_mycamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit , 100f))
        {
            if (hit.distance < m_maxDistanceInteraction)//if the object is close enough to be interacted with
            {
                if (hit.transform.gameObject.tag == "Movable")
                {
                    if (m_timerLift == 0)
                    {
                        StartCoroutine("TimerLiftItem");//lance un compteur après 1ere fois bouton appuyer qd on vise objet movable
                                                        
                    }
                    m_timerLift += Time.deltaTime;//vas compter le temps ou le bouton est appuyer pendant qu'on vise objet movable, si on a laisser appuyer 4/10sec canlift=true (voir coroutine)
                    if (m_canLift)
                    {
                        Debug.Log("lifting");
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

    private void HandleAttack()
    {
        if(!m_isAttacking && ! m_isPullingObject) //ne peut pas lancer d'attaque si attaque en cours ou objet porté
        {
            if(!m_myInventory.GetIsInventoryEmpty())
            {
                if (m_myInventory.GetCurrentlyEquippedItem().isAWeapon)
                {
                    Animator weaponAnimator = m_myInventory.GetCurrentlyEquippedItem().obj.GetComponent<Animator>();
                    StartCoroutine("TimerAttackDuration", weaponAnimator);
                }
            }
        }
    }
    private void AttackHit()
    {
        Ray ray = m_mycamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.distance < m_maxDistanceInteraction)//if the object is close enough to be interacted with
            {
                if (hit.transform.gameObject.GetComponent<InteractableObject>())
                {
                    hit.transform.gameObject.GetComponent<InteractableObject>().HittingObject(2);
                }
            }
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
            m_myInventory.AddToInventoryAndEquip(obj, 0, true); //objet , nombre charges , isaweapon
        }
        if (obj.name.Contains("Key"))//la rotation vas varier selon le type d'item, exemple, hache et clé ont rotation différentes...
        {
            obj.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            m_myInventory.AddToInventoryAndEquip(obj, 0, false); //objet , nombre charges , isaweapon
        }
    }
    #endregion

    #region Tools Debug And Utility


    #endregion


    #region Private And Protected Members
    private bool m_onTimerSwitchWeapon;
    private bool m_canLift;
    private bool m_canDrop;
    private float m_timerLift;
    private bool m_isAttacking;
    private bool m_canDropEquipped=true;


    private bool m_isPullingObject;
    private GameObject m_pulledObject;
    #endregion
}
