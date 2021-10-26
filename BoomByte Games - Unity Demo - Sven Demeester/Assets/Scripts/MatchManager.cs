using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    private static MatchManager _Instance;
    public static MatchManager Instance { get { return _Instance; } }


    private BallStopper[] Balls;

    public bool CanScore = true;

    [SerializeField]
    private int _ScoreToWin = 3;

    private int _Score = 0;
    private int _Shots = 0;

    private IngameUI _InGameUI;
    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        IngameUI IGUI = FindObjectOfType<IngameUI>();

        if (IGUI != null)
        {
            _InGameUI = IGUI;
        }

        Balls = FindObjectsOfType<BallStopper>();

        _Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsBallRolling)
        {
            if (AreBallsStopped())
            {
                GameManager.Instance.IsBallRolling = false;
                RecordManager.Instance.StopRecording();
            }
        }

        if(_ScoreToWin > _Score)
        _InGameUI.UpdateScoreBoard(_Shots, _Score, (int)Time.timeSinceLevelLoad);
    }

    public bool AreBallsStopped()
    {
        bool AllStopped = true;

        foreach (BallStopper ball in Balls)
        {
            if(!ball.IsStopped)
            {
                AllStopped = false;
            }
        }

        return AllStopped;
    }

    public void GainPoint() 
    {
        if (CanScore)
        {
            _Score++;
            CanScore = false;

            if(_Score >= _ScoreToWin)
            {
                ActivateWinScreen();
            }
        }
    }

    public void AddShot() 
    {
        _Shots++; 
    }

    public void WriteFile()
    {

        GameResult Test = new GameResult();

        Test.Score = _Score;
        Test.ShotsMade = _Shots;
        Test.TimeSpent = (int)Time.timeSinceLevelLoad;

        string jsonString = JsonUtility.ToJson(Test);
        File.WriteAllText(GameManager.Instance.SaveFile, jsonString);
    }


    void ActivateWinScreen()
    {
        _InGameUI.ActivateWinMenu();
        _InGameUI.UpdateScoreBoard(_Shots, _Score, (int)Time.timeSinceLevelLoad);
        WriteFile();
    }
}
