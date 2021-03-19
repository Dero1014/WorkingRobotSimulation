using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMoveHand_Script : MonoBehaviour
{
    public GameObject moveTool;

    public RobotManager roboMan;


    private void Start()
    {
        roboMan = GetComponentInParent<RobotManager>();
    }

    public bool moveToolActive = false;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.transform.tag == "Hand")
                {
                    if (hitObject.transform.GetComponentInParent<RobotManager>().rank != roboMan.rank)
                    {
                        moveToolActive = false;
                        moveTool.SetActive(false);
                    }
                    else
                    {
                        moveToolActive = !moveToolActive;
                        moveTool.SetActive(moveToolActive);
                    }

                }
                else
                {
                    if (hitObject.transform.tag != "Move Tool")
                    {
                        moveToolActive = false;
                        moveTool.SetActive(false);
                    }
                    else
                    {
                        if (hitObject.transform.GetComponentInParent<RobotManager>())
                        {
                            if (hitObject.transform.GetComponentInParent<RobotManager>().rank != roboMan.rank)
                            {
                                moveToolActive = false;
                                moveTool.SetActive(false);
                            }
                        }
                    }

                }
            }
        }
        
    }
}

