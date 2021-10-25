using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    private static RecordManager _instance;
    public static RecordManager Instance { get { return _instance; } }

    private ObjectRecording[] objectRecordings;
    int index = 0;
    int FramesRecorded;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
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
                recording.SetTransform(index);
            }
            index++;

            if(FramesRecorded == index)
            {
                foreach (ObjectRecording recording in objectRecordings)
                {
                    recording.TurnOnRigidBody();
                }
                index = 0;
                GameManager.Instance.IsReplaying = false;
            }
        }
        else if(GameManager.Instance.IsRecording)
        {
            foreach (ObjectRecording recording in objectRecordings)
            {
                recording.AddRecordFrame();
            }
            FramesRecorded++;
        }
    }

    public void StartRecording()
    {
        foreach (ObjectRecording recording in objectRecordings)
        {
            recording.ResetRecording();
        }
        GameManager.Instance.IsRecording = true;
    }

    public void StopRecording()
    {
        GameManager.Instance.IsRecording = false;
    }


    public void StartReplay()
    {
        index = 0;

        GameManager.Instance.IsRecording = false;
        GameManager.Instance.IsReplaying = true;
    }
}
