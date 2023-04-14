using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public float speed = 10f;           // 투사체 속도
    public float maxDistance = 10f;     // 투사체 최대 이동 거리
    public float fadeDuration = 1f;     // 투사체 사라지는 시간

    private float distanceTraveled;     // 이동한 거리
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

        // 이동한 거리 계산
        distanceTraveled += speed * Time.deltaTime;

        // 최대 이동 거리를 넘어섰을 경우
        if (distanceTraveled >= maxDistance && !isFading)
        {
            // 페이드 아웃 시작
            isFading = true;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        // 페이드 아웃 시간동안 점점 투명해지도록 색상 변경
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

        // 투사체 삭제
        Destroy(gameObject);
    }
}