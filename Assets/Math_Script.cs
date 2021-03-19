using System.Collections;
using System.Collections.Generic;
using MathLibrary;
using UnityEngine;

//this script holds a part of the code for robot commands to make it easier to read
//overall this script might need some better management
public class Math_Script : MonoBehaviour
{
    public RobotCommands rc;

    private void Start()
    {
        rc = GetComponent<RobotCommands>();
    }

    public int Calculation(char[] c, int j)
    {
        int result = 0; //result is used for a bit cleaner view

        string textNumA = null; //holds A and B part of the text that has numbers or variables
        string textNumB = null;
        string mathOperator = null;

        bool lookingForB = false;
        bool doMath = false; //signifies when a = is found to see what it needs to do
        bool foundVariableA = false; //check if its a variable 
        bool foundVariableB = false; //check if its another variable

        for (int i = 0; i < c.Length; ++i)
        {
            if (c[i].ToString() == "=")
            {
                doMath = true;
            }

            if (doMath && c[i].ToString() != "=" && c[i].ToString() != "+" && c[i].ToString() != "-" && c[i].ToString() != "*" && c[i].ToString() != "/") //search for the numbers a and b
            {

                if (!lookingForB && !foundVariableA)
                {
                    textNumA += c[i].ToString();

                    for (int k = 0; k < rc.variables.Count; ++k)
                    {
                        if (c[i].ToString() == rc.variables[k].varName)
                        {
                            textNumA = rc.variables[k].number.ToString();
                            foundVariableA = true;
                        }
                    }
                }



                if (lookingForB && !foundVariableB)
                {
                    textNumB += c[i].ToString();

                    for (int h = 0; h < rc.variables.Count; ++h)
                    {
                        if (c[i].ToString() == rc.variables[h].varName)
                        {
                            textNumB = rc.variables[h].number.ToString();
                            foundVariableB = true;
                            print("Number B is = " + textNumB);
                        }
                    }
                }

            }

            if (c[i].ToString()=="+" || c[i].ToString() == "-" || c[i].ToString() == "*" || c[i].ToString() == "/")
            {
                mathOperator = c[i].ToString();
                lookingForB = true;
            }

        }
        print(textNumA);
        print(textNumB);

        if (textNumB == null)
        {
            result = int.Parse(textNumA);
            return result;
        }

        result = MathOperator.Calculate(int.Parse(textNumA), int.Parse(textNumB),mathOperator);
       
        return result;

    }

    public bool Compareision(string c)
    {
        string textA=null;
        string textB=null;

        bool foundA = false;
        bool foundB = false;

        for (int i = 0; i < c.Length; ++i)
        {
            //look for laser
            if (c[i].ToString()=="L")
            {
                print(i);
            }
            if (c[i].ToString()=="L" && c[i+1].ToString() == "A" && c[i+2].ToString() == "S" && c[i+3].ToString() == "E" && c[i+4].ToString() == "R")
            {
                string laserName = "LASER";
                for (int j = i+5; j < c.Length; j++)
                {
                    if (int.TryParse(c[j].ToString(), out int n))
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            if (c[j].ToString() == k.ToString())
                            {
                                laserName += c[j].ToString();
                                print(laserName);
                            }
                        }
                    }
                    else
                    {
                        LaserDetection_Script laser = GameObject.Find(laserName).GetComponent<LaserDetection_Script>();
                        print("Time to set it");
                        if (!foundA)
                        {
                            foundA = true;
                            textA = laser.distance.ToString();
                            i = j;
                        }
                        else
                        {
                            textB = laser.distance.ToString();
                        }
                        break;
                    }

                    if (foundA)
                    {
                        LaserDetection_Script laser = GameObject.Find(laserName).GetComponent<LaserDetection_Script>();
                        textB = laser.distance.ToString();
                    }

                }
            }

            //look for variable
            for (int j = 0; j < rc.variables.Count; j++)
            {
                if (c[i].ToString() == rc.variables[j].varName && !foundA) 
                {
                    textA = rc.variables[j].number.ToString();
                    i++;
                    foundA = true;
                    break;
                }
            }

            //look for number
            for (int j = 0; j < 9; ++j)
            {
                if (c[i].ToString() == j.ToString() && !foundA)
                {
                    textA += c[i].ToString();
                }
                else if (c[i].ToString() == " " || c[i].ToString() == "<" || c[i].ToString() == ">" || c[i].ToString() == "!" || c[i].ToString() == "=")
                {
                    foundA = true;
                }
            }

            for (int j = 0; j < rc.variables.Count; j++)
            {
                if (c[i].ToString() == rc.variables[j].varName && !foundB)
                {
                    textB = rc.variables[j].number.ToString();
                    i++;
                    break;
                }

            }

            for (int j = 0; j < 9; ++j)
            {
                if (c[i].ToString() == j.ToString() && !foundB)
                {
                    textB += c[i].ToString();
                }
            }

         
            if (textA!=null && textB!=null)
            {
                for (int j = 0; j < c.Length; j++)
                {
                    switch (c[j].ToString())
                    {
                        case ">": return MathComparator.CompareTwoNumbersWith(float.Parse(textA), float.Parse(textB), ">");
                        case "<": return MathComparator.CompareTwoNumbersWith(float.Parse(textA), float.Parse(textB), "<");
                        case "=": return MathComparator.CompareTwoNumbersWith(float.Parse(textA), float.Parse(textB), "=");
                        case "!": return MathComparator.CompareTwoNumbersWith(float.Parse(textA), float.Parse(textB), "!");
                    }
                    
                }
                break;
            }

        }

        return false;
    }
}
