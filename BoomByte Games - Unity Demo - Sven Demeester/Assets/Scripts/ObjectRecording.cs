using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRecording : MonoBehaviour
{

    private List<RecordFrame> recordedFrames = new List<RecordFrame>();
    private Rigidbody rigidBody;

    private void Start()
    {
        if(GetComponent<Rigidbody>() != null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
    }

    public void ResetRecording() { recordedFrames.Clear(); }

    public void AddRecordFrame()
    {
        recordedFrames.Add(new RecordFrame { position = transform.position, rotation = transform.rotation });
    }

    public void SetTransform(int index)
    {
        RecordFrame rf = recordedFrames[index];

        transform.position = rf.position;
        transform.rotation = rf.rotation;
    }

}
