using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour
{
    public float damage;
    public bool destroyOnCollision;

    public bool knockback;
    public float knockbackForce;

    void OnTriggerEnter(Collider hit)
    {
        Actor actor = hit.GetComponent<Actor>();
        if(actor)
        {
            actor.Damage(damage);
            if(knockback)
            {
                Vector3 dir = actor.transform.position - transform.position;
                dir.y = 0;
                actor.GetComponent<Rigidbody>().AddForce(dir * knockbackForce, ForceMode.Impulse);
            }
            if(destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }
}
