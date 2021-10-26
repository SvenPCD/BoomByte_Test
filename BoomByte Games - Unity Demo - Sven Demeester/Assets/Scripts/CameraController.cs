using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform BallTransform;

    [SerializeField]
    private float Height = 1f;

    [SerializeField]
    private float _Speed = 100f;

    private Vector3 CamOffset;

    // Start is called before the first frame update
    void Start()
    {
        CamOffset = this.transform.position - BallTransform.position;
    }

    // Update is called once per frame
    void Update()
    {

            if (Input.GetKey(KeyCode.A))
            {
                CamOffset = Quaternion.AngleAxis(Time.deltaTime * -_Speed, Vector3.up) * CamOffset;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                CamOffset = Quaternion.AngleAxis(Time.deltaTime * _Speed, Vector3.up) * CamOffset;
            }

            this.transform.position = BallTransform.position + CamOffset;
            this.transform.LookAt(BallTransform.position + new Vector3(0, Height, 0));
    }
}
