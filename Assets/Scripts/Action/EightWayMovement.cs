using UnityEngine;
using System.Collections;

public class EightWayMovement : Action
{
    [Header("Properties")]
    public float speed;
    [Range(0,1)]
    public float acceleration;

    //Components
    private Rigidbody rigid;

    protected override void Initialize()
    {
        base.Initialize();
        rigid = GetComponent<Rigidbody>();
    }

    protected override void Execute(InputState actions)
    {
        Vector3 xDir = (transform.right * actions.primaryDirection.x) * speed;
        Vector3 yDir = (transform.forward * actions.primaryDirection.y) * speed;
        Vector3 vect = xDir + yDir;
        vect.y = rigid.velocity.y;
        rigid.velocity = Vector3.Lerp(rigid.velocity, vect, acceleration);
    }
}
