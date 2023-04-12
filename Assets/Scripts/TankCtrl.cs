using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCtrl : MonoBehaviour
{
    public float forwardSpeed = 5.0f; // 탱크의 전진 속도
    public float reverseSpeed = 2.0f; // 탱크의 후진 속도
    public float forwardAcceleration = 2.0f; // 탱크의 전진 가속력
    public float reverseAcceleration = 1.0f; // 탱크의 후진 가속력
    public float deceleration = 4.0f; // 탱크의 감속력
    public float rotationSpeed = 180.0f; // 탱크의 회전 속도
    public float rotationDeceleration = 2.0f; // 탱크의 회전 감속력

    private Rigidbody2D rb; // 탱크의 리지드바디 컴포넌트
    private float horizontalInput; // 수평 입력값
    private float verticalInput; // 수직 입력값

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnGUI()
    {
        // 탱크의 현재 속도를 텍스트로 표시 (색상: 검은색, 크기: 24)
        GUI.contentColor = Color.black; // 텍스트 색상을 검은색으로 설정
        GUI.skin.label.fontSize = 24; // 텍스트 크기를 24로 설정
        Rect labelRect = new Rect(10, 10, 200, 30); // 렉트의 크기와 위치를 수정
        GUI.Label(labelRect, "Speed: " + rb.velocity.magnitude.ToString("0.00"));
    }

    private void Update()
    {
        // 수평 및 수직 입력값을 받아옴
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // 탱크의 회전 로직
        float rotationAngle = -horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        float targetRotationSpeed = rotationSpeed;
        if (Mathf.Abs(horizontalInput) < 0.1f) // 입력값이 0에 가까우면 회전 감속
        {
            targetRotationSpeed = Mathf.Lerp(rotationSpeed, 0.0f, rotationDeceleration * Time.fixedDeltaTime);
        }
        rb.rotation += rotationAngle * (rotationSpeed / targetRotationSpeed);

        // 탱크의 이동 로직
        Vector2 moveDirection = Quaternion.Euler(0, 0, rb.rotation) * Vector2.up;
        float targetSpeed = 0.0f;
        float accelerationRate = 0.0f;
        if (verticalInput > 0)
        {
            targetSpeed = forwardSpeed * verticalInput;
            accelerationRate = forwardAcceleration;
        }
        else if (verticalInput < 0)
        {
            targetSpeed = reverseSpeed * verticalInput;
            accelerationRate = reverseAcceleration;
        }
        float currentSpeed = Vector2.Dot(rb.velocity, moveDirection);
        float speed = Mathf.Lerp(currentSpeed, targetSpeed, accelerationRate * Time.fixedDeltaTime);

        // 움직이는 도중에 차체를 회전할 때, 속도를 느리게 감소
        if (Mathf.Abs(horizontalInput) > 0.1f && Mathf.Abs(verticalInput) > 0.1f)
        {
            float speedDecreaseAmount = 2f; // 속도 감소량 (예시: 0.02는 1프레임당 속도의 2%씩 감소)
            speed -= speed * speedDecreaseAmount * Time.fixedDeltaTime;
        }

        rb.velocity = moveDirection * speed;

        // 탱크의 감속 로직
        if (Mathf.Abs(verticalInput) < 0.1f) // 입력값이 0에 가까우면 감속
        {
            speed = Mathf.Lerp(speed, 0.0f, deceleration * Time.fixedDeltaTime);
            rb.velocity = moveDirection * speed;
        }
    }
}