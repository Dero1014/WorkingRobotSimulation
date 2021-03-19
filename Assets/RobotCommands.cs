using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//my lib
using ExtraString;
public class RobotCommands : MonoBehaviour
{

    public RobotKinematicsTypeB rk;
    public RobotManager rm;
    [Space(10)]
    public List<string> commands;

    [Space(10)]
    public List<VariableType> variables;

    public int commandLine;

    private Math_Script math; //to hold the long ass code somewhere else
    private void Start()
    {
        math = gameObject.AddComponent<Math_Script>();
        rk = GetComponent<RobotKinematicsTypeB>();
    }

    public void FullReset() //CALL THIS WHEN YOU WANNA START EVERYTHING
    {
        variables = new List<VariableType>();
        rk.target = null;
        commandLine = 0;
        ResetRepeatVariables();
        CancelInvoke();
        ResetLoopVariables();
        RunCommand();
       
    }
   
    public void RunCommand()
    {
        if (commandLine >= commands.Count)
            commandLine = 0;

        string commandText = commands[commandLine];
        char[] c = commandText.ToCharArray();
        
        bool selectedCommand = false;

        for (int i = 0; i < commandText.Length; i++)
        {

            #region READ VARIABLES

            if (c[i].ToString() == "I" && c[i + 1].ToString() == "N" && c[i + 2].ToString() == "T" && !selectedCommand)
            {

                int num = i + 3;
                ReadInt(c, num);
                selectedCommand = true;
            }

            #endregion

            #region FUNCTIONS

            if (commandText.Contains("REPEAT") && !selectedCommand)
            {
                RepeatNumber(commandText);
                SaveLine();
                selectedCommand = true;
            }

            if (commandText.Contains("REND") && !selectedCommand)
            {
                RepeatLine();
                selectedCommand=true;
            }

            if (commandText.Contains("IF") && !selectedCommand)
            {
                CheckCondition(commandText);
                selectedCommand = true;
            }

            if (commandText.Contains("IEND") && !selectedCommand)
            {
                commandLine++;
                if (commandLine < commands.Count)
                    RunCommand();
                selectedCommand = true;
            }

            if (commandText.Contains("LOOP") && !selectedCommand)
            {
                Looping();
                selectedCommand = true;
            }

            if (commandText.Contains("BREAK") && !selectedCommand)
            {
                BreakLoop();
                selectedCommand = true;
            }

            if (commandText.Contains("LEND") && !selectedCommand)
            {
                LoopIt();
                selectedCommand = true;
            }


            #endregion

            #region  WRITE VARIABLES

            if (!selectedCommand)
            {
                for (int j = 0; j < variables.Count; ++j)
                {
                    if (c[i].ToString() == variables[j].varName)
                    {
                        DoMath(c, j);
                        selectedCommand = true;
                    }
                }
            }

            #endregion

            #region BASIC COMMANDS
            if (!selectedCommand)
            {
                switch (c[i].ToString())
                {
                    case "M": MoveCommand(c); selectedCommand = true; break;
                    case "W": selectedCommand = true;  WaitCommand(c);  break;
                    case "G": GrabCommand(); selectedCommand = true; break;
                    case "R": ReleaseCommand(); selectedCommand = true; break;
                    case "S": SpeedCommand(c); selectedCommand = true; break;

                    default:
                        break;
                }
            }

            #endregion

        }
    }

    #region Commands

