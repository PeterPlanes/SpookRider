using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameUIManager : MonoBehaviour
{
    public GameObject UI_Pause;
    public GameObject UI_GameOver;
    public GameObject UI_GameisFinished;

    private enum GameUI_State
    {
        GamePlay, GamePause, GameOver, GameisFinished
    }
    GameUI_State currentState;

    // Start is called before the first frame update
    void Start()
    {
        SwitchUIState(GameUI_State.GamePlay);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseUI();
        }

        // Check Car IsDead
        if (CarCrashS1.instance != null && CarCrashS1.instance.isDead)
        {
            SwitchUIState(GameUI_State.GameOver);
            return; // Stop Working
        }

        if (CarCrashS2.instance != null && CarCrashS2.instance.isDead)
        {
            SwitchUIState(GameUI_State.GameOver);
            return; // Stop Working
        }

        // Check Goal
        if (CarCrashS2.instance != null && CarCrashS2.objectCountS2 >= CarCrashS2.instance.targetCountS2)
        {
            SwitchUIState(GameUI_State.GameisFinished);
            
        }

    }

    private void SwitchUIState(GameUI_State state)
    {
        UI_Pause.SetActive(false);  //Hide Ui
        UI_GameisFinished.SetActive(false);
        UI_GameOver.SetActive(false);

        Time.timeScale = 1.0f;

        switch (state) //State
        {
            case GameUI_State.GamePlay:
                break;
            case GameUI_State.GamePause:
                Time.timeScale = 0;
                UI_Pause.SetActive(true);
                break;
            case GameUI_State.GameOver:
                UI_GameOver.SetActive(true);
                break;
            case GameUI_State.GameisFinished:
                Time.timeScale = 0;
                UI_GameisFinished.SetActive(true);
                break;
        }
        currentState = state;
    }

    public void TogglePauseUI()
    {
        if (currentState == GameUI_State.GamePlay) //Check State game   play > pause
        {
            SwitchUIState(GameUI_State.GamePause);
        }
        else if (currentState == GameUI_State.GamePause) //Check State game   play < pause
        {
            SwitchUIState(GameUI_State.GamePlay);
        }
    }

    public void B_Mainmenu()
    {
        CarCrashS1.objectCount = 0;
        CarCrashS2.objectCountS2 = 0;
        SceneManager.LoadScene("Level1main");
    }
    public void B_Restart()
    {
        CarCrashS1.objectCount = 0;
        CarCrashS2.objectCountS2 = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void B_Resume()
    {
        SwitchUIState(GameUI_State.GamePlay);
    }
   // IEnumerator delayGUIGameisFinished()
    //{
      //  yield return new WaitForSeconds(3f); //Delay 3 second
        //SwitchUIState(GameUI_State.GameisFinished);
    //}

}