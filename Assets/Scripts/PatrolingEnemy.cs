using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PatrolingEnemy : Actor
{
    [Header("Patrolling")]
    public List<Transform> waypoints;
    public float waypointRange;
    public float waypointDelay;
    public InputState.Actions patrolAction;

    [Header("Seeking")]
    public float seekLength;
    public float currentSeekTime;
    public InputState.Actions seekAction;

    [Header("Attacking")]
    public float attackRange;
    public float attackDelay;
    public float attackLength;
    public float attackCooldown;
    public InputState.Actions attackAction;
    public InputState.Actions trackingAction;

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
            case State.Setup:
                ResetWaypoints();
                currentState = State.Active;
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
        actions.AssignAction(trackingAction, true);
        yield return new WaitForSeconds(delay);
        actions.AssignAction(attackAction, true);
        yield return new WaitForSeconds(length);
        yield return new WaitForEndOfFrame();
        actions.AssignAction(attackAction, false);
        actions.AssignAction(trackingAction, false);
        currentBehaviour = Behaviour.Seek;
        currentCooldown = attackCooldown;
    }

    void Seeking()
    {
        actions.AssignAction(patrolAction, false);
        actions.AssignAction(attackAction, false);
        currentSeekTime -= Time.deltaTime;

        if (currentSeekTime > 0)
        {
            actions.AssignAction(seekAction, true);
            actions.target = Player.instance.transform;

            if (Vector3.Distance(transform.position, actions.target.position) <= attackRange && currentCooldown <= 0)
            {
                actions.AssignAction(seekAction, false);
                currentBehaviour = Behaviour.Attack;
            }
        }
        else
        {
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
        actions.AssignAction(seekAction, false);
        actions.AssignAction(attackAction, false);

        if (currentSeekTime > 0)
        {
            currentBehaviour = Behaviour.Seek;
        }
        else
        {
            if (selectedWaypoint != null)
            {
                actions.AssignAction(patrolAction, true);
                if (Vector3.Distance(transform.position, selectedWaypoint.position) <= waypointRange)
                {
                    StartCoroutine(NextWaypoint(waypointDelay));
                }
            }
        }
    }

    IEnumerator NextWaypoint(float delay)
    {
        selectedWaypoint = null;
        actions.AssignAction(patrolAction, false);
        yield return new WaitForSeconds(delay);
        currentWaypoint++;
        if(currentWaypoint >= waypoints.Count)
        {
            currentWaypoint = 0;
        }
        selectedWaypoint = waypoints[currentWaypoint];
        actions.target = selectedWaypoint;
    }

    protected override void Hit()
    {
        base.Hit();
        Detection();
    }
    public void Detection()
    {
        currentSeekTime = seekLength;
    }

    protected override void CheckStatus()
    {
        base.CheckStatus();
        if(currentHealth <= 0)
        {
            currentState = State.Inactive;
            Sequence death = DOTween.Sequence();
            transform.DOShakePosition(1, 1, 50, 100);
            death.Append(transform.DOScale(transform.localScale * 1.5f, .5f));
            death.Append(transform.DOScale(Vector3.zero, .5f));
            death.OnComplete(() => Destroy(gameObject));
        }
    }
}
