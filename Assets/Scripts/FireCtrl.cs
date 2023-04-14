using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 발사 위치
    public float speed = 10f; // 투사체 속도
    public ParticleSystem fireEffect;

    private void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fireEffect.Play();
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
