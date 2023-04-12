using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    public Transform tankTurretTransform; // 탱크 포탑의 Transform 컴포넌트
    public float rotationSpeedDegreesPerSecond = 45.0f; // 초당 회전 속도

    void Update()
    {
        // 마우스 위치를 월드 좌표로 가져오기
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 월드 좌표의 z 값은 0으로 고정

        // 탱크 포탑의 현재 위치와 마우스 위치를 이용하여 반대 방향을 향하는 회전 각도 계산
        Vector3 direction = tankTurretTransform.position - mousePosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 목표 회전 각도를 Quaternion으로 변환
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // 초당 회전 속도에 맞게 회전 각도 보간
        float step = rotationSpeedDegreesPerSecond * Time.deltaTime;
        tankTurretTransform.rotation = Quaternion.RotateTowards(tankTurretTransform.rotation, targetRotation, step);
    }
}
