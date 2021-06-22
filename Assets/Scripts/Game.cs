using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
//    [SerializeField]
//    private Cube ShootCube;

    public Vector3 Force = Vector3.back * 100f;
    public Pool CubesPool;

    public Transform CubesContainer;
    public Transform ShotPost;
    public Transform WinUI;
    public Text WinScore;

    [Range(0, 6)]
    public int RowsCount = 3; 
    
    private float _boxStep = 1.2f;
    private Cube _bulletCube;

    private Vector3 _targetLeft;
    private Vector3 _targetRight;
    private Vector3 _сurrentPosition;
    private bool isLeftDirection = true;
    private Coroutine _waitCoroutine;
    [SerializeField] private float Speed = 100f;

    public GameInputManager Input;

    private bool listenTouch = false;

    private int maxValue = 1;
    private int score = 0;
    private int moves = 0;

    public Text ScoreTF;

    private Camera _cam;
    private int _screenWidth;

    private void Start()
    {
        _cam = Camera.main;
        _screenWidth = Screen.width;
        _targetLeft = Vector3.left * _boxStep * 2f;
        _targetRight = Vector3.right * _boxStep * 2f;
        _сurrentPosition = Vector3.zero;
        WinUI.gameObject.SetActive(false);
        InitGame();
    }


    private float PointToPosition(float source)
    {
        return source / _screenWidth - 0.5f;
    }
    
    private void MoveCube(Vector2 position)
    {
        if (!listenTouch)
        {
            return;
        }
        Debug.Log(position);
        var pos = _bulletCube.transform.localPosition;
        pos.x = -PointToPosition(position.x) * _boxStep * 4f;
        pos.x = Mathf.Clamp(pos.x, _targetLeft.x, _targetRight.x);
        _bulletCube.transform.localPosition = pos;
        _сurrentPosition = pos;
    }

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

    private void InitGame()
    {
        moves = 0;
        score = 0;
        CubesContainer.DestroyChildren(true);
        for (int i = 0; i < RowsCount; i++)
        {
            CreateRow(_boxStep * i);
        }

        AddBulletCube();
        UpdateScore();
    }

    private void UpdateScore()
    {
        ScoreTF.text = $"Score: {score}\nMoves: {moves}";
    }

    private void AddBulletCube()
    {
        if (_bulletCube != null)
        {
            //allow to the launched cube collide with the game over area 
            _bulletCube.gameObject.layer = 8;
        }
        ShotPost.DestroyChildren(true);
        _bulletCube = CubesPool.GetItem<Cube>();
        _bulletCube.Reset();
        _bulletCube.EnablePhysics(false);
        _bulletCube.transform.SetParent(ShotPost);
        _bulletCube.transform.localPosition = _сurrentPosition;
        _bulletCube.SetValue(Random.Range(0, maxValue));
        _bulletCube.OnDestroyedEvent += OnDestroyCube;
        _bulletCube.OnGrowEvent += OnGrowCube;
        _bulletCube.OnWinEvent += OnWin;
        _bulletCube.gameObject.layer = 7;
        
        listenTouch = true;
    }

    private void OnWin(Cube target)
    {
        WinUI.gameObject.SetActive(true);
        WinScore.text = $"Score: {score}\nMoves: {moves}";
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

        cube.SetValue(Random.Range(0, 3));
        cube.OnDestroyedEvent += OnDestroyCube;
        cube.OnGrowEvent += OnGrowCube;
        cube.gameObject.layer = 8;
    }

    private void OnGrowCube(Cube target)
    {
        maxValue = Math.Max(target.Val, maxValue);
        score += target.Score;
        UpdateScore();
    }

    private void OnDestroyCube(Cube target)
    {
        target.OnDestroyedEvent -= OnDestroyCube;
        target.OnGrowEvent -= OnGrowCube;
        target.transform.localScale = Vector3.one;
        CubesPool.ReturnItem(target.gameObject);
    }


    private void Shoot(Vector2 position)
    {
        if (!listenTouch)
        {
            return;
        }
        listenTouch = false;

        var newPosition = _bulletCube.transform.localPosition;
        newPosition.x = -PointToPosition(position.x) * _boxStep * 4f;
        _bulletCube.transform.localPosition = newPosition;
        _bulletCube.transform.SetParent(CubesContainer);
        _bulletCube.transform.localRotation = Quaternion.identity;
        _bulletCube.transform.localScale = Vector3.one;
        _bulletCube.EnablePhysics(true);
        _bulletCube.Shoot(Force);

        moves += 1;
        UpdateScore();
        
        _waitCoroutine = StartCoroutine(WaitFor(1f, AddBulletCube));
    }

    private IEnumerator WaitFor(float time, Action callback = null)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

}
