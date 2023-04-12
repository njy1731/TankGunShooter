using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    public Transform tankTurretTransform; // ��ũ ��ž�� Transform ������Ʈ
    public float rotationSpeedDegreesPerSecond = 45.0f; // �ʴ� ȸ�� �ӵ�

    void Update()
    {
        // ���콺 ��ġ�� ���� ��ǥ�� ��������
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // ���� ��ǥ�� z ���� 0���� ����

        // ��ũ ��ž�� ���� ��ġ�� ���콺 ��ġ�� �̿��Ͽ� �ݴ� ������ ���ϴ� ȸ�� ���� ���
        Vector3 direction = tankTurretTransform.position - mousePosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ��ǥ ȸ�� ������ Quaternion���� ��ȯ
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // �ʴ� ȸ�� �ӵ��� �°� ȸ�� ���� ����
        float step = rotationSpeedDegreesPerSecond * Time.deltaTime;
        tankTurretTransform.rotation = Quaternion.RotateTowards(tankTurretTransform.rotation, targetRotation, step);
    }
}
