 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RobotKinematics))]
public class Kinematics_Editor : Editor
{
    private void OnSceneGUI()
    {
        RobotKinematics rk = (RobotKinematics)target;

        Handles.color = Color.black;

        Handles.SphereHandleCap(0,rk.fPoint, Quaternion.identity, 0.2f,EventType.Repaint);
        Handles.DrawLine(rk.joints[1].position, rk.fPoint);
    }
}
