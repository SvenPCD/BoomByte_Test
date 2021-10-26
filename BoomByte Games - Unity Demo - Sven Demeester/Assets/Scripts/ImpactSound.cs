using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource _Sound;

    private Rigidbody _Rigidbody;

    void Start()
    {
        Rigidbody RB = GetComponent<Rigidbody>();
        if(RB != null)
        {
            _Rigidbody = RB;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Floor")
        {
            _Sound.volume = _Rigidbody.velocity.magnitude / 30;
            _Sound.Play();
        }
    }
}
