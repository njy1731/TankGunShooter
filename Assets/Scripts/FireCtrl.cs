using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform firePoint; // �߻� ��ġ
    public float speed = 10f; // ����ü �ӵ�
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
