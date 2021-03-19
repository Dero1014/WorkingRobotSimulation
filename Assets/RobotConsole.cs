using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotConsole : MonoBehaviour
{
    public InputField console;
    public RobotCommands rCom;
    public TextMeshProUGUI conRobName;
    
    public List<string> codes = new List<string>();

    public int robotRank = 0;
    //public bool insertNew = false;

    private void Start()
    {
        rCom = FindObjectOfType<RobotCommands>(); // need to change this for multiple robots
    }


    string consoleText;
    public void CompileCode()
    {
        consoleText = console.text;
        char[] c = consoleText.ToCharArray();
        
        rCom.commands = new List<string>(); //whenever we compile we should remake the list of commands being sent

        string currentText = null; //the text we are currently looking at
        for (int i = 0; i < console.text.Length; ++i) //check if there are any commands in the console text
        {
            
            if (c[i].ToString()!="\n")
                currentText += c[i].ToString();
                
            if ((c[i].ToString() == "\n" && currentText != "" ) || i==console.text.Length-1) //save the command and send it
            {
                rCom.commands.Add(currentText);
                currentText = "";
            }         
          
        }

        //here is where you add the repeat fcn from the old script
        rCom.commandLine = 0;
    }

    public void ShowText()
    {
        console.text = codes[robotRank];
    }

    public void SaveText()
    {
        if (console.text != "")
            codes[robotRank] = console.text;
    }


    public void AddNewCodeSection()
    {
        codes.Add("CODE HERE"); //add a new code into the list so it opens up the code at open console
    }

    public void StartCommand()
    {
        rCom.FullReset();
    }

}
