using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
    [Header("State")]
    public State currentState;
    private State oldState;
    public enum State
    {
        Active,
        Inactive,
        Setup
    }

    [Header("Status")]
    public float health;
    protected float currentHealth;

    public float soundEmission;

    protected float maxLight = 50;
    public float currentLight;

    //Input
    protected InputBus bus;
    protected InputState actions;

    void Awake()
    {
        currentHealth = health;
        Initialize();
    }

    protected virtual void Hit()
    {
        CheckStatus();
    }
    protected virtual void Initialize()
    {
        bus = GetComponent<InputBus>();
        actions = new InputState();
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        Hit();
    }

    public void ChangeStatus(State newState, float time)
    {
        StartCoroutine(TimedStatusChange(newState, time));
        Hit();
    }

    public void DamageOverTime(float duration, int ticks, float amount)
    {
        StartCoroutine(TakeDamageOverTime(duration, ticks, amount));
        Hit();
    }

    IEnumerator TimedStatusChange(State newState, float time)
    {
        currentState = newState;
        yield return new WaitForSeconds(time);
        currentState = State.Active;
    }

    IEnumerator TakeDamageOverTime(float duration, int ticks, float amount)
    {
        int currentTicks = 0;
        while(currentTicks < ticks)
        {
            currentTicks++;
            currentHealth -= amount;
            yield return new WaitForSeconds(duration);
        }
    }

    protected virtual void CheckStatus() { }
}
