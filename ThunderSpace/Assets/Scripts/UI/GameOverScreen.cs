using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] PlayerInput input;

    [SerializeField] Canvas HUDCanvas;

    //exit animator
    int exitStateID = Animator.StringToHash("GameOverScreenExit");

    Canvas canvas;

    Animator animator;

    void Awake(){
        canvas =  GetComponent<Canvas>();
        animator = GetComponent<Animator>();

        canvas.enabled = false;
        animator.enabled = false;
    }

    void OnEnable(){
        GameManager.onGameOver += OnGameOver;

        input.onConfirmGameOver += OnConfirmGameOver;
    }

    void OnDisable(){
        GameManager.onGameOver -= OnGameOver;

        input.onConfirmGameOver -= OnConfirmGameOver;
    }

    void OnGameOver(){
        HUDCanvas.enabled = false;
        canvas.enabled = true;
        animator.enabled = true;
        input.DisableAllInputs();
    }

    void OnConfirmGameOver(){
        input.DisableAllInputs();
        animator.Play(exitStateID);
        SceneLoader.Instance.LoadMainMenuScene();
    }

    //Animation event
    void EnableGameOverScreenInput(){
        input.EnableGameOverScreenInput();
    }
    
}
