using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreboardTxt;
    [SerializeField]
    private GameObject _ReplayButton;
    [SerializeField]
    private GameObject _SkipButton;
    [SerializeField]
    private GameObject _WinMenu;

    private void Update()
    {
        if (!GameManager.Instance.IsBallRolling && !GameManager.Instance.IsReplaying && RecordManager.Instance.HasRecorded) _ReplayButton.SetActive(true);
        else _ReplayButton.SetActive(false);

        if (GameManager.Instance.IsReplaying) _SkipButton.SetActive(true);
        else _SkipButton.SetActive(false);
    }


    private void Start()
    {
        UpdateScoreBoard(0, 0, 0);
        _WinMenu.SetActive(false);
    }

    public void StartReplay()
    {
        RecordManager.Instance.StartReplay();
    }

    public void SlowReplay()
    {
        RecordManager.Instance.StartSlowReplay();
    }

    public void SkipReplay()
    {
        RecordManager.Instance.SkipReplay();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.ReturnToMenu();
    }

    public void ActivateWinMenu()
    {
        _WinMenu.SetActive(true);
    }

    public void UpdateScoreBoard(int ShotCount, int ScoreCount, int timecount)
    {
        StringBuilder SB = new StringBuilder();
        SB.Append("Score: ");
        SB.Append(ScoreCount);
        SB.Append("\n");
        SB.Append("ShotCount: ");
        SB.Append(ShotCount);
        SB.Append("\n");
        SB.Append("Time: ");
        SB.Append(timecount);
        SB.Append(" s");

        _ScoreboardTxt.text = SB.ToString();
    }


}
