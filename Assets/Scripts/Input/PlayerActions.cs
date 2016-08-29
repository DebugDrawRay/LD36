using InControl;

public class PlayerActions : PlayerActionSet
{
    public PlayerAction MoveForward;
    public PlayerAction MoveBackward;
    public PlayerAction StrafeLeft;
    public PlayerAction StrafeRight;

    public PlayerTwoAxisAction Move;

    public PlayerAction LookUp;
    public PlayerAction LookDown;
    public PlayerAction LookLeft;
    public PlayerAction LookRight;

    public PlayerTwoAxisAction Look;

    public PlayerAction LeanLeft;
    public PlayerAction LeanRight;

    public PlayerAction UseTool;

    public PlayerAction ClearSequence;

    public PlayerAction ToolInputOne;
    public PlayerAction ToolInputTwo;
    public PlayerAction ToolInputThree;
    public PlayerAction ToolInputFour;
    public PlayerAction ToolInputFive;
    public PlayerAction ToolInputSix;
    public PlayerAction ToolInputSeven;
    public PlayerAction ToolInputEight;

    public PlayerActions()
    {
        MoveForward = CreatePlayerAction("Move Forward");
        MoveBackward = CreatePlayerAction("Move Backward");
        StrafeLeft = CreatePlayerAction("Strafe Left");
        StrafeRight = CreatePlayerAction("Strafe Right");

        Move = CreateTwoAxisPlayerAction(StrafeLeft, StrafeRight, MoveBackward, MoveForward);

        LookUp = CreatePlayerAction("Look Up");
        LookDown = CreatePlayerAction("Look Down");
        LookLeft = CreatePlayerAction("Look Left");
        LookRight = CreatePlayerAction("Look Right");

        Look = CreateTwoAxisPlayerAction(LookLeft, LookRight, LookDown, LookUp);

        LeanLeft = CreatePlayerAction("Lean Left");
        LeanRight = CreatePlayerAction("Lean Right");

        UseTool = CreatePlayerAction("Use Tool");

        ClearSequence = CreatePlayerAction("Clear/Confirm Sequence");

        ToolInputOne = CreatePlayerAction("Tool Input One");
        ToolInputTwo = CreatePlayerAction("Tool Input Two");
        ToolInputThree = CreatePlayerAction("Tool Input Three");
        ToolInputFour = CreatePlayerAction("Tool Input Four");
        ToolInputFive = CreatePlayerAction("Tool Input Five");
        ToolInputSix = CreatePlayerAction("Tool Input Six");
        ToolInputSeven = CreatePlayerAction("Tool Input Seven");
        ToolInputEight = CreatePlayerAction("Tool Input Eight");
    }

    public static PlayerActions BindAll()
    {
        PlayerActions newActions = new PlayerActions();

        newActions.MoveForward.AddDefaultBinding(Key.W);
        newActions.MoveBackward.AddDefaultBinding(Key.S);
        newActions.StrafeLeft.AddDefaultBinding(Key.A);
        newActions.StrafeRight.AddDefaultBinding(Key.D);

        newActions.LookUp.AddDefaultBinding(Mouse.PositiveY);
        newActions.LookDown.AddDefaultBinding(Mouse.NegativeY);
        newActions.LookRight.AddDefaultBinding(Mouse.PositiveX);
        newActions.LookLeft.AddDefaultBinding(Mouse.NegativeX);

        newActions.LeanLeft.AddDefaultBinding(Key.Q);
        newActions.LeanRight.AddDefaultBinding(Key.E);

        newActions.UseTool.AddDefaultBinding(Mouse.LeftButton);

        newActions.ClearSequence.AddDefaultBinding(Key.Space);

        newActions.ToolInputOne.AddDefaultBinding(Key.Key1);
        newActions.ToolInputTwo.AddDefaultBinding(Key.Key2);
        newActions.ToolInputThree.AddDefaultBinding(Key.Key3);
        newActions.ToolInputFour.AddDefaultBinding(Key.Key4);
        newActions.ToolInputFive.AddDefaultBinding(Key.Key5);
        newActions.ToolInputSix.AddDefaultBinding(Key.Key6);
        newActions.ToolInputSeven.AddDefaultBinding(Key.Key7);
        newActions.ToolInputEight.AddDefaultBinding(Key.Key8);

        return newActions;
    }

}
