using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_HandTool_KeepDistance : MonoBehaviour
{
    public Transform handTool;
    public float maxDistance;
    public float minDistance;

    private Vector3 direction;
    private float distance;
    private float difDis;

    void Update()
    {
        direction = handTool.position - transform.position;
        distance = direction.magnitude;
        direction = direction.normalized;

        if (distance > maxDistance)
        {
            difDis = maxDistance - distance;
            handTool.localPosition += (direction * difDis);
        }

        if (distance < minDistance)
        {
            difDis = minDistance - distance;
            handTool.localPosition += (direction * difDis);
        }

        if (handTool.localPosition.y < -0.5f)
        {
            handTool.localPosition = new Vector3(handTool.localPosition.x, -0.5f, handTool.localPosition.z);
        }
        

    }
}
