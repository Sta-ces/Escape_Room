using UnityEngine;

//[RequireComponent(typeof(FlashLight))]
public class DemoFlashLight : MonoBehaviour 
{
    [Tooltip("")]
    public  FlashLight flashLight;

    [Header("Quick Info")]
    public string howToUse= "Use Mouse Click Down to switch light";
    
    void Update () 
    {
        if (flashLight == null)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            flashLight.Switch();
        }
    }
}