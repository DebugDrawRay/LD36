using UnityEngine;
using System.Collections;

public class Ram : Action
{
    public InputState.Actions assignedAction;
    public float strength;
    public float damage;
    public float cooldown;
    private float currentCooldown;

    private Rigidbody rigid;

    protected override void Initialize()
    {
        base.Initialize();
        rigid = GetComponent<Rigidbody>();
    }
    protected override void Execute(InputState actions)
    {
        if(actions.CheckAction(assignedAction) && currentCooldown <= 0)
        {
            Vector3 dir = actions.target.position - transform.position;
            dir.y = 0;
            rigid.AddForce(dir * strength, ForceMode.Impulse);
            currentCooldown = cooldown;
        }
    }
    void Update()
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }
}
