using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, 
InputActions.IGameplayActions, 
InputActions.IPauseMenuActions,
InputActions.IGameOverScreenActions
{   
    //event for start move
    public event UnityAction<Vector2> onMove = delegate{};
    //event for stop move
    public event UnityAction onStopMove = delegate{};

    //fire
    public event UnityAction onFire = delegate{};
    public event UnityAction onStopFire = delegate{};

    //pause
    public event UnityAction onPause = delegate{};
    public event UnityAction onUnpause = delegate{};

    //game over
    public event UnityAction onConfirmGameOver = delegate{};


    InputActions inputActions;

    void OnEnable() {
        inputActions = new InputActions();
        //callback function for gameplay acitons
        inputActions.Gameplay.SetCallbacks(this);
        inputActions.PauseMenu.SetCallbacks(this);
        inputActions.GameOverScreen.SetCallbacks(this);
    }

    void OnDisable() {
        DisableAllInputs();
    }

    //pause
    void SwitchActionMap(InputActionMap actionMap, bool isUIInput){
        inputActions.Disable();
        actionMap.Enable();

        if(isUIInput){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }else{
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    //switch input setting update mode
    public void SwitchToDynamicUpdateMode(){
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    }

    public void SwitchToFixedUpdateMode(){
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    }

    public void DisableAllInputs(){
        inputActions.Disable();
    }

    public void EnableGameplayInput(){
        SwitchActionMap(inputActions.Gameplay,false);
    }

    public void EnablePauseMenuInput(){
        SwitchActionMap(inputActions.PauseMenu, true);
    }

    public void EnableGameOverScreenInput(){
        SwitchActionMap(inputActions.GameOverScreen, false);
    }

    public void OnMove(InputAction.CallbackContext context){
        //keep moving when player press the key
        if(context.phase == InputActionPhase.Performed){
            onMove.Invoke(context.ReadValue<Vector2>());
        }
        //stop move when released the key
        if(context.phase == InputActionPhase.Canceled){
            onStopMove.Invoke();
        }
    }

    public void OnFire(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed){
            onFire.Invoke();
        }

        if(context.phase == InputActionPhase.Canceled){
            onStopFire.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context){
        if(context.performed){
            onPause.Invoke();
        }
    }

    public void OnUnPause(InputAction.CallbackContext context){
        if(context.performed){
            onUnpause.Invoke();
        }
    }

    public void OnConfirmGameOver(InputAction.CallbackContext context){
        if(context.performed){
            onConfirmGameOver.Invoke();
        }
    }
    
}
