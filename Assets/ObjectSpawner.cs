using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject toSpawn;
    public List<Transform> waypoints;
	void Awake ()
    {
        GameObject obj = (GameObject)Instantiate(toSpawn, transform.position, transform.rotation);
        obj.transform.SetParent(transform);
        obj.GetComponent<PatrolingEnemy>().waypoints = waypoints;
	}
}
