using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] Canvas mainMenuCanvas;
    [Header("Button")]
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonOption;
    [SerializeField] Button buttonQuit;
    

    void OnEnable(){
        //buttonStartGame.onClick.AddListener(OnStartGameButtonClick);
        ButtonPressedBehavior.buttonFunctionTable.Add(buttonStart.gameObject.name,OnStartGameButtonClick);
        ButtonPressedBehavior.buttonFunctionTable.Add(buttonOption.gameObject.name,OnButtonOptionsClick);
        ButtonPressedBehavior.buttonFunctionTable.Add(buttonQuit.gameObject.name,OnButtonQuitClick);

    }

    void OnDisable(){
        ButtonPressedBehavior.buttonFunctionTable.Clear();
    }

    void Start(){
        Time.timeScale = 1f;
        GameManager.GameState = GameState.Playing;
        UIInput.Instance.SelectUI(buttonStart);
    }

    void OnStartGameButtonClick(){
        mainMenuCanvas.enabled = false;
        SceneLoader.Instance.LoadGameplayScene();
    }

    void OnButtonOptionsClick(){
        UIInput.Instance.SelectUI(buttonOption);
    }

    void OnButtonQuitClick(){
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
