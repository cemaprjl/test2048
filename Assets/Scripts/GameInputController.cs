using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class GameInputController : IInputActionCollection
{
    public IEnumerator<InputAction> GetEnumerator()
    {
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Contains(InputAction action)
    {
        return false;
    }

    public void Enable()
    {
    }

    public void Disable()
    {
    }

    public InputBinding? bindingMask { get; set; }
    public ReadOnlyArray<InputDevice>? devices { get; set; }
    public ReadOnlyArray<InputControlScheme> controlSchemes { get; }
}
