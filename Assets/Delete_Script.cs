using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Script : MonoBehaviour
{

    private GameObject target;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check for what axis has been selected
        {
            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.transform.gameObject.layer != 0)
                {
                    target = hitObject.transform.gameObject;
                    while (target.transform.parent != null)
                    {
                        target = target.transform.parent.gameObject;
                    }
                }
                else
                {
                    target = null;
                }
            }
        }

        if (target != null)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                target.SetActive(false);
                print("Delete");
            }
        }
    }

}
