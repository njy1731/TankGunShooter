using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public float speed = 10f;           // ����ü �ӵ�
    public float maxDistance = 10f;     // ����ü �ִ� �̵� �Ÿ�
    public float fadeDuration = 1f;     // ����ü ������� �ð�

    private float distanceTraveled;     // �̵��� �Ÿ�
    private bool isFading;

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
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2, 0.03f);
        }

        else
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 4, 0.03f);
        }
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fireEffect.Play();
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * speed;
        }

        // �̵��� �Ÿ� ���
        distanceTraveled += speed * Time.deltaTime;

        // �ִ� �̵� �Ÿ��� �Ѿ�� ���
        if (distanceTraveled >= maxDistance && !isFading)
        {
            // ���̵� �ƿ� ����
            isFading = true;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        // ���̵� �ƿ� �ð����� ���� ������������ ���� ����
        float elapsedTime = 0f;
        Color color = GetComponent<Renderer>().material.color;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            color.a = alpha;
            GetComponent<Renderer>().material.color = color;
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        // ����ü ����
        Destroy(gameObject);
    }
}