    bool moving;
    Transform target;
    void MoveCommand(char[] c)
    {
        PointRank[] points = FindObjectsOfType<PointRank>();

        for (int i = 0; i < c.Length; i++) //need to do
        {
            if (c[i].ToString() == "P")
            {
                for (int j = 0; j < variables.Count; j++)
                {
                    if (c[i + 1].ToString() == variables[j].varName)
                    {
                        string name = c[i].ToString() + variables[j].number.ToString();
                        foreach (PointRank point in points)
                        {
                            if (point.rank == rm.rank)
                            {
                                if (point.gameObject.name == name)
                                {

                                    target = point.gameObject.transform;
                                    rk.target = target;
                                    rk.track = true;
                                }
                            }
                        }
                        break;
                    }
                }

                for (int j = 0; j < 9; j++)
                {
                    if (c[i + 1].ToString() == j.ToString())
                    {
                        string name = c[i].ToString() + c[i + 1].ToString();

                        foreach (PointRank point in points)
                        {
                            if (point.rank == rm.rank)
                            {
                                if (point.gameObject.name == name)
                                {
                                    target = point.gameObject.transform;
                                    rk.target = target;
                                    rk.track = true;
                                }
                            }
                        }
                        
                        break;
                    }
                }
            }
        }

        moving = true;
    }

    void WaitCommand(char[] c)
    {
        string secondsText = null;
        float waitSeconds = 0f;

        bool ms = false;
        for (int i = 0; i < c.Length; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (c[i].ToString() == j.ToString())
                    secondsText += c[i].ToString();
            }

            if (c[i].ToString() == "S" && !ms)
                waitSeconds = float.Parse(secondsText);

            if (c[i].ToString() == "M" && c[i + 1].ToString() == "S")
            {
                waitSeconds = float.Parse(secondsText);
                waitSeconds = waitSeconds / 1000;
                ms = true;
            }

        }

        print("We are waiting for : " + waitSeconds);

