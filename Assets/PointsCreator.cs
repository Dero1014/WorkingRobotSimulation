using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is actually kinda fine but I need to change to what point should the robot chose cuz two robots can't have P1 tho maybe I think I should make an overlap sphere that takes into account 
//how close are the points to the robot so it focuses on those points so it will take into account those points who are closest to it (GO TO UNITY ASSETS IDEAS)

//also what happenes when I remove a point, I might not do that but it would cool to reset the numbering so if you have P100, P135 and P200 and you press reset number you get P0,P1,P0
public class PointsCreator : MonoBehaviour
{
    public GameObject point;

    public int rank = 0;

    int num = 0; //contains the number of points
    void Update()
    {
        RobotConsole console = FindObjectOfType<RobotConsole>();

        if (Input.GetKeyDown("p") && console==null) // create a point and give it a name
        {
            GameObject clone = Instantiate(point, transform.position, Quaternion.identity);
            clone.name = "P" + num.ToString();
            clone.GetComponent<PointRank>().rank = rank;
            num++;
        }
    }
}
