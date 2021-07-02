using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameState CurrentState => _state;
    
    private GameState _state = GameState.Lobby;

    public GameObject GameUI;
    public GameObject LobbyUI;
    public GameObject WinUI;
    public GameObject LooseUI;
    
    
    private void Awake()
    {
        SetState(_state);
    }

    public void SetState(GameState st)
    {
        _state = st;
        GameUI.SetActive(st == GameState.Play);
        LobbyUI.SetActive(st == GameState.Lobby);
        WinUI.SetActive(st == GameState.Win);
        LooseUI.SetActive(st == GameState.Loose);
    }

}