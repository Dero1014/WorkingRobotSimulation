using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSensors : MonoBehaviour
{
    public GameObject proxy;
    public GameObject laser;

    public GameObject panel;

    private Transform clone;

    private Vector3 mPos;

    private int numL = 0;
    private int numP = 0;

    // Update is called once per frame
    void Update()
    {
        Plane planeUp = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distanceToPlane;

        if (planeUp.Raycast(ray, out distanceToPlane))
            mPos = ray.GetPoint(distanceToPlane);

        if (clone != null)
            clone.position = mPos;

        if (Input.GetMouseButtonDown(1)) //press right click to set the robot
            clone = null;
    }

    public void MakeLaser()
    {
        clone = Instantiate(laser, mPos, Quaternion.identity).transform;
        clone.name = "LASER" + numL.ToString();
        numL++;
    }

    public void MakeProxy()
    {
        clone = Instantiate(proxy, mPos, Quaternion.identity).transform;
        clone.name = "PROXY" + numP.ToString();
        numP++;
    }

    public void ToggleSensors()
    {
        panel.SetActive(!panel.activeSelf);
    }

}
