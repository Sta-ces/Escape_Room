using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject  : MonoBehaviour
{

    #region Public Members
    public bool m_isDestructible;
    public int m_maxHitPoints = 5;
    public int m_currentHitPoints = 5;
    #endregion


    #region Public Void
    public void UseActionKeyOnObject()
    {
        //Debug.Log("Object " + this.gameObject.name + " is used");
        //code some effect here...
    }
    public void HittingObject(int damage)
    {
        if(m_isDestructible)
        {
            m_currentHitPoints-=damage;
        }
        if(m_currentHitPoints<1)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion


    #region System

    #endregion

    #region Private Void

    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    
    #endregion

}
