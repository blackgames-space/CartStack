using UnityEngine;

public  class VariablesSaver : MonoSingleton<VariablesSaver>
{
    public const string MoneyVariable = "Money";
    public const string CurrentLevelVariable = "Current level";
    public const string PreviousLevelVariable = "Previous level";
    public const string OpeningConeIndexVariable = "Opening cone index";
    public const string OpeningConeProgressVariable = "Opening cone progress";
    public const string NextConeVariable = "Next cone";
    public const string UnlockedConesVariable = "Unlocked cones";


    private int _currentMoney;
    private int _currentLevel;
    private int _previousLevel;
    private int _openingConeIndex;
    private int _openingConeProgress;
    private int _nextCone;
    private string _unlockedCones;

    private int _defaultMoney = 0;
    //private int _defaultMoney = 90;
    private int _defaultLevel = 1;
    private int _defaultPreviousLevel = -1;

    private int _defaultOpeningConeIndex = 1;
    private int _defaultOpeningConeProgress = 0;
    private int _defaultConeIndex = -1;
    private string _defaultUnlockedCones = "[\"Honeybunny\"]";

    protected override void Awake()
    {
        base.Awake();

        _currentMoney = LoadMoney();
        _currentLevel = LoadCurrentLevel();
        _previousLevel = LoadPreviousLevel();
        _openingConeIndex = LoadOpeningIndex();
        _openingConeProgress = LoadOpeningProgress();
        _nextCone = LoadNextCone();
        _unlockedCones = LoadUnlockedCones();

        Debug.Log("Current Money: " + _currentMoney);
        Debug.Log("Current Level: " + _currentLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {

        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Nullify();
        }

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    _currentLevel = 8;
        //    ChangeLevel(_currentLevel);
        //    Debug.Log("New lvl: " + _currentLevel);
        //}

    }

    public void AddMoney(int value)
    {
        _currentMoney += value;
        SaveMoney();
        //Debug.Log(LoadHearts());
    }

    public void SetMoney(int value)
    {
        _currentMoney = value;
        SaveMoney();
        //Debug.Log(LoadHearts());
    }

    public void SetOpeningConeIndex(int value)
    {
        _openingConeIndex = value;
        SaveOpeningConeIndex();
    }

    public void SetOpeningConeProgress(int value)
    {
        _openingConeProgress = value;
        SaveOpeningConeProgress();
    }

    public void SetNextCone(int value)
    {
        _nextCone = value;
        SaveNextConeIndex();
    }

    public void SetUnlockedCones(string value)
    {
        _unlockedCones = value;
        SaveUnlockedCones();
    }

    public int GetOpeningConeIndex()
    {
        return _openingConeIndex;
    }

    public int GetOpeningConeProgress()
    {
        return _openingConeProgress;
    }

    public int GetNextCone()
    {
        return _nextCone;
    }

    public string GetUnlockedCones()
    {
        return _unlockedCones;
    }

    public int GetMoney()
    {
        return LoadMoney();
    }

    public int GetCurrentLevel()
    {
        return LoadCurrentLevel();
    }
    public int GetPreviousLevel()
    {
        return LoadPreviousLevel();
    }

    public void Nullify()
    {
        PlayerPrefs.DeleteAll();
        SetAllToDefaults();
    }

    public void SetAllToDefaults()
    {
        _currentMoney = _defaultMoney;
        _currentLevel = _defaultLevel;
        _previousLevel = _defaultPreviousLevel;
    }

    public void ChangeLevel(int level)
    {
        _currentLevel = level;
        _previousLevel = level -1;
        SaveLevels();
        //Debug.Log(LoadLevel());
    }

    public void ChangeLevel(int currentLevel, int previousLevel)
    {
        _currentLevel = currentLevel;
        _previousLevel = previousLevel;
        SaveLevels();
        //Debug.Log(LoadLevel());
    }

    //UTILITY
    private void SaveMoney()
    {
        PlayerPrefs.SetInt(MoneyVariable, _currentMoney);

        PlayerPrefs.Save();
    }

    private int LoadMoney()
    {
        if (PlayerPrefs.HasKey(MoneyVariable)) _currentMoney = PlayerPrefs.GetInt(MoneyVariable);
        else _currentMoney = _defaultMoney;

        return _currentMoney;
    }

    private int LoadOpeningIndex()
    {
        if (PlayerPrefs.HasKey(OpeningConeIndexVariable)) _openingConeIndex = PlayerPrefs.GetInt(OpeningConeIndexVariable);
        else _openingConeIndex = _defaultOpeningConeIndex;

        return _openingConeIndex;
    }

    private int LoadOpeningProgress()
    {
        if (PlayerPrefs.HasKey(OpeningConeProgressVariable)) _openingConeProgress = PlayerPrefs.GetInt(OpeningConeProgressVariable);
        else _openingConeProgress = _defaultOpeningConeProgress;

        return _openingConeProgress;
    }

    private int LoadNextCone()
    {
        if (PlayerPrefs.HasKey(NextConeVariable)) _nextCone = PlayerPrefs.GetInt(NextConeVariable);
        else _nextCone = _defaultConeIndex;

        return _nextCone;
    }

    private string LoadUnlockedCones()
    {
        if (PlayerPrefs.HasKey(UnlockedConesVariable)) _unlockedCones = PlayerPrefs.GetString(UnlockedConesVariable);
        else _unlockedCones = _defaultUnlockedCones;

        return _unlockedCones;
    }

    private void SaveLevels()
    {
        PlayerPrefs.SetInt(CurrentLevelVariable, _currentLevel);

        PlayerPrefs.SetInt(PreviousLevelVariable, _previousLevel);

        PlayerPrefs.Save();
    }

    private void SaveOpeningConeProgress()
    {
        PlayerPrefs.SetInt(OpeningConeProgressVariable, _openingConeProgress);

        PlayerPrefs.Save();
    }

    private void SaveOpeningConeIndex()
    {
        PlayerPrefs.SetInt(OpeningConeIndexVariable, _openingConeIndex);

        PlayerPrefs.Save();
    }

    private void SaveNextConeIndex()
    {
        PlayerPrefs.SetInt(NextConeVariable, _nextCone);

        PlayerPrefs.Save();
    }

    private void SaveUnlockedCones()
    {
        PlayerPrefs.SetString(UnlockedConesVariable, _unlockedCones);

        PlayerPrefs.Save();
    }


    //private void SaveCurrentLevel()
    //{
    //    PlayerPrefs.SetInt(CurrentLevelVariable, _currentLevel);

    //    PlayerPrefs.Save();
    //}

    //private void SavePreviousLevel()
    //{
    //    PlayerPrefs.SetInt(previousLevelVariable, _previousLevel);

    //    PlayerPrefs.Save();
    //}

    private int LoadCurrentLevel()
    {
        if (PlayerPrefs.HasKey(CurrentLevelVariable)) _currentLevel = PlayerPrefs.GetInt(CurrentLevelVariable);
        else _currentLevel = _defaultLevel;

        return _currentLevel;
    }

    private int LoadPreviousLevel()
    {
        if (PlayerPrefs.HasKey(PreviousLevelVariable)) _currentLevel = PlayerPrefs.GetInt(PreviousLevelVariable);
        else _previousLevel = _defaultPreviousLevel;

        return _previousLevel;
    }
}
