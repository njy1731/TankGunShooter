using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // �÷��̾� ������Ʈ
    public float smoothSpeed = 0.125f;
    public float maxDistance = 5.0f; // ī�޶� �÷��̾�� �ִ�� �־��� �� �ִ� �Ÿ�

    private Camera mainCamera;
    public float cameraSpeed = 5f; // ī�޶� �̵� �ӵ�
    public float smoothTime = 0.1f; // �ε巯�� �̵��� ���� �ð�
    public Vector3 offset; // ī�޶�� ����� �Ÿ�
    private Vector3 velocity = Vector3.zero; // �̵� �ӵ��� ������ ����
    private bool isRightClicking = false;
    private Rigidbody2D targetRigidbody;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        offset = transform.position - target.position;
        targetRigidbody = target.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        Zoom();
    }

    private void FixedUpdate()
    {
        //Follow();
    }

    //void Follow()
    //{
    //    // ĳ���Ͱ� �̵��ϴ� ����� �ӵ��� ���Ѵ�.
    //    Vector2 targetVelocity = targetRigidbody.velocity;
    //    Vector3 targetDirection = targetVelocity.normalized;
    //    float targetSpeed = targetVelocity.magnitude;

    //    // ī�޶� �̵��� ��ġ�� ����Ѵ�.
    //    Vector3 targetPosition = target.position + offset;
    //    Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / cameraSpeed);

    //    // ī�޶�� ��� ������ �Ÿ��� ����Ѵ�.
    //    Vector3 cameraToTarget = newPosition - targetPosition;
    //    float distanceToTarget = cameraToTarget.magnitude;

    //    // ĳ���Ͱ� ������ �� ī�޶��� �̵� �ӵ��� �����Ѵ�.
    //    float targetMoveSpeed = cameraSpeed;
    //    if (targetSpeed > cameraSpeed)
    //    {
    //        targetMoveSpeed *= targetSpeed / cameraSpeed;
    //    }

    //    // ĳ���Ͱ� ȸ���� �� ī�޶��� ȸ�� �ӵ��� ���δ�.
    //    float targetRotationSpeed = 1f;
    //    if (targetSpeed > 0f)
    //    {
    //        targetRotationSpeed = Mathf.Clamp01(Mathf.Abs(targetRigidbody.angularVelocity) / targetSpeed);
    //    }

    //    // ī�޶� ���ο� ��ġ�� �̵��Ѵ�.
    //    transform.position = newPosition;

    //    // ī�޶��� ȸ���� �ε巴�� �����Ѵ�.
    //    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, -targetDirection);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, targetRotationSpeed);
    //}
    
    void Zoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRightClicking = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRightClicking = false;
        }

        Vector3 desiredPosition;
        if (isRightClicking)
        {
            desiredPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y));
            desiredPosition.z = transform.position.z;
            desiredPosition = new Vector3(Mathf.Clamp(desiredPosition.x, target.position.x - maxDistance, target.position.x + maxDistance),
                                          Mathf.Clamp(desiredPosition.y, target.position.y - maxDistance, target.position.y + maxDistance),
                                          desiredPosition.z);
        }
        else
        {
            desiredPosition = target.position + offset;
            desiredPosition = new Vector3(Mathf.Clamp(desiredPosition.x, target.position.x - maxDistance, target.position.x + maxDistance),
                                          Mathf.Clamp(desiredPosition.y, target.position.y - maxDistance, target.position.y + maxDistance),
                                          desiredPosition.z);
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
