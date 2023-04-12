using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCtrl : MonoBehaviour
{
    public float forwardSpeed = 5.0f; // ��ũ�� ���� �ӵ�
    public float reverseSpeed = 2.0f; // ��ũ�� ���� �ӵ�
    public float forwardAcceleration = 2.0f; // ��ũ�� ���� ���ӷ�
    public float reverseAcceleration = 1.0f; // ��ũ�� ���� ���ӷ�
    public float deceleration = 4.0f; // ��ũ�� ���ӷ�
    public float rotationSpeed = 180.0f; // ��ũ�� ȸ�� �ӵ�
    public float rotationDeceleration = 2.0f; // ��ũ�� ȸ�� ���ӷ�

    private Rigidbody2D rb; // ��ũ�� ������ٵ� ������Ʈ
    private float horizontalInput; // ���� �Է°�
    private float verticalInput; // ���� �Է°�

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnGUI()
    {
        // ��ũ�� ���� �ӵ��� �ؽ�Ʈ�� ǥ�� (����: ������, ũ��: 24)
        GUI.contentColor = Color.black; // �ؽ�Ʈ ������ ���������� ����
        GUI.skin.label.fontSize = 24; // �ؽ�Ʈ ũ�⸦ 24�� ����
        Rect labelRect = new Rect(10, 10, 200, 30); // ��Ʈ�� ũ��� ��ġ�� ����
        GUI.Label(labelRect, "Speed: " + rb.velocity.magnitude.ToString("0.00"));
    }

    private void Update()
    {
        // ���� �� ���� �Է°��� �޾ƿ�
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // ��ũ�� ȸ�� ����
        float rotationAngle = -horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        float targetRotationSpeed = rotationSpeed;
        if (Mathf.Abs(horizontalInput) < 0.1f) // �Է°��� 0�� ������ ȸ�� ����
        {
            targetRotationSpeed = Mathf.Lerp(rotationSpeed, 0.0f, rotationDeceleration * Time.fixedDeltaTime);
        }
        rb.rotation += rotationAngle * (rotationSpeed / targetRotationSpeed);

        // ��ũ�� �̵� ����
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

        // �����̴� ���߿� ��ü�� ȸ���� ��, �ӵ��� ������ ����
        if (Mathf.Abs(horizontalInput) > 0.1f && Mathf.Abs(verticalInput) > 0.1f)
        {
            float speedDecreaseAmount = 2f; // �ӵ� ���ҷ� (����: 0.02�� 1�����Ӵ� �ӵ��� 2%�� ����)
            speed -= speed * speedDecreaseAmount * Time.fixedDeltaTime;
        }

        rb.velocity = moveDirection * speed;

        // ��ũ�� ���� ����
        if (Mathf.Abs(verticalInput) < 0.1f) // �Է°��� 0�� ������ ����
        {
            speed = Mathf.Lerp(speed, 0.0f, deceleration * Time.fixedDeltaTime);
            rb.velocity = moveDirection * speed;
        }
    }
}