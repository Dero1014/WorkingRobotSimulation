using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rtool is going good but it will need some time until we got it just right
public class RotTool_Script : MonoBehaviour
{
    public Transform target;
    public GameObject[] planes;
    public float multi;
    public float rotSpeed;
    [Space(10)]
    public float maxAngle;

    Transform[] graphic;

    Vector3 direction = Vector3.zero;

    bool rotActive = false;
    bool xAxis = false;
    bool yAxis = false;
    bool zAxis = false;
    bool rotationChecked = false;

    private void Start()
    {
        graphic = GetComponentsInChildren<Transform>();

        graphic[1].gameObject.SetActive(false);

    }

    void Update()
    {
        FindTargetedObject();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check for what axis has been selected
        {
            GetMousePosition();

            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.transform.tag == "Rot Tool")
                {
                    rotActive = true;

                    if (hitObject.transform.name == "X")
                        xAxis = true;
                    else if (hitObject.transform.name == "Y")
                        yAxis = true;
                    else if (hitObject.transform.name == "Z")
                        zAxis = true;

                }
            }
        }


        if (Input.GetKey(KeyCode.Mouse0)) //IF HELD YOU CAN MOVE IT
            MoveTool();
        else //IF ITS NOT HELD THEN NOTHING IS PICKED
        {
            xAxis = false;
            yAxis = false;
            zAxis = false;
            rotActive = false;
            rotationChecked = false;
        }

    }

    void FindTargetedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check for what axis has been selected
        {
            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.transform.gameObject != null && hitObject.transform.name != "X" && hitObject.transform.name != "Y" && hitObject.transform.name != "Z" && !hitObject.transform.GetComponentInParent<RobotCommands>())
                {
                    target = hitObject.transform;
                    transform.position = target.position;
                    graphic[1].gameObject.SetActive(true);
                }

                if (hitObject.transform.GetComponentInParent<RobotCommands>())
                    graphic[1].gameObject.SetActive(false);

            }
            else
            {
                graphic[1].gameObject.SetActive(false);
            }
        }


        if (target != null && graphic[1].gameObject.activeSelf)
        {
            ResizeMoveTool();
        }


    }

    bool xPlane = false;
    bool yPlane = false;
    bool zPlane = false;
    float oldDistance = 0;
    float xR =0;
    float yR = 0;
    float zR = 0;
    void MoveTool()
    {
        if (rotActive)
        {
            //move on X axis
            if (xAxis)
            {
                Vector3 mousePosition = GetMousePosition(); //get the mouse position
                
                if (!rotationChecked) //set the difference between the mouse and the origin point
                {
                    xR = target.localEulerAngles.x;
                    yR = target.localEulerAngles.y;
                    zR = target.localEulerAngles.z;
                    rotationChecked = true;
                }
                float distance = (mousePosition - transform.position).magnitude;
                float sign = 1;
                direction = (mousePosition - transform.position).normalized;

                float angle = 0;
                float mouseDrag = Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"));

                if (distance>=oldDistance)
                    sign = 1;
                else
                    sign = - 1;

                if (zPlane)
                {
                    angle = Vector3.SignedAngle(transform.right, direction, (Camera.main.transform.position - transform.position).normalized);
                }
                else
                {
                    angle = Vector3.SignedAngle(transform.forward, direction, (Camera.main.transform.position - transform.position).normalized);
                    angle = -angle * Mathf.Sign(angle) + 90;
                }

                print(sign  );
             
                

                if (mouseDrag!=0)
                {
                    xR += (rotSpeed * Mathf.Sign(angle) * Time.deltaTime * sign);

                    target.transform.rotation = Quaternion.Euler(xR, yR, zR);
                }

                oldDistance = distance;

            }

            /*
            if (yAxis)
            {
                Vector3 mousePosition = GetMousePosition(); //get the mouse position

                if (!distanceChecked) //set the difference between the mouse and the origin point
                {
                    distanceChecked = true;
                    distance = mousePosition - transform.position;
                }
                print("This is y");

                transform.position = new Vector3(transform.position.x, mousePosition.y - distance.y, transform.position.z); //apply the movement
            }

            if (zAxis)
            {
                Vector3 mousePosition = GetMousePosition(); //get the mouse position

                if (!distanceChecked) //set the difference between the mouse and the origin point
                {
                    distanceChecked = true;
                    distance = mousePosition - transform.position;
                }

                print("This is z");

                transform.position = new Vector3(transform.position.x, transform.position.y, mousePosition.z - distance.z); //apply the movement
            }
            */
        }
    }

    //resizing the move tool to fit object size
    void ResizeMoveTool()
    {
        float xMax = target.localScale.x;
        float yMax = target.localScale.x;
        float zMax = target.localScale.x;

        float max = 0;

        if (xMax >= max)
            max = xMax;

        if (yMax >= max)
            max = yMax;

        if (zMax >= max)
            max = zMax;

        if (target.localScale.x*multi >= graphic[1].localScale.x || target.localScale.y*multi >= graphic[1].localScale.y || target.localScale.z * multi >= graphic[1].localScale.z)
            graphic[1].localScale = Vector3.one * max * multi;
        else
            graphic[1].localScale = Vector3.one;
    }

    #region mousePosition

    Vector3 pos;

    Vector3 GetMousePosition()
    {
        //Use of planes to determain the position of the mouse relative to the axis we are using
        Plane mousePlane = new Plane();
        //Plane planeY = new Plane(Vector3.forward, transform.position);

        Vector3 camPos = Camera.main.transform.position;
        float max = 0;
       
        Vector3 dirX = (camPos - transform.position).normalized;
        float angleX = Vector3.Angle(dirX, transform.right);
        angleX = Mathf.Abs(angleX - 90);
        Vector3 dirY = (camPos - transform.position).normalized;
        float angleY = Vector3.Angle(dirY, transform.up);
        angleY = Mathf.Abs(angleY - 90);
        Vector3 dirZ = (camPos - transform.position).normalized;
        float angleZ = Vector3.Angle(dirZ, transform.forward);
        angleZ = Mathf.Abs(angleZ - 90);

        if (angleX >= max)
            max = angleX;
        if (angleY >= max)
            max = angleY;
        if (angleZ >= max)
            max = angleZ;

        if (max == angleX)
        {
            mousePlane = new Plane(transform.right, transform.position);
            xPlane = true;
            yPlane = false;
            zPlane = false;
        }
        else if (max == angleY)
        {
            mousePlane = new Plane(transform.up, transform.position);
            xPlane = false;
            yPlane = true;
            zPlane = false;
        }
        else if (max == angleZ)
        {
            mousePlane = new Plane(transform.forward, transform.position);
            xPlane = false;
            yPlane = false;
            zPlane = true;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //for more effective results we use two planes that cover the parts that it needs to calculate for that axis 

        float distanceToPlane;

        if (mousePlane.Raycast(ray, out distanceToPlane))
            pos = ray.GetPoint(distanceToPlane);

        return pos;
    }

    #endregion
}
