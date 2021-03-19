using System;
[System.Serializable]

//This works not much said it holds a variable name and its interger number
public class VariableType 
{
    public string varName=null;
    public int number=0;

    public void Variable(string varN, int num)
    {
        varName = varN;
        number = num;
    }

}
