using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PauseGame()
    { 
        SceneManager.LoadScene("Menu");
    }
    
    public void OpenItemShop()
    {
        SceneManager.LoadScene("ShopSystem", LoadSceneMode.Additive);
    }

    public void CloseItemShop()
    {
        SceneManager.UnloadScene("ShopSystem");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Map1");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}