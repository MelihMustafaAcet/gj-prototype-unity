using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject completeLevelUI;

    public GameObject fadeAnimUI;
    public GameObject MainScreenUI;
    public GameObject FailScreenUI;
    public GameObject coinText;
    public GameObject coinTotal;
    public GameObject coinTotalS;
    public GameObject coinTotalX;
    public GameObject coinImg;
    public float fadeAnimDelay;

    public GameObject player;
    public GameObject playerBody;
    //public SwerveInputSystem swerveInputSystem;
    //public SwerveMovement swerveMovement;
    bool isLevelCompleted = false;
    bool isLevelRestarting = false;
    bool isLevelStarted = false;

    int coinCounter=0;

    public void CompleteLevel()
    {
        if(!isLevelCompleted){
            coinTotal.GetComponent<UnityEngine.UI.Text>().text = coinCounter.ToString();
            coinTotalS.GetComponent<UnityEngine.UI.Text>().text = coinCounter.ToString();
            coinTotalX.GetComponent<UnityEngine.UI.Text>().text = (coinCounter*10).ToString();
            coinImg.SetActive(false);
            coinText.SetActive(false);
            isLevelCompleted = true;
            completeLevelUI.SetActive(true);
            //Do Player stop anim stop every thing...
            StopPlayer();
        }
    }
    public void StartLevel()
    {
        MainScreenUI.SetActive(false);
        coinImg.SetActive(true);
        coinText.SetActive(true);
        player.GetComponent<SwerveInputSystem>().EnableSwerveInput();
        player.GetComponent<SwerveMovement>().EnableSwerveMove();
        player.GetComponent<PlayerMovement>().StartRunning();
        playerBody.GetComponent<Animator>().SetBool("isRunning",true);
    }
    public void SwipeToPlayButton(){
        StartLevel();
    }
    public void PlayerFailed(){
        FailScreenUI.SetActive(true);
        StopPlayer();
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isLevelRestarting=false;
    }

    public void RestartButton()
    {
        if(!isLevelRestarting)
        {
            isLevelRestarting=true;
            fadeAnimUI.SetActive(false);
            fadeAnimUI.SetActive(true);
            Invoke("RestartLevel",fadeAnimDelay);
        }
    }

    public void EndStartingPanel()
    {
        if(!isLevelStarted)
        {
            isLevelStarted=true;
            MainScreenUI.SetActive(false);
        }
    }

    public void CoinSButton()
    {
        //next level anim
        fadeAnimUI.SetActive(false);
        fadeAnimUI.SetActive(true);
        Invoke("LoadNextLevel",1.5f);
    }

    public void CoinXButton()
    {
        fadeAnimUI.SetActive(false);
        fadeAnimUI.SetActive(true);
        Invoke("LoadNextLevel",1.5f);
    }

    public void LoadNextLevel()
    {
        
        if(SceneManager.GetActiveScene().buildIndex==2){
            SceneManager.LoadScene(0);    
        }
        else
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); 
        }
    }

    public void StopPlayer()
    {
        player.GetComponent<SwerveInputSystem>().DisableSwerveInput();
        player.GetComponent<SwerveMovement>().DisableSwerveMove();
        player.GetComponent<PlayerMovement>().StopRunning();
    }

    public void IncrementCoin()
    {
        coinCounter++;
        coinText.GetComponent<UnityEngine.UI.Text>().text = coinCounter.ToString();
        //txt.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
        //textleri güncelle
    }
}
