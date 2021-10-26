using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform _CameraTrans;
    [SerializeField]
    private float _MaxHitSpeed = 5;
    [SerializeField]
    private float _MinHitSpeed = 1;

    private float _CurrentSpeed = 0;
    private float _HitStrength = 0;

    private bool _IsGoingUp = false;

    [SerializeField]
    private Slider _PowerSlider;

    [SerializeField]
    private LineRenderer _Targeting;


    private bool _IsYellowHit = false;
    private bool _IsRedHit = false;
    
    void Update()
    {

        if (!GameManager.Instance.IsBallRolling && !GameManager.Instance.IsReplaying)
        {
            Vector3 Direction = this.transform.position - _CameraTrans.position;
            Direction.y = 0;

            MatchManager.Instance.CanScore = true;
            _IsYellowHit = false;
            _IsRedHit = false;

            if (Input.GetKey(KeyCode.Space))
            {
                _PowerSlider.gameObject.SetActive(true);

                if (_IsGoingUp)
                {
                    _HitStrength += Time.deltaTime;

                    if (_HitStrength >= 1) _IsGoingUp = false;
                }
                else
                {
                    _HitStrength -= Time.deltaTime;

                    if (_HitStrength <= 0) _IsGoingUp = true;
                }

                _PowerSlider.value = _HitStrength;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                _CurrentSpeed = Mathf.Lerp(_MinHitSpeed, _MaxHitSpeed, _HitStrength);

                GetComponent<Rigidbody>().velocity = Direction * _CurrentSpeed;

                _HitStrength = 0;
                RecordManager.Instance.StartRecording();
                MatchManager.Instance.AddShot();
            }

            Ray ray = new Ray(transform.position, Direction);
            RaycastHit hitData;
            Physics.Raycast(ray, out hitData);

            if (Physics.Raycast(ray, out hitData))
            {
                _Targeting.gameObject.SetActive(true);
                _Targeting.transform.position = hitData.point;
                _Targeting.SetPosition(0, this.transform.position);
                _Targeting.SetPosition(1, hitData.point);
            }
        }
        else
        {
            _Targeting.gameObject.SetActive(false);
            _PowerSlider.gameObject.SetActive(false);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {

        if (MatchManager.Instance.CanScore)
        {
            if (collision.gameObject.tag == "YellowBall")
            {
                _IsYellowHit = true;
                if (_IsRedHit)
                {
                    MatchManager.Instance.GainPoint();
                    _IsYellowHit = false;
                    _IsRedHit = false;
                }
            }
            else if(collision.gameObject.tag == "RedBall")
            {
                _IsRedHit = true;
                if (_IsYellowHit)
                {
                    MatchManager.Instance.GainPoint();
                    _IsYellowHit = false;
                    _IsRedHit = false;
                }
            }
        }
    }

}
