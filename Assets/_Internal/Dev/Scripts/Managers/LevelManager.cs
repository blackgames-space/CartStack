using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoSingleton<LevelManager>
{
    public event UnityAction GameStartAction;
    public event UnityAction<bool> GameFinishAction;
    public event UnityAction GameEndAction;

    //[SerializeField]
    //string _finisherStartTag = "FinisherStart";
    //[SerializeField]
    //string _finisherEndTag = "FinisherEnd";
    //[SerializeField]
    //public Transform finisherStartPos;
    //[SerializeField]
    //public Transform finisherEndPos;

    private void Start()
    {
        //finisherStartPos = GameObject.FindWithTag(_finisherStartTag).transform;
        //finisherEndPos = GameObject.FindWithTag(_finisherEndTag).transform;
    }

    void OnGameStart()
    {
        GameStartAction?.Invoke();
    }

    void OnGameFinish(bool isWin)
    {
        GameFinishAction?.Invoke(isWin);
    }

    void OnGameEnd()
    {
        GameEndAction?.Invoke();
    }

    public void StartGame()
    {
        Debug.Log("Game started");
        //if (StatModule.Instance != null) StatModule.Instance.LevelStart();
        OnGameStart();
    }

    public void EndGame()
    {
        //if (StatModule.Instance != null) StatModule.Instance.LevelFinish(true);
        OnGameEnd();
    }

    public void Finish(bool isWin)
    {
        OnGameFinish(isWin);
    }
}

