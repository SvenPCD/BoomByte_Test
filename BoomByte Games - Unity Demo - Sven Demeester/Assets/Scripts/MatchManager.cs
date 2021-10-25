using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    private static MatchManager _instance;
    public static MatchManager Instance { get { return _instance; } }


    private BallStopper[] Balls;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Balls = FindObjectsOfType<BallStopper>();

        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
}
