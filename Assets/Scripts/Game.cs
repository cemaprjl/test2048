using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{

    #region properties
    public Vector3 Force = Vector3.back * 100f;
    public Pool CubesPool;
    public Transform CubesContainer;
    public Transform ShotPost;
    
    public Text WinScore;
    public Text ScoreTF;
    
    public UIController ui;
    
    [Range(0, 6)]
    public int RowsCount = 3; 
    
    private bool _listenTouch = false;

    private int _maxCubeValue = 1;
    private int _score = 0;
    private int _moves = 0;
    private float _boxStep = 1.2f;
    private int _screenWidth;
    private Cube _bulletCube;
    private bool _initializedGame = false;
    
    private Vector3 _targetLeft;
    private Vector3 _targetRight;
    private Vector3 _сurrentPosition;

    public GameInputManager Input;

    #endregion
    
    private void OnEnable()
    {
        Input.OnDrag += MoveCube;
        Input.OnUntap += Shoot;
    }

    private void OnDisable()
    {
        Input.OnDrag -= MoveCube;
        Input.OnUntap -= Shoot;
    }   
    private void Start()
    {
        _screenWidth = Screen.width;
        _targetLeft = Vector3.left * _boxStep * 2f;
        _targetRight = Vector3.right * _boxStep * 2f;
        _сurrentPosition = Vector3.zero;
        InitGame();
    }
    public void RestartGame()
    {
        if (!_initializedGame)
        {
            InitGame();
        }
        ui.SetState(GameState.Play);
        _listenTouch = true;
    }

    private void InitGame()
    {
        _moves = 0;
        _score = 0;
        _maxCubeValue = 2;
        CubesContainer.DestroyChildren(true);
        for (int i = 0; i < RowsCount; i++)
        {
            CreateRow(_boxStep * i);
        }
        _initializedGame = true;
        AddBulletCube();
        UpdateScore();
    }


    private float PointToPosition(float source)
    {
        return source / _screenWidth - 0.5f;
    }
    
    private void MoveCube(Vector2 position)
    {
        if (!_listenTouch)
        {
            return;
        }
        var pos = _bulletCube.transform.localPosition;
        pos.x = -PointToPosition(position.x) * _boxStep * 4f;
        pos.x = Mathf.Clamp(pos.x, _targetLeft.x, _targetRight.x);
        _bulletCube.transform.localPosition = pos;
        _сurrentPosition = pos;
    }



    private void UpdateScore()
    {
        ScoreTF.text = $"Score: {_score}\nMoves: {_moves}";
    }

    private void AddBulletCube()
    {
        if (_bulletCube != null)
        {
            //allow to the launched cube collide with the game over area 
            _bulletCube.gameObject.layer = (int)CollisionLayers.Cube;
        }
        ShotPost.DestroyChildren(true);
        _bulletCube = CubesPool.GetItem<Cube>();
        _bulletCube.Reset();
        _bulletCube.EnablePhysics(false);
        _bulletCube.transform.SetParent(ShotPost);
        _bulletCube.transform.localPosition = _сurrentPosition;
        _bulletCube.SetValue(Random.Range(0, _maxCubeValue));
        _bulletCube.OnDestroyedEvent += OnDestroyCube;
        _bulletCube.OnGrowEvent += OnGrowCube;
        _bulletCube.OnWinEvent += OnWin;
        _bulletCube.OnLooseEvent += OnLoose;
        _bulletCube.gameObject.layer = (int)CollisionLayers.Bullet;
        
        _listenTouch = true;
    }

    private void OnWin(Cube target)
    {
        ui.SetState(GameState.Win);
        WinScore.text = $"Score: {_score}\nMoves: {_moves}";
    }
    
    private void OnLoose(Cube target)
    {
        ui.SetState(GameState.Loose);
    }

    private void CreateRow(float pos)
    {
        for (int i = -2; i < 3; i++)
        {
            AddInitialCube(new Vector3(_boxStep * i, 0f, pos));
        }
    }

    private void AddInitialCube(Vector3 position)
    {
        var cube = CubesPool.GetItem<Cube>();
        cube.Reset();
        cube.transform.SetParent(CubesContainer);
        cube.transform.localPosition = position;

        cube.SetValue(Random.Range(0, _maxCubeValue));
        cube.OnDestroyedEvent += OnDestroyCube;
        cube.OnGrowEvent += OnGrowCube;
        cube.OnWinEvent += OnWin;
        cube.OnLooseEvent += OnLoose;
        cube.gameObject.layer = (int)CollisionLayers.Cube;
    }

    private void OnGrowCube(Cube target)
    {
        _maxCubeValue = Math.Max(target.Val, _maxCubeValue);
        _score += target.Score;
        UpdateScore();
    }

    private void OnDestroyCube(Cube target)
    {
        target.OnDestroyedEvent -= OnDestroyCube;
        target.OnGrowEvent -= OnGrowCube;
        target.OnWinEvent -= OnWin;
        target.OnLooseEvent -= OnLoose;
        target.transform.localScale = Vector3.one;
        CubesPool.ReturnItem(target.gameObject);
    }


    private void Shoot(Vector2 position)
    {
        
        Debug.Log($"SHOOT {ui.CurrentState}, listenTouch {_listenTouch}");
        
        if (!_listenTouch || ui.CurrentState != GameState.Play)
        {
            return;
        }
        _listenTouch = false;
        _initializedGame = false;
        
        var newPosition = _bulletCube.transform.localPosition;
        newPosition.x = -PointToPosition(position.x) * _boxStep * 4f;
        _bulletCube.transform.localPosition = newPosition;
        _bulletCube.transform.SetParent(CubesContainer);
        _bulletCube.transform.localRotation = Quaternion.identity;
        _bulletCube.transform.localScale = Vector3.one;
        _bulletCube.EnablePhysics(true);
        _bulletCube.Shoot(Force);

        _moves += 1;
        UpdateScore();
        
        StartCoroutine(WaitFor(1f, AddBulletCube));

        
    }

    private IEnumerator WaitFor(float time, Action callback = null)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    
    
}
