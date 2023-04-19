using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 플레이어 오브젝트
    public float smoothSpeed = 0.125f;
    public float maxDistance = 5.0f; // 카메라가 플레이어에서 최대로 멀어질 수 있는 거리

    private Camera mainCamera;
    private Vector3 offset;
    private bool isRightClicking = false;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        offset = transform.position - target.position;
    }

    void LateUpdate()
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