using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, InputActions.IGameplayActions
{   
    //event for start move
    public event UnityAction<Vector2> onMove = delegate{};
    //event for stop move
    public event UnityAction onStopMove = delegate{};

    //fire
    public event UnityAction onFire = delegate{};
    public event UnityAction onStopFire = delegate{};


    InputActions inputActions;

    void OnEnable() {
        inputActions = new InputActions();
        //callback function for gameplay acitons
        inputActions.Gameplay.SetCallbacks(this);
    }

    void OnDisable() {
        DisableAllInputs();
    }

    public void DisableAllInputs(){
        inputActions.Gameplay.Disable();
    }

    public void EnableGameplayInput(){
        inputActions.Gameplay.Enable();

        //make mouse invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
    
}
