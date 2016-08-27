using UnityEngine;
using System.Collections;

public class InputBus : MonoBehaviour
{
    public delegate void InputAction(InputState actions);
    public InputAction Actions;
}

public class InputState
{
    public Vector2 primaryDirection;
    public Vector2 secondaryDirection;

    public bool action0;
    public bool action1;
    public bool action2;
    public bool action3;
    public bool action4;
    public bool action5;
    public bool action6;
    public bool action7;
    public bool action8;
    public bool action9;
    public bool action10;
    public bool action11;

    public enum Actions
    {
        Action0,
        Action1,
        Action2,
        Action3,
        Action4,
        Action5,
        Action6,
        Action7,
        Action8,
        Action9,
        Action10,
        Action11
    }

    public void AssignAction(Actions action, bool active)
    {
        switch(action)
        {
            case Actions.Action0:
                action0 = active;
                break;
            case Actions.Action1:
                action1 = active;
                break;
            case Actions.Action2:
                action2 = active;
                break;
            case Actions.Action3:
                action3 = active;
                break;
            case Actions.Action4:
                action4 = active;
                break;
            case Actions.Action5:
                action5 = active;
                break;
            case Actions.Action6:
                action6 = active;
                break;
            case Actions.Action7:
                action7 = active;
                break;
            case Actions.Action8:
                action8 = active;
                break;
            case Actions.Action9:
                action9 = active;
                break;
            case Actions.Action10:
                action10 = active;
                break;
            case Actions.Action11:
                action11 = active;
                break;
        }
    }

    public bool CheckAction(Actions action)
    {
        switch (action)
        {
            case Actions.Action0:
                return action0;
            case Actions.Action1:
                return action1;
            case Actions.Action2:
                return action2;
            case Actions.Action3:
                return action3;
            case Actions.Action4:
                return action4;
            case Actions.Action5:
                return action5;
            case Actions.Action6:
                return action6;
            case Actions.Action7:
                return action7;
            case Actions.Action8:
                return action8;
            case Actions.Action9:
                return action9;
            case Actions.Action10:
                return action10;
            case Actions.Action11:
                return action11;
            default:
                return false;
        }
    }
}
