using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    private static RecordManager _Instance;
    public static RecordManager Instance { get { return _Instance; } }

    private ObjectRecording[] objectRecordings;
    int _Index = 0;
    int _FramesRecorded;

    public bool HasRecorded = false;

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _Instance = this;
    }

    private void Start()
    {
        objectRecordings = FindObjectsOfType<ObjectRecording>();
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.IsReplaying)
        {
            foreach(ObjectRecording recording in objectRecordings)
            {
                recording.SetTransform(_Index);
            }
            _Index++;

            if(_FramesRecorded <= _Index)
            {
                foreach (ObjectRecording recording in objectRecordings)
                {
                    recording.TurnOnRigidBody();
                }
                _Index = 0;
                GameManager.Instance.IsReplaying = false;
                Time.timeScale = 1;
            }
        }
        else if(GameManager.Instance.IsRecording)
        {
            foreach (ObjectRecording recording in objectRecordings)
            {
                recording.AddRecordFrame();
            }
            _FramesRecorded++;
        }
    }

    public void StartRecording()
    {
        _FramesRecorded = 0;
        foreach (ObjectRecording recording in objectRecordings)
        {
            recording.ResetRecording();
        }
        GameManager.Instance.IsRecording = true;
    }

    public void StopRecording()
    {
        GameManager.Instance.IsRecording = false;
        HasRecorded = true;
    }

    public void SkipReplay()
    {
        _Index = _FramesRecorded - 1;
        foreach (ObjectRecording recording in objectRecordings)
        {
            recording.SetTransform(_Index);
            recording.TurnOnRigidBody();
        }

        _Index = 0;
        GameManager.Instance.IsReplaying = false;
        Time.timeScale = 1;
    }

    public void StartReplay()
    {
        _Index = 0;

        MatchManager.Instance.CanScore = false;
        GameManager.Instance.IsRecording = false;
        GameManager.Instance.IsReplaying = true;
    }

    public void StartSlowReplay()
    {
        _Index = 0;

        MatchManager.Instance.CanScore = false;
        GameManager.Instance.IsRecording = false;
        GameManager.Instance.IsReplaying = true;
        Time.timeScale = 0.5f;
    }
}
