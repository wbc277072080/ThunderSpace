using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Canvas hUDCanvas;
    [SerializeField] Canvas menusCanvas;

    [Header("Player Input")]
    [SerializeField] Button resumeButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button mainMenuButton;


    void OnEnable(){
        playerInput.onPause += Pause;
        playerInput.onUnpause += Unpause;

        // resumeButton.onClick.AddListener(OnResumeButtonClick);
        // optionsButton.onClick.AddListener(OnOptionsButtonClick);
        // mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);

        //behavior
        ButtonPressedBehavior.buttonFunctionTable.Add(resumeButton.gameObject.name, OnResumeButtonClick);
        ButtonPressedBehavior.buttonFunctionTable.Add(optionsButton.gameObject.name, OnOptionsButtonClick);
        ButtonPressedBehavior.buttonFunctionTable.Add(mainMenuButton.gameObject.name, OnMainMenuButtonClick);
    }
    void OnDisable(){
        playerInput.onPause -= Pause;
        playerInput.onUnpause -= Unpause;

        // resumeButton.onClick.RemoveAllListeners();
        // optionsButton.onClick.RemoveAllListeners();
        // mainMenuButton.onClick.RemoveAllListeners();
    }

    //Pause
    void Pause(){
        Time.timeScale = 0f;
        GameManager.GameState = GameState.Paused;
        hUDCanvas.enabled = false;
        menusCanvas.enabled = true;
        playerInput.EnablePauseMenuInput();
        playerInput.SwitchToDynamicUpdateMode();

        UIInput.Instance.SelectUI(resumeButton);
    }

    void Unpause(){
        resumeButton.Select();
        resumeButton.animator.SetTrigger("Pressed");
        //OnResumeButtonClick();
    }

    void OnResumeButtonClick(){
        Time.timeScale = 1f;
        GameManager.GameState = GameState.Playing;
        hUDCanvas.enabled = true;
        menusCanvas.enabled = false;
        playerInput.EnableGameplayInput();
        playerInput.SwitchToFixedUpdateMode();
    }

    //option
    void OnOptionsButtonClick(){
       UIInput.Instance.SelectUI(optionsButton);
       playerInput.EnablePauseMenuInput();
    }

    //main menu
    void OnMainMenuButtonClick(){
        menusCanvas.enabled = false;
        //load main menu scene
        SceneLoader.Instance.LoadMainMenuScene();
        

    }


}
