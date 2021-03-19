using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotKinematics : MonoBehaviour
{
    public Transform[] joints;

    [Space(10)]
    public Transform target; // the position the robot is going for

    [Header("Hand Settings")]
    public Transform hand;

    [Space(10)]
    public float distanceFromTarget;

    [HideInInspector]public Vector3 fPoint = Vector3.zero;
    private float fDistance = 0;
    [Range(0,360)]public float fAngle = 0;

    
    private bool handClosed = false;

    void Update()
    {

        distanceFromTarget = (target.position - hand.position).magnitude;
        //print(distanceFromTarget);
        if (target != null)
        {
            Simulated();
            ShoulderRotation();
            ElbowRotation();

            TargetTracking();
        }

    }
    [Space(30)]
    [Range(-1, 1)] public float xx;
    [Range(-1,1)]public float yy;
   [Range(-1,1)] public float zz;

    #region rotations
    void Simulated()
    {
        //make predictions
        Vector3 directionFromElbowToHand = (hand.position - joints[1].position);
        Vector3 vForward = joints[1].forward;
        fPoint = joints[1].forward * directionFromElbowToHand.magnitude;
        float xF = Mathf.Cos(Mathf.Deg2Rad * (fAngle + 90))-1;
        float yF = Mathf.Sin(Mathf.Deg2Rad * (fAngle + 90));

        print("X : " + xF + " YF : " + yF);

        vForward = new Vector3(vForward.x, vForward.y + yF, vForward.z +xF);
        directionFromElbowToHand =  Quaternion.Euler(joints[1].forward*fAngle) * directionFromElbowToHand;
        fPoint = directionFromElbowToHand + (joints[1].position);


        fDistance = (fPoint - target.position).magnitude;

    }

    void ShoulderRotation()
    {
    }


    void ElbowRotation()
    {
    }

    #endregion


    #region handControl

    private Transform targetGrab;
    public void CloseHand()
    {
        if (!handClosed)
        {
            handClosed = true;

            Collider[] col = Physics.OverlapSphere(hand.position, 1f, LayerMask.GetMask("Item"));
            print(col[0]);
            targetGrab = col[0].GetComponent<Transform>();
            targetGrab.parent = hand;
        }
    }

    public void OpenHand()
    {
        handClosed = false;
        if (targetGrab != null)
            targetGrab.parent = null;
    }

    #endregion

    #region TARGET FOLLOW
    [HideInInspector] public bool commandFulfilled = false;
    [HideInInspector] public bool track = false;
    void TargetTracking()//check how close it is to the targeted position
    {
        
    }
    #endregion
}
