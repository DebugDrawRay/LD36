using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
    [Header("State")]
    public State currentState;
    public enum State
    {
        Active,
        Inactive
    }

    [Header("Status")]
    public float health;
    protected float currentHealth;

    //Input
    protected InputBus bus;
    protected InputState actions;

    void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        bus = GetComponent<InputBus>();
        actions = new InputState();
    }
}
