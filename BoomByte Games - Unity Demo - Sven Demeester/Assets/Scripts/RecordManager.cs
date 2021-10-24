using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    private ObjectRecording[] objectRecordings;
    int index = 0;
    int FramesRecorded;

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
                GameManager.Instance.IsReplaying = false;
                index = 0;
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
