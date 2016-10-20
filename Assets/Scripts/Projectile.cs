using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public bool destroyOnContact = true;

    [System.Serializable]
    public class ActorCollision : UnityEvent<Actor> { }

    [SerializeField]
    public ActorCollision OnActorCollision;
    public UnityEvent OnCollision;
    public UnityEvent OnTimedDestroy;

    private Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = transform.forward * speed;
    }

    void OnDestroy()
    {
        OnTimedDestroy.Invoke();
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        Actor isActor = hit.GetComponent<Actor>();

        if(isActor)
        {
            OnActorCollision.Invoke(isActor);
        }
        else
        {
            OnCollision.Invoke();
        }

        if (destroyOnContact)
        {
            Destroy(gameObject);
        }
    }
}
