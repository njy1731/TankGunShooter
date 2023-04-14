using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    [SerializeField] private float ZoomSpd = 0.03f;

    void Update()
    {
        Zoom();
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
}