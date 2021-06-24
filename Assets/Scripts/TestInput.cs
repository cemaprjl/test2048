// GENERATED AUTOMATICALLY FROM 'Assets/Settings/TouchControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class TestInput 
{
    public InputActionAsset asset { get; }
    public TestInput()
    {
        asset = ScriptableObject.CreateInstance<InputActionAsset>();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    private readonly InputActionMap m_Touch;
    private ITouchActions m_TouchActionsCallbackInterface;
    private readonly InputAction m_Touch_TouchContact;
    private readonly InputAction m_Touch_MouseMove;
    public struct TouchActions
    {
        private TestInput m_Wrapper;
        public TouchActions(TestInput wrapper) { m_Wrapper = wrapper; }
        public InputAction TouchContact => m_Wrapper.m_Touch_TouchContact;
        public InputAction MouseMove => m_Wrapper.m_Touch_MouseMove;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterface != null)
            {
                TouchContact.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchContact;
                TouchContact.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchContact;
                TouchContact.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchContact;
                MouseMove.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnMouseMove;
                MouseMove.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnMouseMove;
                MouseMove.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnMouseMove;
            }
            m_Wrapper.m_TouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                TouchContact.started += instance.OnTouchContact;
                TouchContact.performed += instance.OnTouchContact;
                TouchContact.canceled += instance.OnTouchContact;
                MouseMove.started += instance.OnMouseMove;
                MouseMove.performed += instance.OnMouseMove;
                MouseMove.canceled += instance.OnMouseMove;
            }
        }
    }
    public TouchActions Touch => new TouchActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Click;
    public struct UIActions
    {
        private TestInput m_Wrapper;
        public UIActions(TestInput wrapper) { m_Wrapper = wrapper; }
        public InputAction Click => m_Wrapper.m_UI_Click;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                Click.started += instance.OnClick;
                Click.performed += instance.OnClick;
                Click.canceled += instance.OnClick;
            }
        }
    }
    public UIActions UI => new UIActions(this);
    private int m_mainSchemeIndex = -1;
    public InputControlScheme mainScheme
    {
        get
        {
            if (m_mainSchemeIndex == -1) m_mainSchemeIndex = asset.FindControlSchemeIndex("main");
            return asset.controlSchemes[m_mainSchemeIndex];
        }
    }
    public interface ITouchActions
    {
        void OnTouchContact(InputAction.CallbackContext context);
        void OnMouseMove(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnClick(InputAction.CallbackContext context);
    }
}
