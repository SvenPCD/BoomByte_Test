using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    [SerializeField]
    private Text ScoreboardTxt;
    [SerializeField]
    private GameObject ReplayButton;


    private void Update()
    {
        if (!GameManager.Instance.IsBallRolling) ReplayButton.SetActive(true);
        else ReplayButton.SetActive(false);
    }


    private void Start()
    {
        UpdateScoreBoard(0, 0, 0);
    }

    public void StartReplay()
    {
        RecordManager.Instance.StartReplay();
    }

    public void UpdateScoreBoard(int ShotCount, int ScoreCount, float timecount)
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

        ScoreboardTxt.text = SB.ToString();
    }


}
