using UnityEngine;
using System.Collections.Generic;

public class ToolFunction : ScriptableObject
{
    public Tool.Input[] sequence;
    public Tool.Function function;
    public bool CheckSequenceMatch(List<Tool.Input> check)
    {
        if (check.Count != Tool.sequenceLength)
        {
            return false;
        }
        else
        {
            bool match = true;
            for (int i = 0; i < sequence.Length; i++)
            {
                if (sequence[i] != check[i])
                {
                    match = false;
                }
            }
            return match;
        }
    }
}
