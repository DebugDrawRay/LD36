using UnityEngine;
using System.Collections;

public class Ram : Action
{
    public InputState.Actions assignedAction;
    public float strength;
    public float damage;

    private Rigidbody rigid;

    protected override void Initialize()
    {
        base.Initialize();
        rigid = GetComponent<Rigidbody>();
    }
    protected override void Execute(InputState actions)
    {
        if(actions.CheckAction(assignedAction))
        {
            Vector3 dir = actions.target.position - transform.position;
            dir.y = 0;
            rigid.AddForce(dir * strength, ForceMode.Impulse);
        }
    }
}
