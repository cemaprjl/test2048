// GENERATED AUTOMATICALLY FROM 'Assets/Settings/TouchControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TouchControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TouchControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchControls"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""6af08a24-6c85-4585-95c4-7f3522c734f2"",
            ""actions"": [
                {
                    ""name"": ""TouchContact"",
                    ""type"": ""Button"",
                    ""id"": ""ad5a4558-02fb-443c-9d0d-965d8e5ddec0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e9f5cfc3-64f7-4a71-9968-d1a14b68eaf9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c74a1a5d-398d-4c34-b9c4-79adfdd539b3"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de57aa52-114c-4e70-9c4e-9844facf267f"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""main"",
            ""bindingGroup"": ""main"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_TouchContact = m_Touch.FindAction("TouchContact", throwIfNotFound: true);
        m_Touch_MouseMove = m_Touch.FindAction("MouseMove", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Touch
    private readonly InputActionMap m_Touch;
    private ITouchActions m_TouchActionsCallbackInterface;
    private readonly InputAction m_Touch_TouchContact;
    private readonly InputAction m_Touch_MouseMove;
    public struct TouchActions
    {
        private @TouchControls m_Wrapper;
        public TouchActions(@TouchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchContact => m_Wrapper.m_Touch_TouchContact;
        public InputAction @MouseMove => m_Wrapper.m_Touch_MouseMove;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterface != null)
            {
                @TouchContact.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchContact;
                @TouchContact.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchContact;
                @TouchContact.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchContact;
                @MouseMove.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnMouseMove;
                @MouseMove.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnMouseMove;
                @MouseMove.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnMouseMove;
            }
            m_Wrapper.m_TouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchContact.started += instance.OnTouchContact;
                @TouchContact.performed += instance.OnTouchContact;
                @TouchContact.canceled += instance.OnTouchContact;
                @MouseMove.started += instance.OnMouseMove;
                @MouseMove.performed += instance.OnMouseMove;
                @MouseMove.canceled += instance.OnMouseMove;
            }
        }
    }
    public TouchActions @Touch => new TouchActions(this);
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
}
