using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public void StartGame() 
    {
        Debug.Log("開始遊戲");  
        SceneManager.LoadScene(1);              
    }
    public void QuitGame() 
    {
        Debug.Log("離開遊戲");
        Application.Quit();      
    }
    public void MainGame() 
    {
        SceneManager.LoadScene(0);              
    }

    public void ResetGame() 
    {
        Debug.Log("重新遊戲");      
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);          
    }
    public void StopGame() 
    {
        Debug.Log("暫停遊戲");
        Time.timeScale = 0f;
    }
    public void ContinueGame() 
    {
        Debug.Log("繼續遊戲");
        Time.timeScale = 1f;
    }
}
