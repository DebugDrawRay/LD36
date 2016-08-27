using UnityEngine;
using System.Collections;

public class ModStatus : MonoBehaviour
{
    public float damage;
    public int effectTicks;
    public float effectDuration;
    public Actor.State stateChange;

    public void ChangeStatus(Actor actor)
    {
        actor.ChangeStatus(stateChange, effectDuration);
    }

    public void DamageOverTime(Actor actor)
    {
        actor.DamageOverTime(effectDuration, effectTicks, damage);
    }

    public void Damage(Actor actor)
    {
        actor.Damage(damage);
    }
}
