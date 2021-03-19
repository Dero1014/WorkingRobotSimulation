using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RobotKinematicsTypeB))]
public class TypeBEditor : Editor
{
    public void OnSceneGUI()
    {
        RobotKinematicsTypeB rk = (RobotKinematicsTypeB)target;
        Handles.color = Color.red;
        Handles.DrawSphere(0, rk.hand.position, Quaternion.identity, rk.range);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RobotKinematicsTypeB rk = (RobotKinematicsTypeB)target;

        if (GUILayout.Button("DoAll"))
        {
            rk.SegmentFollow();
        }

    }
}
