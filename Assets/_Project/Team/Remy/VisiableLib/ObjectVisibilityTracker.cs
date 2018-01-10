using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectVisibilityTracker : DualBehaviour 
{
    public Transform _target;
    public Camera [] _targetTrackers;

    private bool _isVisible;

    public UnityEvent _onObjectVisible;
    public UnityEvent _onObjectHide;

    public bool IsVisible
    {
        get { return _isVisible; }
        set {
            bool oldValue= _isVisible;
            bool newValue= value;

            if (newValue != oldValue)
            {
                //DO SOMETHING
                if (newValue)
                    _onObjectVisible.Invoke();
                else
                    _onObjectHide.Invoke();

            }

            _isVisible = value; }
    }

    public void Awake()
    {

        ///BAD CODE: DOUBLONS
        if (_target == null || _targetTrackers.Length == 0)
        {
            Debug.LogWarning("Should not be null !", this.gameObject);
            return;

        }
        if (VisibleObject.IsVisible(_targetTrackers, _target))
            _onObjectVisible.Invoke();
        else
            _onObjectHide.Invoke();
    }

    public void Update()
    {
        CheckIfVisible();

    }

    private void CheckIfVisible()
    {
        ///BAD CODE: DOUBLONS
        if (_target == null || _targetTrackers.Length == 0)
        {
            Debug.LogWarning("Should not be null !", this.gameObject);
            return;

        }

        IsVisible = VisibleObject.IsVisible(_targetTrackers, _target);
    }
}
