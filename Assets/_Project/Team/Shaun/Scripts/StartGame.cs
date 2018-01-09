using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    #region Public Members

    GameObject SceneLoader;
    bool _load;

    #endregion

    #region Public void

    #endregion

    #region System

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync("test", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("UIStart");
        DontDestroyOnLoad(SceneLoader);
        
    }

    public void LoadTrue()
    {
        _load = true;
    }
    private void Update()
    {
        if(_load == true)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("test");
            //SceneManager.LoadSceneAsync("test", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("UIStart");
            DontDestroyOnLoad(SceneLoader);
        }
    }

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    #endregion
}
