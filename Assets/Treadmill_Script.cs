using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill_Script : MonoBehaviour
{
    public Transform treadmill;
    public Transform[] legs;

    [Space(10)]
    public float treadSpeed;

    [Space(10)]
    public float lenght;
    public float width;

    void Update()
    {

        legs[0].localPosition = new Vector3(0 + (treadmill.localScale.x / 2), 0, 0 + (treadmill.localScale.z / 2));
        legs[1].localPosition = new Vector3(0 - (treadmill.localScale.x / 2), 0, 0 + (treadmill.localScale.z / 2));
        legs[2].localPosition = new Vector3(0 + (treadmill.localScale.x / 2), 0, 0 - (treadmill.localScale.z / 2));
        legs[3].localPosition = new Vector3(0 - (treadmill.localScale.x / 2), 0, 0 - (treadmill.localScale.z / 2));

    }

    Rigidbody targetRb;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            targetRb = collision.gameObject.GetComponent<Rigidbody>();
            targetRb.velocity = treadmill.forward * treadSpeed * Time.deltaTime;
        }
    }

}
