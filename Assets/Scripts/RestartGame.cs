using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartGame : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        
        Debug.Log($"click {pointerEventData}");
        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        
        Debug.Log($"click {eventData}");
        
    }
}
