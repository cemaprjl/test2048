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
//        _touchControls.Touch.TouchInput.performed += context => ActionChecker(context);
//        _touchControls.Touch.MouseMove.started += context => Moved(context);
//        _touchControls.Touch.MouseMove.canceled += context => ActionChecker(context);
        _touchControls.Player.Move.performed += context => Moved(context);
    }

  
    
    private void Moved(InputAction.CallbackContext context)
    {
        if(_pressed) 
        {
//            Debug.Log("MOVE " + context.ReadValue<Vector2>());
            OnDrag?.Invoke(context.ReadValue<Vector2>());
        }
    }

    private void UnTouched(InputAction.CallbackContext context)
    {
        _pressed = false;
        var f = Touchscreen.current.primaryTouch.position.ReadValue();
        OnUntap?.Invoke(f);
    }

    private void Touched(InputAction.CallbackContext context)
    {
        _pressed = true;
        var f = Touchscreen.current.primaryTouch.position.ReadValue();
        OnTap?.Invoke(f);
    }

}
