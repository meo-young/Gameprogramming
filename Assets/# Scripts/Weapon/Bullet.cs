using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum FirePos
    {
        Right,
        Left
    }
    [Tooltip("발사 효과")]
    [SerializeField] GameObject flash;

    [Tooltip("타격 효과")]
    [SerializeField] GameObject hit;

    [Tooltip("총알 나가는 위치")]
    [SerializeField] Transform firePosition;

    [Tooltip("어떤 총알인지 판단하기위한 변수")]
    [SerializeField] int bulletInfo;


    Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        firePosition = GameObject.FindWithTag("FirePos").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        firePosition = GameObject.FindWithTag("FirePos").GetComponent<Transform>();

        rigidBody.velocity = firePosition.forward * 300;

        this.transform.position = firePosition.transform.position;
        this.transform.rotation = firePosition.transform.rotation;

        var flashInstance = Instantiate(flash, firePosition.position, Quaternion.identity);
        flashInstance.transform.forward = firePosition.forward;

        var flashPs = flashInstance.GetComponent<ParticleSystem>();
        if (flashPs != null)
        {
            Destroy(flashInstance, flashPs.main.duration);
        }
        else
        {
            var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(flashInstance, flashPsParts.main.duration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        switch(bulletInfo)
        {
            case 0:
                Instantiate(GameManager.instance.redBulletEffect, this.transform.position, transform.rotation);
                break;
            case 1:
                Instantiate(GameManager.instance.blueBulletEffect, this.transform.position, transform.rotation);
                break;
        }
        if (hit != null)
        {
            Vector3 hitPosition = this.transform.position;
            var hitInstance = Instantiate(hit, hitPosition, Quaternion.identity);

            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }
        if (this.gameObject.activeSelf)
        { this.gameObject.SetActive(false); }

    }
}
