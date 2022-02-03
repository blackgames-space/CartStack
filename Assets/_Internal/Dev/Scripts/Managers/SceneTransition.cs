using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneTransition : MonoSingleton<SceneTransition>
{
    protected int _currentLevel;

    [SerializeField]
    protected int _maxLevel = 10;

    [SerializeField]
    protected TextMeshProUGUI _levelText;

    [SerializeField]
    protected VariablesSaver _saver;

    [SerializeField]
    bool _isTransferScene = false;

    [SerializeField]
    float _waitBeforeTransfere = 1f;

    //protected override void Awake ()
    //{
    //    base.Awake();

    //    _saver = VariablesSaver.Instance;
    //    _currentLevel = _saver.GetCurrentLevel();
    //    Debug.Log("Current level from SceneTransition: " + _currentLevel);

    //    if (!_isTransferScene)
    //    {
    //        _levelText.text = "LEVEL " + _currentLevel;
    //    }
    //}

    private void Start()
    {
        _saver = VariablesSaver.Instance;
        _currentLevel = _saver.GetCurrentLevel();
        Debug.Log("Current level from SceneTransition: " + _currentLevel);

        if (!_isTransferScene)
        {
            _levelText.text = "LEVEL " + _currentLevel;
        }
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public int GetMaxLevel()
    {
        return _maxLevel;
    }

    public int GetPreviousLevel()
    {
        return _saver.GetPreviousLevel();
    }

    public void TransferToNextlevel()
    {
        _saver.ChangeLevel(_currentLevel+1, SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(WaitingBeforeTransfer(0));
    }

    IEnumerator WaitingBeforeTransfer(int sceneIndex)
    {
        yield return new WaitForSeconds(_waitBeforeTransfere);

        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(_currentLevel);
    }

    public void LoadChosenLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void Restart()
    {
        StartCoroutine(WaitingBeforeTransfer(SceneManager.GetActiveScene().buildIndex));
    }
}
