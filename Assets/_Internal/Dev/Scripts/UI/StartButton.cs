using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void StartLevel()
    {
        LevelManager.Instance.StartGame();
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
