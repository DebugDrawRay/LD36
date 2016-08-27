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
        Inactive
    }

    [Header("Status")]
    public float health;
    protected float currentHealth;

    public float soundEmission;
    public float visualStealth;

    //Input
    protected InputBus bus;
    protected InputState actions;

    void Awake()
    {
        currentHealth = health;
        Initialize();
    }

    protected virtual void Initialize()
    {
        bus = GetComponent<InputBus>();
        actions = new InputState();
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
    }

    public void ChangeStatus(State newState, float time)
    {
        StartCoroutine(TimedStatusChange(newState, time));
    }

    public void DamageOverTime(float duration, int ticks, float amount)
    {
        StartCoroutine(TakeDamageOverTime(duration, ticks, amount));
    }

    IEnumerator TimedStatusChange(State newState, float time)
    {
        oldState = currentState;
        currentState = newState;
        yield return new WaitForSeconds(time);
        currentState = oldState;
    }

    IEnumerator TakeDamageOverTime(float duration, int ticks, float amount)
    {
        int currentTicks = 0;
        while(currentTicks < ticks)
        {
            currentTicks++;
            currentHealth -= amount;
            yield return new WaitForSeconds(duration / ticks);
        }
    }
}
