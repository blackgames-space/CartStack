using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera _main;

    PlayerMove _playerMove;

    [SerializeField]
    public float _normalizeDeltaMultiplier = 1f;

    float _oldPosX;
    float _newPosX;
    float _deltaX;

    public bool isRunning;
    [SerializeField]
    bool _isChangingDirection;

    LevelManager _levelManager;

    int _frameCount;

    // Start is called before the first frame update
    void Start()
    { 
        _main = Camera.main;
        _playerMove = GetComponent<PlayerMove>();

        _isChangingDirection = false;
        isRunning = false;

        _levelManager = LevelManager.Instance;
        _levelManager.GameStartAction += StartRunning;

        _deltaX = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _newPosX = _main.ScreenToViewportPoint(Input.mousePosition).x;
            _isChangingDirection = true;

            _frameCount = 0;
            _deltaX = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            _oldPosX = _newPosX;

            _newPosX = _main.ScreenToViewportPoint(Input.mousePosition).x;

            _deltaX += (_newPosX - _oldPosX) * _normalizeDeltaMultiplier;
            _frameCount++;

        }

        if (Input.GetMouseButtonUp(0))
        {
            _isChangingDirection = false;
            _frameCount = 0;
            _deltaX = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1) Time.timeScale = 0.06f;
            else Time.timeScale = 1f;
        }

        //if (isRunning)
        //{
        //    if (!_isChangingDirection) _playerMove.Move();
        //    else _playerMove.Move(_deltaX);
        //}
    }

    public void StartRunning()
    {
        isRunning = true;
    }

    private void FixedUpdate()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    _oldPosX = _main.ScreenToViewportPoint(Input.mousePosition).x;
        //    _isChangingDirection = true;
        //}

        //if (Input.GetMouseButton(0))
        //{
        //    _newPosX = _main.ScreenToViewportPoint(Input.mousePosition).x;

        //    _deltaX = (_newPosX - _oldPosX) / _normalizeDeltaMultiplier;

        //    _oldPosX = _newPosX;
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    _isChangingDirection = false;
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (Time.timeScale == 1) Time.timeScale = 0.01f;
        //    else Time.timeScale = 1f;
        //}

        if (isRunning)
        {
            if(_frameCount > 0) _deltaX = _deltaX / _frameCount;

            //Debug.Log("Fixed update call, deltaX is " + _deltaX);
            if (!_isChangingDirection) _playerMove.Move();
            else _playerMove.Move(_deltaX);

            _frameCount = 0;
            _deltaX = 0f;
        }
    }
}
