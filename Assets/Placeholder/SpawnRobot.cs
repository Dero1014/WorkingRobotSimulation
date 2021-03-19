using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRobot : MonoBehaviour
{
    public GameObject robot;
    public GameObject handTool;
    public GameObject point;
    public RobotConsole robotConsole;
   
    
    private OpenConsole opC;
    private GameObject cloneRobot;
    private GameObject cloneHandTool;

    private Transform cloneTrans;

    private Vector3 mPos;
    private int robRank = 0;

    void Update()
    {
        Plane planeUp = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;

        if(planeUp.Raycast(ray, out distanceToPlane))
            mPos = ray.GetPoint(distanceToPlane);

        if (cloneRobot!=null) //when you have a robot follow mouse position
            cloneTrans.position = mPos;

        if (Input.GetMouseButtonDown(1)) //press right click to set the robot
            cloneRobot = null;
    }

    public void MakeRobot()
    {
        //spawn a robot
        cloneRobot = Instantiate(robot, mPos, Quaternion.identity);
        cloneRobot.name = "Robot " + robRank.ToString(); //name it
        cloneTrans = cloneRobot.GetComponent<Transform>(); //assign its transform
        cloneRobot.GetComponent<RobotManager>().rank = robRank; //set the rank
        opC = cloneRobot.GetComponent<OpenConsole>(); //get the OpenConsole script

        //assign its handtool
        cloneHandTool = Instantiate(handTool, new Vector3(cloneTrans.position.x, cloneTrans.position.y + 3.75f, cloneTrans.position.z), Quaternion.identity, cloneTrans); 
        cloneHandTool.name = "Hand Tool " + robRank.ToString(); //name it
        cloneHandTool.GetComponent<MoveTool_Script>().holdToHand = true; //set it to control the hand
        cloneRobot.GetComponent<RobotKinematicsTypeB>().target = cloneHandTool.transform;  //set the target from the hand
        cloneHandTool.gameObject.SetActive(false); //set the handtool to false 
        cloneHandTool.AddComponent<PointsCreator>().point = point;
        cloneHandTool.GetComponent<PointsCreator>().rank = robRank;
        cloneRobot.GetComponentInChildren<ToggleMoveHand_Script>().moveTool = cloneHandTool; // give it the cloned hand tool specified for the robot
        opC.handTrans = cloneHandTool.transform;

        //assign its console
        opC.console = robotConsole.gameObject;
        robotConsole.AddNewCodeSection();

        //stup the max distance
        cloneRobot.GetComponent<Robot_HandTool_KeepDistance>().maxDistance = 3f;
        cloneRobot.GetComponent<Robot_HandTool_KeepDistance>().minDistance = 0.5f;
        cloneRobot.GetComponent<Robot_HandTool_KeepDistance>().handTool = cloneHandTool.transform; //set the handtool

        robRank++;
        
    }

}
