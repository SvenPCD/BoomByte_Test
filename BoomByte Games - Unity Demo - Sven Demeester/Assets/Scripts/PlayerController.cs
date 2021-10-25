using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform CameraTrans;
    [SerializeField]
    private float MaxHitSpeed = 5;
    [SerializeField]
    private float MinHitSpeed = 1;

    private float CurrentSpeed = 0;
    private float HitStrength = 0;

    private bool IsGoingUp = false;

    [SerializeField]
    private Slider PowerSlider;

    [SerializeField]
    private LineRenderer Targeting;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsBallRolling && !GameManager.Instance.IsReplaying)
        {
            Vector3 Diretion = this.transform.position - CameraTrans.position;
            Diretion.y = 0;

            if (Input.GetKey(KeyCode.Space))
            {
                PowerSlider.gameObject.SetActive(true);

                if (IsGoingUp)
                {
                    HitStrength += Time.deltaTime;

                    if (HitStrength >= 1) IsGoingUp = false;
                }
                else
                {
                    HitStrength -= Time.deltaTime;

                    if (HitStrength <= 0) IsGoingUp = true;
                }

                PowerSlider.value = HitStrength;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                CurrentSpeed = Mathf.Lerp(MinHitSpeed, MaxHitSpeed, HitStrength);

                GetComponent<Rigidbody>().velocity = Diretion * CurrentSpeed;

                HitStrength = 0;
                RecordManager.Instance.StartRecording();
            }

            Ray ray = new Ray(transform.position, Diretion);
            RaycastHit hitData;
            Physics.Raycast(ray, out hitData);

            if (Physics.Raycast(ray, out hitData))
            {
                Targeting.gameObject.SetActive(true);
                Targeting.transform.position = hitData.point;
                Targeting.SetPosition(0, this.transform.position);
                Targeting.SetPosition(1, hitData.point);
            }
            Debug.DrawRay(transform.position, Diretion * 10);

            
        }
        else
        {
            Targeting.gameObject.SetActive(false);
            PowerSlider.gameObject.SetActive(false);
        }

    }
    
}
