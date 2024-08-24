using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject winScreen;
    void Update(){
        if (gameOverScreen.activeSelf || winScreen.activeSelf){
            if(Input.GetKeyDown(KeyCode.Space)){
                SceneManager.LoadScene("Game");
            }else if(Input.GetKeyDown(KeyCode.Escape)){
                Application.Quit();
            }
        }
    }
    public void OnEnable()
    {
        PlayerController.GameOver += GameOver;
        Boss.win += Win;
    }
    
    public void OnDisable()
    {
        PlayerController.GameOver -= GameOver;
        Boss.win -= Win;
    }
    
    private void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    private void Win(){
        winScreen.SetActive(true);
    }
}
