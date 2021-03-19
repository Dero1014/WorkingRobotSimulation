using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxySensor_Script : MonoBehaviour
{
    public bool state = false;
    [Header("Sensor Settings")]
    public float distanceDetection;
    public Transform sensorCapsule;

    void Update()
    {
        sensorCapsule.localScale = new Vector3(sensorCapsule.localScale.x, distanceDetection, sensorCapsule.localScale.z);

        sensorCapsule.localPosition = new Vector3(distanceDetection, 0, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag!="Move Tool")
        {
            state = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        state = false;
    }
    
}
