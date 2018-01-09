using UnityEngine;

public class FlashLight : MonoBehaviour
{
    #region Public Methods

    public void SetLightState(bool lightON)
    {
        lightEmitter.enabled = lightON;
        lightSource.SetActive(lightON);
        soundEmitter.clip = lightON ? soundON : soundOFF;
        soundEmitter.Play();
    }

    public void Switch()
    {
        SetLightState(!IsLightON());
    }

    public bool IsLightON()
    {
        return lightEmitter.enabled;
    }

    #endregion

    #region Private and Protected Members

    [Header("Params")]

    [SerializeField]
    private Light lightEmitter;

    [SerializeField]
    private AudioSource soundEmitter;

    [SerializeField]
    [Tooltip("Sound play when light switch on")]
    private AudioClip soundON;

    [SerializeField]
    [Tooltip("Sound play when light switch off")]
    private AudioClip soundOFF;

    [SerializeField]
    private GameObject lightSource;

    #endregion
}
