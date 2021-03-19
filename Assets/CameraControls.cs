using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float sensitivity=0; //Koliko će se brzo micat kamera u odnosu na miš

    [Header("Zoom settings")]
    public float zoomSensitivity=0; //Koliko će brzo zumirat u odnosu na micanja kotača na mišu
    [Tooltip("For forward movemnt of ctrl + scroll")]public float moveSpeed = 0; //brzina micanja kamere naprijed/nazad
    
    [Space(10)]
    public float minZoom = 0; //minimalni zoom
    public float maxZoom = 0; //maksimalni zoom

    [Header("Pan settings")]
    public float panSpeed = 0; //brzina micanja miša

    [Header("Camera settings")]
    [Tooltip("The distance the camera will be on focus")]public float focusDistance =0;

    float rotX = 0;
    float rotY = 0;
    void Start()
    {
        rotX = transform.localEulerAngles.x;
        rotY = transform.localEulerAngles.y;
    }


    void Update()
    {
        //I just clumped everything together

        if (Input.GetKey(KeyCode.Mouse1))
        {
            CameraRotation();
            if (Input.GetKey(KeyCode.LeftAlt))
                CameraFocus();
        }
        else if (Input.GetKey(KeyCode.Mouse2))
            CameraPan();
        else if(Input.GetKeyDown(KeyCode.Mouse0))
            CheckObject();
        else if (Input.GetKeyDown(KeyCode.LeftAlt))
            CameraFocus();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            CameraMove();
        }
        else
        {
            CameraZoom();
        }

    }

    void CameraRotation()
    {
        float xMouse = Input.GetAxisRaw("Mouse X");
        float yMouse = Input.GetAxisRaw("Mouse Y");

        float yR = xMouse * sensitivity * Time.deltaTime;
        float xR = yMouse * sensitivity * Time.deltaTime;

        rotX -= xR;
        rotY += yR;
        
        //for better look at the rotation 
        //if (Mathf.Abs(rotX)>360)
        //{
        //    if (Mathf.Sign(rotX)==1)
        //        rotX -= 360;
        //    else
        //        rotX += 360;
        //}

        transform.localEulerAngles = new Vector3(rotX, rotY, 0);
    }

    void CameraZoom()
    {
        float scrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
        float zoom = scrollWheel * zoomSensitivity * Time.deltaTime; 

        Camera.main.fieldOfView -= zoom;

        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);

    }

    void CameraMove()
    {

        float move = Input.GetAxisRaw("Mouse ScrollWheel") * moveSpeed *Time.deltaTime;

        transform.position += (transform.forward * move);

    }

    void CameraPan()
    {
        float xMouseDrag = Input.GetAxisRaw("Mouse X");
        float yMouseDrag = Input.GetAxisRaw("Mouse Y");

        transform.position += (-transform.right * xMouseDrag * panSpeed * Time.deltaTime);
        transform.position += (-transform.up * yMouseDrag * panSpeed * Time.deltaTime);

    }

    private Transform target; //last clicked on target
    void CameraFocus() //focusing on the object
    {
        if (target!=null)
        {
            Vector3 focusPosition = target.position;
            focusPosition += (-transform.forward * focusDistance);
            transform.position = focusPosition;
        }
    }

    void CheckObject() //for having an object to focus on
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            if (hit.transform.tag != "Move Tool")
                target = hit.transform;

        }

        while (target.parent!=null) //checks the ultimate parent
        {
            if (target.parent != null)
                target = target.parent;
        }

    }

}
