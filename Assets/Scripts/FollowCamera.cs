using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 플레이어 오브젝트
    public float smoothSpeed = 0.125f;
    public float maxDistance = 5.0f; // 카메라가 플레이어에서 최대로 멀어질 수 있는 거리

    private Camera mainCamera;
    public float cameraSpeed = 5f; // 카메라 이동 속도
    public float smoothTime = 0.1f; // 부드러운 이동을 위한 시간
    public Vector3 offset; // 카메라와 대상의 거리
    private Vector3 velocity = Vector3.zero; // 이동 속도를 저장할 변수
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
    //    // 캐릭터가 이동하는 방향과 속도를 구한다.
    //    Vector2 targetVelocity = targetRigidbody.velocity;
    //    Vector3 targetDirection = targetVelocity.normalized;
    //    float targetSpeed = targetVelocity.magnitude;

    //    // 카메라가 이동할 위치를 계산한다.
    //    Vector3 targetPosition = target.position + offset;
    //    Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / cameraSpeed);

    //    // 카메라와 대상 사이의 거리를 계산한다.
    //    Vector3 cameraToTarget = newPosition - targetPosition;
    //    float distanceToTarget = cameraToTarget.magnitude;

    //    // 캐릭터가 가속할 때 카메라의 이동 속도를 조절한다.
    //    float targetMoveSpeed = cameraSpeed;
    //    if (targetSpeed > cameraSpeed)
    //    {
    //        targetMoveSpeed *= targetSpeed / cameraSpeed;
    //    }

    //    // 캐릭터가 회전할 때 카메라의 회전 속도를 줄인다.
    //    float targetRotationSpeed = 1f;
    //    if (targetSpeed > 0f)
    //    {
    //        targetRotationSpeed = Mathf.Clamp01(Mathf.Abs(targetRigidbody.angularVelocity) / targetSpeed);
    //    }

    //    // 카메라를 새로운 위치로 이동한다.
    //    transform.position = newPosition;

    //    // 카메라의 회전을 부드럽게 조절한다.
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
