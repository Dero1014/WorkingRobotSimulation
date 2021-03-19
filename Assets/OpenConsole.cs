using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need to change how we use move script still have to think about it
public class OpenConsole : MonoBehaviour
{
    public GameObject console; //should find a way to make a console for a spawned robot and assign the console to the robot
    public RobotCommands rcc;
    public Transform handTrans;

    [HideInInspector]public RobotKinematicsTypeB rk;

    bool consoleActive = false;

    private void Start()
    {
        rk = GetComponent<RobotKinematicsTypeB>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hitRobot;


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hitRobot))
            {
                if (hitRobot.transform.gameObject.layer == 9 && hitRobot.transform.GetComponentInParent<RobotManager>().rank == gameObject.GetComponent<RobotManager>().rank && hitRobot.transform.name != "Hand") //check if you clicked on the robot if you did open its console
                {
                    consoleActive = !consoleActive;

                    //if the console is active and we are switching between robots reset it so it updates
                    if (console.GetComponent<RobotConsole>().conRobName.text != gameObject.name && !consoleActive)
                        consoleActive = !consoleActive;

                    console.SetActive(consoleActive); //activate it
                    console.GetComponent<RobotConsole>().SaveText();
                    console.GetComponent<RobotConsole>().conRobName.text = gameObject.name; //set its name
                    console.GetComponent<RobotConsole>().rCom = gameObject.GetComponent<RobotCommands>(); //give it the robot command center 
                    console.GetComponent<RobotConsole>().robotRank = gameObject.GetComponent<RobotManager>().rank; //give it the rank of the robot to access the code
                    console.GetComponent<RobotConsole>().ShowText(); //show the code of the robot1
                    //rcc.enabled = consoleActive;
                }
            }
        }

        if (!console.activeSelf && handTrans.gameObject.activeSelf) //if the console isn't active then set the target for the robot to the hand (THIS WORKS BUT ONLY WHEN THE HAND TRANS IS ACTIVE)
            rk.target = handTrans;
        
    }

}