        Invoke("WaitComplete", waitSeconds);

    }

    void GrabCommand()
    {
        rk.CloseHand();
        commandLine++;
        if (commandLine < commands.Count)
            RunCommand();
    }

    void ReleaseCommand()
    {
        rk.OpenHand();
        commandLine++;
        if (commandLine < commands.Count)
            RunCommand();
    }

    void SpeedCommand(char[] c)
    {
        float speed = 0;
        string speedText = null;
        //string speedDecimalText = null; WILL ADD LATER
        //bool isDecimal = false;
        for (int i = 0; i < c.Length; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (c[i].ToString() == j.ToString())
                    speedText += c[i].ToString();
            }
        }

        speed = float.Parse(speedText); //change to number

        print("Current speed is : " + speed);

        rk.robotSpeed = speed;

        commandLine++;
        if (commandLine < commands.Count)
            RunCommand();

    }

    #endregion


    private void Update()
    {
        if (moving)
        {
            if (rk.commandFulfilled)
            {
                commandLine++;
                rk.track = false;
                if (commandLine<commands.Count)
                {
                    moving = false;
                    RunCommand();
                }   
            }
        }
    }

    #region SetStartPosition
    public void SavePosition()
    {
        //rk.SetStartPosition();
    }

    void StartFrom()
    {
        //rk.StartPosition();
    }
    #endregion

    #region Invokes

    void WaitComplete()
    {
        print("Done waiting");
        commandLine++;
        if (commandLine < commands.Count)
            RunCommand();
    }

    #endregion

    #region Variables

    void ReadInt(char[] c, int num)
    {
        string varName = null;
        string varNumber = null;

        bool lookingForNumbers=false;
        for (int i = num; i < c.Length; ++i)
        {
            if (c[i].ToString()!=" ")
            {
                if (c[i].ToString()=="=")
                    lookingForNumbers = true;

                if (!lookingForNumbers)
                    varName += c[i];

                else if(lookingForNumbers && c[i].ToString() != "=" && c[i].ToString()!=";")
                    varNumber += c[i].ToString();

            }
        }


        VariableType varType = new VariableType();
        varType.Variable(varName, int.Parse(varNumber));

        variables.Add(varType);
        commandLine++;
        if (commandLine < commands.Count)
            RunCommand();

    }

    void DoMath(char[] c, int j)
    {

        variables[j].number = math.Calculation(c, j);

        commandLine++;
        if (commandLine < commands.Count)
            RunCommand();

    }

    #endregion

    #region Functions Functions

    #region Repeat
    [Space(50)]
    List<Function_REPEAT> repeaters = new List<Function_REPEAT>();
    int r = -1;

    void ResetRepeatVariables() //use for when reruning the script
    {
        repeaters = new List<Function_REPEAT>();
        r = -1;
        return;
    }

    void RepeatNumber(string lineText) //get the necessery number from the ()
    {
        Function_REPEAT rep = new Function_REPEAT();

        string numText = CropString.CropBetween(lineText, "(" , ")");

        rep.repeatNumber = int.Parse(numText);

        repeaters.Add(rep);
        r++;
    }

    void SaveLine() //save the line we are starting from
    {
        repeaters[r].beginingRepeatLine = commandLine;

        commandLine++;
        if (commandLine < commands.Count)
            RunCommand();
    }

    void RepeatLine() //do this every time we hit an end
    {
        repeaters[r].endRepeatLine = commandLine;

        commandLine = repeaters[r].beginingRepeatLine;

        if (repeaters[r].repeatNumber!=00)
        {
            repeaters[r].repeated++;
        }

        if (repeaters[r].repeated>=repeaters[r].repeatNumber && repeaters[r].repeatNumber!=00)
        {
            commandLine = repeaters[r].endRepeatLine;
            repeaters[r].repeated = 0;
            r--;
        }
        commandLine++;

        if (commandLine < commands.Count)
            RunCommand();
    }
    #endregion

    #region IF

    public Function_IF iferMain = new Function_IF();
    bool its = false;
    void CheckCondition(string lineText)
    {
        string condition = CropString.CropBetween(lineText, "(", ")");
        print("Check " + condition);
        if (condition.Contains("PROXY")) 
        {
            ProxySensor_Script prox = GameObject.Find(condition).GetComponent<ProxySensor_Script>();
            its = prox.state;
        }
        else
            its = math.Compareision(condition);

        print(its);

        Function_IF ifer = new Function_IF();
        ifer.condition = its;
        iferMain = ifer;
        CheckingCondition();
    }

    void CheckingCondition()
    {
        int skip = 0;

        if (iferMain.condition)
        {
            commandLine++;
            if (commandLine < commands.Count)
                RunCommand();
        }

        while (!iferMain.condition)
        {
            commandLine++;
            string commandText = commands[commandLine];
            char[] c = commandText.ToCharArray();

            if (commandText.Contains("IF"))
                skip++;

            if (commandText.Contains("IEND"))
            {
                if (skip<=0)
                {
                    iferMain.condition = true;
                    commandLine++;
                    if (commandLine < commands.Count)
                        RunCommand();
                    break;
                }
                skip--;
            }

        }
    }

    #endregion

    #region Loop

    int loopLine = 0;
    List<Function_Loop> loops = new List<Function_Loop>();
    int l = -1;

    void ResetLoopVariables() //use for when reruning the script
    {
        loops = new List<Function_Loop>();
        l = -1;
        return;
    }

    void Looping()
    {
        Function_Loop loop = new Function_Loop();
        loop.beginingLine = commandLine;

        loops.Add(loop);
        l++;

        commandLine++;
        if (commandLine < commands.Count)
            RunCommand();
    }

    void BreakLoop()
    {
        if (l>=0)
        {
            if (loops[l].endLine != 0)
            {
                commandLine = loops[l].endLine;
                commandLine++;
                l--;
                if (commandLine < commands.Count)
                    RunCommand();
            }
            else
            {
                bool foundEndLoop = false;
                while (!foundEndLoop)
                {
                    commandLine++;
                    string commandText = commands[commandLine];
                    if (commandText.Contains("LEND"))
                    {
                        l--;
                        commandLine++;
                        if (commandLine < commands.Count)
                            RunCommand();
                    }

                }
            }
        }
    }

    void LoopIt()
    {
        loops[l].endLine = commandLine;
        commandLine = loops[l].beginingLine;
        commandLine++;
        if (commandLine < commands.Count)
            RunCommand();
    }

    #endregion

    #endregion

    /*
        commandLine++;
        if (commandLine < commands.Count)
            RunCommand();
            */
}
