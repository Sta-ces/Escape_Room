using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformActivation : MonoBehaviour {

    public GameObject m_objectToPutt;
    public bool m_actived = false;

    private void OnCollisionStay(Collision other) {
        if(other.gameObject == m_objectToPutt)
        {
            Debug.Log(gameObject.name+" actived");
            m_actived = true;
        }
    }
}
