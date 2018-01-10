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
    public void UseActionKeyOnObject(GameObject EquippedItem = null)
    {
        if(gameObject.name.Contains("Chest"))
        {
            if(EquippedItem!=null)
            {
                if(EquippedItem.name.Contains("Key"))
                {
                    gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                    foreach (Collider col in gameObject.GetComponents<Collider>())
                    {
                        if (col.isTrigger)
                        {
                            StartCoroutine("TimerToOpenChest", col);
                        }
                    }
                }
                else
                {
                    //un son de lock
                }
            }
        }
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

    IEnumerator TimerToOpenChest(Collider col)//pour qu'on ne drop pas l'item a l'instant ou l'a ramasser, on met un timer^^
    {
        yield return new WaitForSeconds(2f);
        col.enabled = false;
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
