using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControl : MonoBehaviour
{
    [Tooltip ("The distance at which the shoulder move towards or away from the target")]public float minDistance;
    public float maxDistance;

    [Space(10)]
    public Transform target; // the position the robot is going for

    [Header("Base settings")]
    public Transform robotBase;
    public float baseSpeed;
    public float minBaseAngle;

    [Header("Shoulder settings")]
    public Transform robotShoulder;
    public float maxAngleDeviation;
    public float shoulderSpeed;

    [Header("Elbow settings")]
    public Transform robotElbow;
    public float minElbowAngle;
    public float elbowSpeed;

    [Header("Hand Settings")]
    public Transform hand;
    
    [Space(10)]
    public float minDistanceFromTarget=0;// don't forget to set the distance for positions 


    private Vector3 rE = Vector3.zero;
    private Vector3 rS = Vector2.zero;
    private Vector3 rB = Vector3.zero;

    private bool handClosed = false;

    private void Start()
    {
        if (robotBase != null)
            rB = robotBase.localEulerAngles;
        if (robotShoulder != null)
            rS = robotShoulder.localEulerAngles;
        if (robotElbow!=null)
            rE = robotElbow.localEulerAngles;
    }

    void Update()
    {

        if (target!=null)
        {
            BaseRotation();
            ShoulderRotation();
            ElbowRotation();

            TargetTracking();
        }

    }

    #region rotations
    void BaseRotation()
    {
        Vector3 direction = (target.position - robotBase.position).normalized;
        direction.y = 0;

        Vector3 robotBody = robotBase.forward;
        robotBody.y = 0;

        float angle = Vector3.SignedAngle(robotBody, direction, robotBase.up);

        if (Mathf.Abs(angle)>minBaseAngle)
            rB.y += (baseSpeed * Time.deltaTime * Mathf.Sign(angle));

        if (Mathf.Abs(angle)< minBaseAngle)
            rB.y += angle;

        robotBase.localEulerAngles = rB;

    }

    void ShoulderRotation()
    {
        Vector3 distance = target.position - robotElbow.position;
        float angl = Vector3.SignedAngle(robotShoulder.up, distance, robotShoulder.right);

        if (Mathf.Sqrt(distance.sqrMagnitude) <= minDistance)
            robotShoulder.localEulerAngles = new Vector3(robotShoulder.localEulerAngles.x - (shoulderSpeed * Time.deltaTime), robotShoulder.localEulerAngles.y, robotShoulder.localEulerAngles.z);

        if (Mathf.Sqrt(distance.sqrMagnitude) >= maxDistance && (angl > 5 || angl < -5))
        {
            if (angl > 5)
                robotShoulder.localEulerAngles = new Vector3(robotShoulder.localEulerAngles.x + (shoulderSpeed * Time.deltaTime), robotShoulder.localEulerAngles.y, robotShoulder.localEulerAngles.z);
            if (angl < -5)
                robotShoulder.localEulerAngles = new Vector3(robotShoulder.localEulerAngles.x - (shoulderSpeed * Time.deltaTime), robotShoulder.localEulerAngles.y, robotShoulder.localEulerAngles.z);
        }

        float X = robotShoulder.localEulerAngles.x;
        if (X > 180)
            X -= 360;

        X = Mathf.Clamp(X, -maxAngleDeviation, maxAngleDeviation);
        robotShoulder.localEulerAngles = new Vector3(X, robotShoulder.localEulerAngles.y, robotShoulder.localEulerAngles.z);
    }


    void ElbowRotation()
    {
        Vector3 elbowDirection = (target.position - robotElbow.position).normalized;
        elbowDirection.x = 0;

        Vector3 robotForeArm = robotElbow.up;
        robotForeArm.x = 0;

        float angle = Vector3.SignedAngle(robotForeArm, elbowDirection, robotElbow.right);

        if (Mathf.Abs(angle) > minElbowAngle)
            rE.x += (elbowSpeed * Time.deltaTime * Mathf.Sign(angle));

        if (Mathf.Abs(angle) < minElbowAngle)
            rE.x += angle;
        
        robotElbow.localEulerAngles = rE;

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

    public  void OpenHand()
    {
        handClosed = false;
        if(targetGrab!=null)
            targetGrab.parent = null;
    }

    #endregion


    [HideInInspector]public bool commandFulfilled = false;
    [HideInInspector] public bool track = false;
    void TargetTracking()//check how close it is to the targeted position
    {
        Vector3 distanceFromTarget = target.position - hand.position;
        if (distanceFromTarget.magnitude<minDistanceFromTarget && track)
            commandFulfilled = true;
        else
            commandFulfilled = false;

    } 


    Quaternion bR;
    Quaternion sR;
    Quaternion eR;
    public void SetStartPosition() //set the position it will start from when ran
    {
        bR = robotBase.rotation;
        sR = robotShoulder.rotation;
        eR = robotElbow.rotation;
    }

    public void StartPosition() //start in this position 
    {
        robotBase.rotation = bR;
        robotShoulder.rotation = sR;
        robotElbow.rotation = eR;
    }


}
