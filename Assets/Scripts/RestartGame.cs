using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartGame : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public Game GameMgr;
    
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log($"OnPointerUp");
        GameMgr.RestartGame();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        
        Debug.Log($"OnPointerDown");
        
    }
}
