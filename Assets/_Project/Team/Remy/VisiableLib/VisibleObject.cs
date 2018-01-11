using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleObject
{
    public static bool IsVisible (Camera cameraPlayer, Vector3 target)
    {
       return IsVisible(new Camera[] { cameraPlayer }, target);
    }

    public static bool IsVisible(Camera cameraPlayer, Transform target)
    {
        return IsVisible(cameraPlayer, target.position);
    }

    public static bool IsVisible(Camera cameraPlayer, GameObject target)
    {
        return IsVisible(cameraPlayer, target.transform.position);
    }
    public static bool IsVisible(Camera[] cameraPlayer, Transform target)
    {
        return IsVisible(cameraPlayer, target.position);
    }

    public static bool IsVisible(Camera[] cameraPlayer, GameObject target)
    {
        return IsVisible(cameraPlayer, target.transform.position);
    }

    public static bool IsVisible(Camera[] cameraPlayers, Vector3 target)
    {

        for (int i = 0; i < cameraPlayers.Length; i++)
        {
            Vector3 viewPos = cameraPlayers[i].WorldToViewportPoint(target);
            if (IsVisibleInTheViewPort(viewPos))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsVisibleInTheViewPort(Vector3 viewportValue) {
        return viewportValue.x > 0 && viewportValue.x < 1 && viewportValue.y > 0 && viewportValue.y < 1 && viewportValue.z > 0;
    }

}
