using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory  : MonoBehaviour
{

    #region Public Members

    #endregion


    #region Public Void
    public void AddToInventory(GameObject PickedItem)
    {
        m_inventory.Add(PickedItem);
    }
    public void AddToInventoryAndEquip(GameObject PickedItem)
    {
        m_inventory.Add(PickedItem);
        m_currentItem = PickedItem;
    }
    public void SwitchToNextItemFromInventory()
    {

    }
    public void DropCurrentItem()
    {

    }
    public void DestroyCurrentItem()/*for useableItems*/
    {

    }
    #endregion


    #region System


    #endregion

    #region Private Void

    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private List<GameObject> m_inventory = new List<GameObject>();
    private GameObject m_currentItem;
    private int m_posOfCurrentItemInInventory;
    #endregion

}
