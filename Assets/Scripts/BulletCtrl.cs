using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float bulletSpd = 5f;

    private void Start()
    {
        DestroyBullet();
    }

    void Update()
    {
        transform.Translate(Vector3.up * 0.7f);
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject, 3f);
        BarrelCtrl.isFire = false;
    }
}
