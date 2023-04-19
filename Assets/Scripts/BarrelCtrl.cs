using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //public float speed = 10f;           // 투사체 속도
    public float maxDistance = 5f;     // 투사체 최대 이동 거리

    public ParticleSystem fireEffect;
    [SerializeField] private float ZoomSpd = 0.03f;

    void Update()
    {
        Zoom();
        Fire();
    }

    void Zoom()
    {
        if (Input.GetMouseButton(1))
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3.5f, 0.03f);
        }

        else
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 4.5f, 0.03f);
        }
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fireEffect.Play();
        }
    }
}