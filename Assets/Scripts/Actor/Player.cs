using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Player : Actor
{
    private PlayerActions input;

    public static Player instance;

    [Header("Cam Shake")]
    public Camera mainCam;
    public float length;
    public float strength;
    public int vibrato;
    public float randomness;

    private Tween currentTween;
    protected override void Initialize()
    {
        base.Initialize();
        input = PlayerActions.BindAll();

        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    protected override void Hit()
    {
        base.Hit();
        HudController.instance.UpdateHealth(currentHealth / health);
        if (currentTween == null)
        {
            currentTween = mainCam.transform.DOShakePosition(length, strength, vibrato, randomness);
            currentTween.OnComplete(() => currentTween = null);
        }
    }
    void Update()
    {
        RunStates();
    }

    void RunStates()
    {
        switch(currentState)
        {
            case State.Active:
                UpdateInput();
                break;
        }
    }

    void UpdateInput()
    {
        actions.primaryDirection = input.Move;
        actions.secondaryDirection = input.Look;

        actions.AssignAction(InputState.Actions.Action0, input.UseTool.WasPressed);

        actions.AssignAction(InputState.Actions.Action9, input.ClearSequence.WasPressed);

        actions.AssignAction(InputState.Actions.Action1, input.ToolInputOne.WasPressed);
        actions.AssignAction(InputState.Actions.Action2, input.ToolInputTwo.WasPressed);
        actions.AssignAction(InputState.Actions.Action3, input.ToolInputThree.WasPressed);
        actions.AssignAction(InputState.Actions.Action4, input.ToolInputFour.WasPressed);
        actions.AssignAction(InputState.Actions.Action5, input.ToolInputFive.WasPressed);
        actions.AssignAction(InputState.Actions.Action6, input.ToolInputSix.WasPressed);
        actions.AssignAction(InputState.Actions.Action7, input.ToolInputSeven.WasPressed);
        actions.AssignAction(InputState.Actions.Action8, input.ToolInputEight.WasPressed);

        actions.AssignAction(InputState.Actions.Action10, input.LeanLeft.IsPressed);
        actions.AssignAction(InputState.Actions.Action11, input.LeanRight.IsPressed);

        bus.Actions(actions);
    }
}
