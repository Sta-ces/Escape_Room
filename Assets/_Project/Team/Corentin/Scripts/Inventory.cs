using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory  : MonoBehaviour
{

    #region Public Members

    #endregion


    #region Public Void
    
    public void AddToInventoryAndEquip(GameObject PickedItem, float charge = 0 , bool weapon= false)
    {
        StructUsableItem item = new StructUsableItem()
        {
            obj = PickedItem,
            charges = charge,
            isAWeapon = weapon
        };
        m_inventory.Add(item);
        if(m_inventory.Count>1)
        {
            m_inventory[m_posOfCurrentEquipedItem].obj.SetActive(false);
        }
        m_posOfCurrentEquipedItem = m_inventory.Count - 1;
    }
    public void SwitchToNextItemFromInventory()
    {
        if(m_inventory.Count>1)//si y'a plus d'une item
        {
            if(m_posOfCurrentEquipedItem + 1 < m_inventory.Count)//si l'item actuellement sélectionnée n'est pas la dernière de la liste, passe a suivante
            {
                m_inventory[m_posOfCurrentEquipedItem].obj.SetActive(false);
                m_posOfCurrentEquipedItem++;
                m_inventory[m_posOfCurrentEquipedItem].obj.SetActive(true);
            }
            else //si l'item actuellement sélectionnée est la dernière de la liste, passe la la 1ere
            {
                m_inventory[m_posOfCurrentEquipedItem].obj.SetActive(false);
                m_posOfCurrentEquipedItem=0;
                m_inventory[m_posOfCurrentEquipedItem].obj.SetActive(true);
            }
        }
    }
    public void DropCurrentItem()
    {
        //not done yet
    }
    public void DestroyCurrentItem()/*for useableItems*/
    {
        //not done yet
    }
    #endregion


    #region System


    #endregion

    #region Private Void

    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members
    private int m_posOfCurrentEquipedItem;
    private List<StructUsableItem> m_inventory = new List<StructUsableItem>();
    #endregion

}
