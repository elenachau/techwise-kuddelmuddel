using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PauseGame()
    { 
        AudioManager.Instance.PlayUI();
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
    
    public void OpenItemShop()
    {
        AudioManager.Instance.PlayUI();
        SceneManager.LoadScene("ShopSystem", LoadSceneMode.Additive);
        
    }

    public void CloseItemShop()
    {
        AudioManager.Instance.PlayUI();
        SceneManager.UnloadSceneAsync("ShopSystem");
    }

    public void PlayGame()
    {
        AudioManager.Instance.PlayUI();
        if (PlayerData.Instance == null){ // game has not started yet
            SceneManager.LoadScene("Map1");
        }
        else {
            SceneManager.UnloadSceneAsync("Menu");
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        AudioManager.Instance.PlayUI();
        Application.Quit();
    }

}