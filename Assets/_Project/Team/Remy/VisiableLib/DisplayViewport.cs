using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayViewport : DualBehaviour 
{

    public Transform _target;
    public Camera _camera;
    [Header("Debug")]
    public Vector3 _viewPort;

    public void Update()
    {
        _viewPort = _camera.WorldToViewportPoint(_target.position);
    }

}
