using UnityEngine;
using System.Collections;

public class Lean : Action
{
    [Header("Properties")]
    public Transform leanTarget;
    public float leanAmount = 45f;
    public float leanSpeed;
    public InputState.Actions leftActionAssignment = InputState.Actions.Action10;
    public InputState.Actions rightActionAssignment = InputState.Actions.Action11;

    protected override void Execute(InputState actions)
    {
        Quaternion def = Quaternion.Euler(Vector3.zero);
        if ( (!actions.CheckAction(leftActionAssignment) && !actions.CheckAction(rightActionAssignment))
            || (actions.CheckAction(leftActionAssignment) && actions.CheckAction(rightActionAssignment)) )
        {
            leanTarget.localRotation = Quaternion.Slerp(leanTarget.localRotation, def, leanSpeed);
        }
        else if (actions.CheckAction(leftActionAssignment))
        {
            if(!actions.CheckAction(rightActionAssignment))
            {
                Quaternion targetRot = Quaternion.Euler(0, 0, leanAmount);
                leanTarget.localRotation = Quaternion.Slerp(leanTarget.localRotation, targetRot, leanSpeed);
            }
        }
        else if (actions.CheckAction(rightActionAssignment))
        {
            if (!actions.CheckAction(leftActionAssignment))
            {
                Quaternion targetRot = Quaternion.Euler(0, 0, -leanAmount);
                leanTarget.localRotation = Quaternion.Slerp(leanTarget.localRotation, targetRot, leanSpeed);
            }
        }


    }
}
