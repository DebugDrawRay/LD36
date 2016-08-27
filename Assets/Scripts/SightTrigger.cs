using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SightTrigger : MonoBehaviour
{
    public UnityEvent OnSight;

    void OnTriggerStay(Collider hit)
    {
        if(hit.GetComponent<Player>())
        {
            OnSight.Invoke();
        }
    }
}
