using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject item;
    public GameObject treadMill;
    [Space(10)]
    public GameObject itemPanel;

    private Transform clone;

    private Vector3 mPos;

    private int num = 0;
    private int numTread = 0;
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

    public void MakeItem()
    {
        clone = Instantiate(item, mPos, Quaternion.identity).transform;
        clone.name = "Item " + num.ToString();
        num++;
    }

    public void MakeTreadMill()
    {
        clone = Instantiate(treadMill, mPos, Quaternion.identity).transform;
        clone.name = "TreadMill " + numTread.ToString();
        numTread++;
    }

    public void ToggleItems()
    {
        itemPanel.SetActive(!itemPanel.activeSelf);
    }
}
