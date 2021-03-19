using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetection_Script : MonoBehaviour
{
    public LayerMask IgnoreMe;

    private Transform[] muzzle;
    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        muzzle = GetComponentsInChildren<Transform>();
    }

    public float distance;
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(muzzle[1].position, muzzle[1].right, out hit, Mathf.Infinity, ~IgnoreMe))
        {
            line.widthMultiplier = 0.1f;
            line.SetPosition(0, muzzle[1].position);
            line.SetPosition(1, hit.point);
            Vector3 direction = hit.point - muzzle[1].position;
            distance = Mathf.Sqrt(direction.sqrMagnitude);
        }
        else
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
    }
}
