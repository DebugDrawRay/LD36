using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tool : Action
{
    public ToolFunction[] availableFunctions;
    public Transform emitionSource;

    private ToolFunction selectedFunction;

    public const int sequenceLength = 3;
    
    //Tool functions and sequencing
    public enum Input
    {
        Lethal,
        NonLethal,
        Placed,
        Held,
        Directed,
        Elemental,
        Object,
        Data
    }

    public enum Function
    {
        Fire,
        Ice,
        Laser,
        Lightning,
        Mine,
        Decoy,
        Nerve,
        Smokescreen,
        Hammer,
        Shield
    }

    private List<InputState.Actions> toolInputs = new List<InputState.Actions>();
    private bool inputInit = false;

    public List<Input> currentSequence = new List<Input>();

    protected override void Initialize()
    {
        base.Initialize();
        InitializeInput();
    }

    void InitializeInput()
    {
        toolInputs.Add(InputState.Actions.Action1);
        toolInputs.Add(InputState.Actions.Action2);
        toolInputs.Add(InputState.Actions.Action3);
        toolInputs.Add(InputState.Actions.Action4);
        toolInputs.Add(InputState.Actions.Action5);
        toolInputs.Add(InputState.Actions.Action6);
        toolInputs.Add(InputState.Actions.Action7);
        toolInputs.Add(InputState.Actions.Action8);

        toolInputs.Shuffle();

        inputInit = true;
    }

    protected override void Execute(InputState actions)
    {
        if (!inputInit)
        {
            Debug.Log("Init");
        }

        for(int i = 0; i < toolInputs.Count; i++)
        {
            if(actions.CheckAction(toolInputs[i]))
            {
                currentSequence.Add((Input)i);
                Debug.Log((Input)i);
            }
        }

        if(actions.CheckAction(InputState.Actions.Action0))
        {
            ActivateTool();
        }
        if (actions.CheckAction(InputState.Actions.Action9))
        {
            currentSequence = new List<Input>();
        }
    }

    void Update()
    {
        CheckSequence();
    }

    void ActivateTool()
    {
        Debug.Log(selectedFunction.function);
        if(selectedFunction != null)
        {
            switch(selectedFunction.function)
            {
                case Function.Fire:
                    break;
                case Function.Ice:
                    break;
                case Function.Laser:
                    break;
                case Function.Lightning:
                    break;
                case Function.Mine:
                    break;
                case Function.Decoy:
                    break;
                case Function.Nerve:
                    break;
                case Function.Smokescreen:
                    break;
                case Function.Hammer:
                    break;
                case Function.Shield:
                    break;
            }
        }
    }

    void ModStatus()
    {

    }

    void CheckSequence()
    {
        if (currentSequence.Count >= sequenceLength)
        {
            bool valid = false;
            for (int i = 0; i < availableFunctions.Length; i++)
            {
                if (availableFunctions[i].CheckSequenceMatch(currentSequence))
                {
                    selectedFunction = availableFunctions[i];
                    currentSequence = new List<Input>();
                    Debug.Log("Vaild");
                    valid = true;
                }
            }
            if(!valid)
            {
                Debug.Log("Invalid");
                GetComponent<Player>().health -= 0;
                currentSequence = new List<Input>();
            }
        }
    }

}

public static class ShuffleExtension
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}