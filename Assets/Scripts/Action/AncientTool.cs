using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AncientTool : Action
{
    [Header("Property")]
    public ToolFunction[] availableFunctions;
    public Transform emissionSource;
    public LayerMask emissionMask;
    public bool shuffleInput;

    [Header("Projectiles")]
    public GameObject fire;
    public GameObject ice;
    public GameObject laser;
    public GameObject lightning;
    public GameObject mine;
    public GameObject decoy;
    public GameObject nerve;
    public GameObject smokescreen;

    [Header("Held")]
    public GameObject hammer;
    public GameObject shield;

    public ToolFunction selectedFunction;

    public const int sequenceLength = 3;

    private float maxCooldown;
    private float currentCooldown;
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
        Shield,
        Scan,
        Cloak
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

        if (shuffleInput)
        {
            toolInputs.Shuffle();
        }

        inputInit = true;
    }

    void Update()
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            HudController.instance.UpdateCooldown(currentCooldown / maxCooldown);
        }
    }

    protected override void Execute(InputState actions)
    {
        for(int i = 0; i < toolInputs.Count; i++)
        {
            if(currentSequence.Count < sequenceLength && actions.CheckAction(toolInputs[i]))
            {
                currentSequence.Add((Input)i);
                HudController.instance.DisplayCharacter((Input)i);
                Debug.Log((Input)i);
            }
        }

        if(actions.CheckAction(InputState.Actions.Action0))
        {
            CheckSequence();
        }

        if (actions.CheckAction(InputState.Actions.Action9))
        {
            currentSequence = new List<Input>();
        }
    }

    void ActivateTool()
    {
        if(selectedFunction != null)
        {
            Debug.Log(selectedFunction.function);
            CreateObject(selectedFunction.function);
            maxCooldown = selectedFunction.cooldown;
            currentCooldown = selectedFunction.cooldown;
            selectedFunction = null;
        }
    }

    void CreateObject(Function type)
    {
        GameObject held = null;
        switch(type)
        {
            case Function.Fire:
                Instantiate(fire, emissionSource.position, emissionSource.rotation);
                break;
            case Function.Ice:
                Instantiate(ice, emissionSource.position, emissionSource.rotation);
                break;
            case Function.Lightning:
                Instantiate(lightning, emissionSource.position, emissionSource.rotation);
                break;
            case Function.Laser:
                Instantiate(laser, emissionSource.position, emissionSource.rotation);
                break;
            case Function.Mine:
                Instantiate(mine, emissionSource.position, emissionSource.rotation);
                break;
            case Function.Decoy:
                Instantiate(decoy, emissionSource.position, emissionSource.rotation);
                break;
            case Function.Nerve:
                Instantiate(nerve, emissionSource.position, emissionSource.rotation);
                break;
            case Function.Smokescreen:
                Instantiate(smokescreen, emissionSource.position, emissionSource.rotation);
                break;
            case Function.Hammer:
                held = (GameObject)Instantiate(hammer, emissionSource.position, emissionSource.rotation);
                held.transform.SetParent(emissionSource.transform);
                break;
            case Function.Shield:
                held = (GameObject)Instantiate(shield, emissionSource.position, emissionSource.rotation);
                held.transform.SetParent(emissionSource.transform);
                break;
        }
    }

    void CheckSequence()
    {
        if (currentSequence.Count >= sequenceLength && currentCooldown <= 0)
        {
            bool valid = false;
            for (int i = 0; i < availableFunctions.Length; i++)
            {
                if (availableFunctions[i].CheckSequenceMatch(currentSequence))
                {
                    selectedFunction = availableFunctions[i];
                    currentSequence = new List<Input>();
                    valid = true;
                }
            }
            if(valid)
            {
                Debug.Log("Vaild " + selectedFunction.function);
                ActivateTool();
            }
            else
            {
                Debug.Log("Invalid");
                GetComponent<Player>().health -= 0;
                currentSequence = new List<Input>();
            }
            HudController.instance.ConfirmCharacters(valid);
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