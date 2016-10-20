using UnityEngine;
using System.Collections;

public class LightSource : MonoBehaviour
{
    public float range;
    public LayerMask castMask;
    public float lightPower;

    void Update()
    {
        Vector3 dir = Player.instance.transform.position - transform.position;
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, castMask))
        {
            Player player = hit.collider.GetComponent<Player>();
            if (player && Vector3.Distance(transform.position, player.transform.position) <= range)
            {
                player.currentLight = range - Vector3.Distance(transform.position, player.transform.position);
            }
        }
    }
}
