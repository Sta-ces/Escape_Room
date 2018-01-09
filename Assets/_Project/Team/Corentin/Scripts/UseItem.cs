using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour {

    #region Public Members
    public GameObject m_playerCharacter;
    #endregion


    #region Public Void
    public void ItemInteractionStart()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
            Debug.DrawLine(ray.origin, hit.point);
    }

    public void ItemInteractionStop()
    {

    }

    #endregion


    #region System
    /*
    void Start()
    {
    }
    void Awake()
    {
    }

    void Update()
    {
    }
    */
    #endregion

    #region Private Void

    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members

    #endregion
}
