using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationKineTest : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        
    }

    void Update()
    {
        if (target!=null)
        {
            Vector3 direction = (target.position - transform.position);
            direction.y = 0;
            transform.LookAt(target,Vector3.down);
        }
    }
}
