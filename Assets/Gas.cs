using UnityEngine;
using System.Collections;

public class Gas : MonoBehaviour
{
    public enum Type
    {
        Nerve,
        Smoke
    }
    public Type gasType;

    public float damageOverTime;
    public float lifetime;

    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerStay(Collider hit)
    {
        PatrolingEnemy act = hit.GetComponent<PatrolingEnemy>();
        switch(gasType)
        {
            case Type.Nerve:
                act.Damage(damageOverTime);
                break;
            case Type.Smoke:
                act.currentSeekTime = 0;
                break;

        }
    }
}
