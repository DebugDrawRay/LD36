using UnityEngine;
using System.Collections;
using Pathfinding;
public class WaypointMovement : Action
{
    public InputState.Actions waypointMovement;
    public InputState.Actions directMovement;
    public InputState.Actions rotateOnly;
    [Header("Properties")]
    public float speed;
    [Range(0,1)]
    public float acceleration;
    [Header("Pathing")]
    public Transform target;
    [Range(0,1)]
    public float accuracy;
    public float nodeRange;
    private Path currentPath;
    private int currentNode;

    //Components
    private Seeker seek;
    private Rigidbody rigid;

    protected override void Initialize()
    {
        base.Initialize();
        seek = GetComponent<Seeker>();
        rigid = GetComponent<Rigidbody>();
    }

    protected override void Execute(InputState actions)
    {
        if(actions.CheckAction(waypointMovement))
        {
            if(currentPath == null)
            {
                currentPath = seek.StartPath(transform.position, actions.target.position);
                currentNode = 0;
            }
            else
            {
                Vector3 targetPoint = currentPath.vectorPath[currentNode];

                Vector3 direction = targetPoint - transform.position;
                Quaternion rot = Quaternion.LookRotation(direction.normalized, transform.up);
                rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, accuracy);

                Vector3 move = transform.forward * speed;

                move.y = rigid.velocity.y;
                rigid.velocity = Vector3.Lerp(rigid.velocity, move, acceleration);
                
                if(Vector3.Distance(transform.position, targetPoint) <= nodeRange)
                {
                    currentNode++;
                    if(currentNode >= currentPath.vectorPath.Count)
                    {
                        currentPath = null;
                    }
                }
            }
        }
        else if(actions.CheckAction(directMovement))
        {
            Vector3 direction = actions.target.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(direction.normalized, transform.up);
            rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, accuracy);

            Vector3 move = transform.forward * speed;

            move.y = rigid.velocity.y;
            rigid.velocity = Vector3.Lerp(rigid.velocity, move, acceleration);
        }
        else if(actions.CheckAction(rotateOnly))
        {
            Vector3 direction = actions.target.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(direction.normalized, transform.up);
            rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, accuracy);
        }
        else
        {
            rigid.velocity = Vector3.zero;
            currentPath = null;
        }
    }
}
