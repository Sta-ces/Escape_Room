using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmeCedric : MonoBehaviour {

    public List<GameObject> m_listPlateform;
    public GameObject m_doorToOpen;

    private void Update()
    {
        if (CheckPlateforme())
        {
            if(!m_success)
                OpenDoor();
        }
    }

    private bool CheckPlateforme()
    {
        bool result = false;
        int numberPlateformActived = 0;
        foreach(GameObject plateform in m_listPlateform)
        {
            plateformActivation script = plateform.GetComponent<plateformActivation>();
            bool isActived = script.m_actived;
            if(isActived)
                numberPlateformActived++;
        }

        if (m_listPlateform.Count == numberPlateformActived)
            result = true;

        return result;
    }

    private void OpenDoor()
    {
        m_doorToOpen.transform.Translate(new Vector3(0,15,0));
        m_success = true;
    }

    private bool m_success = false;
}
