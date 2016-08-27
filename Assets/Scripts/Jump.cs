using UnityEngine;
using System.Collections;

public class Jump : Action
{
    [Header("Properties")]
    public float jumpStrength;

    //Components
    private Rigidbody rigid;

    protected override void Initialize()
    {
        base.Initialize();
        rigid = GetComponent<Rigidbody>();
    }

    protected override void Execute(InputState actions)
    {

    }
}
