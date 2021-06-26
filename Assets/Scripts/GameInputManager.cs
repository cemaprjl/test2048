using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class GameInputManager : MonoBehaviour
{
    private GameInputActions _touchControls;
    public delegate void TouchEvent(Vector2 position);
    public event TouchEvent OnDrag;
    public event TouchEvent OnTap;
    public event TouchEvent OnUntap;

    private bool _pressed = false;
    private Coroutine _check;

#if UNITY_EDITOR
    private ControlScheme _scheme = ControlScheme.editor;
#elif UNITY_ANDROID
    private ControlScheme _scheme = ControlScheme.android;
#endif
    
    
    void Awake()
    {
        _touchControls = new GameInputActions();
    }

    private void OnEnable()
    {
        _touchControls.Enable();
        TouchSimulation.Enable();
    }

    private void OnDisable()
    {
        _touchControls.Disable();
        TouchSimulation.Disable();
        
    }

    private void Start()
    {
        _touchControls.Player.TouchContact.started += context => Touched(context);
        _touchControls.Player.TouchContact.canceled += context => UnTouched(context);
        _touchControls.Player.Move.performed += context => Moved(context);
    }

  
    
    private void Moved(InputAction.CallbackContext context)
    {
        if(_pressed) 
        {
            OnDrag?.Invoke(context.ReadValue<Vector2>());
        }
    }

    private void UnTouched(InputAction.CallbackContext context)
    {
        _pressed = false;
        var pos = Vector2.zero;
        if (_scheme == ControlScheme.android)
        {
            pos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else 
        {
            pos = Mouse.current.position.ReadValue();            
        }

        OnUntap?.Invoke(pos);
    }

    private void Touched(InputAction.CallbackContext context)
    {
        _pressed = true;
        var pos = Vector2.zero;
        if (_scheme == ControlScheme.android)
        {
            pos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else 
        {
            pos = Mouse.current.position.ReadValue();            
        }
        OnTap?.Invoke(pos);
    }

}


public enum ControlScheme {
    editor,
    android
}