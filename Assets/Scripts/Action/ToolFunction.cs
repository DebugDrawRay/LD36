using UnityEngine;
using System.Collections.Generic;

public class ToolFunction : ScriptableObject
{
    public AncientTool.Input[] sequence;
    public AncientTool.Function function;
    public float cooldown;
    public bool CheckSequenceMatch(List<AncientTool.Input> check)
    {
        if (check.Count != AncientTool.sequenceLength)
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
