using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolingEnemy : Actor
{
    [Header("Patrolling")]
    public List<Transform> waypoints;
    public float waypointRange;
    public float waypointDelay;

    [Header("Seeking")]
    public float seekLength;
    private float currentSeekTime;

    [Header("Attacking")]
    public float attackRange;
    public float attackDelay;
    public float attackLength;
    public float attackCooldown;

    private float currentCooldown;

    private int currentWaypoint;
    private Transform selectedWaypoint;

    private bool targetDetected;
    private bool inAttackRange;

    public enum Behaviour
    {
        Patrol,
        Seek,
        Attack,
        None
    }
    public Behaviour currentBehaviour;

    protected override void Initialize()
    {
        base.Initialize();
        ResetWaypoints();
    }

    //Components
    void Update()
    {
        RunStates();
    }

    void RunStates()
    {
        switch(currentState)
        {
            case State.Active:
                RunBehaviours();
                bus.Actions(actions);
                break;
            case State.Inactive:
                break;
        }
    }

    void RunBehaviours()
    {
        switch(currentBehaviour)
        {
            case Behaviour.Patrol:
                Patrolling();
                break;
            case Behaviour.Seek:
                Seeking();
                break;
            case Behaviour.Attack:
                StartCoroutine(Attack(attackDelay, attackLength));
                break;
            case Behaviour.None:
                break;
        }
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    IEnumerator Attack(float delay, float length)
    {
        currentBehaviour = Behaviour.None;
        yield return new WaitForSeconds(delay);
        actions.AssignAction(InputState.Actions.Action2, true);
        yield return new WaitForSeconds(length);
        yield return new WaitForEndOfFrame();
        actions.AssignAction(InputState.Actions.Action2, false);
        currentBehaviour = Behaviour.Seek;
        currentCooldown = attackCooldown;
    }

    void Seeking()
    {
        currentSeekTime -= Time.deltaTime;

        if (currentSeekTime > 0)
        {
            actions.AssignAction(InputState.Actions.Action1, true);
            actions.target = Player.instance.transform;

            if (Vector3.Distance(transform.position, actions.target.position) <= attackRange && currentCooldown <= 0)
            {
                actions.AssignAction(InputState.Actions.Action1, false);
                currentBehaviour = Behaviour.Attack;
            }
        }
        else
        {
            actions.AssignAction(InputState.Actions.Action1, false);
            ResetWaypoints();
            currentBehaviour = Behaviour.Patrol;
        }

    }

    void ResetWaypoints()
    {
        if (waypoints.Count > 0)
        {
            selectedWaypoint = waypoints[0];
            actions.target = selectedWaypoint;
        }
    }

    void Patrolling()
    {
        actions.AssignAction(InputState.Actions.Action0, true);
        if (Vector3.Distance(transform.position, selectedWaypoint.position) <= waypointRange)
        {
            StartCoroutine(NextWaypoint(waypointDelay));
        }

        if(currentSeekTime > 0)
        {
            actions.AssignAction(InputState.Actions.Action0, false);
            currentBehaviour = Behaviour.Seek;
        }
    }

    IEnumerator NextWaypoint(float delay)
    {
        selectedWaypoint = null;
        yield return new WaitForSeconds(delay);
        currentWaypoint++;
        if(currentWaypoint >= waypoints.Count)
        {
            currentWaypoint = 0;
        }
        selectedWaypoint = waypoints[currentWaypoint];
        actions.target = selectedWaypoint;
    }

    public void Detection()
    {
        currentSeekTime = seekLength;
    }
}
