using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRecording : MonoBehaviour
{

    private List<RecordFrame> _RecordedFrames = new List<RecordFrame>();
    private Rigidbody _RigidBody;

    private void Start()
    {
        if(GetComponent<Rigidbody>() != null)
        {
            _RigidBody = GetComponent<Rigidbody>();
        }
    }

    public void ResetRecording() { _RecordedFrames.Clear(); }

    public void AddRecordFrame()
    {
        _RecordedFrames.Add(new RecordFrame { position = transform.position, rotation = transform.rotation });
    }

    public void SetTransform(int index)
    {
        if (index < _RecordedFrames.Count)
        {
            RecordFrame rf = _RecordedFrames[index];

            if (_RigidBody != null)
            {
                _RigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                _RigidBody.isKinematic = true;
            }

            transform.position = rf.position;
            transform.rotation = rf.rotation;
        }
    }

    public void TurnOnRigidBody()
    {
        if (_RigidBody != null)
        {
            _RigidBody.isKinematic = false;
            _RigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }

}
