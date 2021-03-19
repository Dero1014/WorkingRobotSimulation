using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolToggles : MonoBehaviour
{
    public string toggleIns;
    public string toggleSpawn;
    public string toggleMoveTool;

    [Space(10)]
    public GameObject[] objects;

    [Space(10)]
    public InputField console;

    void Update()
    {

        if (!console.isFocused)
        {
            if (Input.GetKeyDown(toggleIns))
            {
                objects[0].SetActive(!objects[0].activeSelf);
            }

            if (Input.GetKeyDown(toggleSpawn))
            {
                objects[1].SetActive(!objects[1].activeSelf);
            }

            if (Input.GetKeyDown(toggleMoveTool))
            {
                objects[2].SetActive(!objects[2].activeSelf);
            }
        }

    }
}
