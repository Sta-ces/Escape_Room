using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMoveLinkedCam : MonoBehaviour 
{

    public KeyCode _keyToMoveCamera;
    public Camera _linkedCamera;


    public void Update() {

        if (Input.GetKey(_keyToMoveCamera)) {
            _linkedCamera.transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        }
    }

}
