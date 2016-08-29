using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject toSpawn;
    public List<Transform> waypoints;
    public bool onAwake = true;
    public bool alignRotation = true;
    public bool setParent = true;
    void Awake()
    {
        if (onAwake)
        {
            Spawn();
        }
    }

    public void Spawn()
    { 
        GameObject obj = (GameObject)Instantiate(toSpawn, transform.position, Quaternion.identity);
        if(alignRotation)
        {
            obj.transform.rotation = transform.rotation;
        }
        if (setParent)
        {
            obj.transform.SetParent(transform);
        }
        if (obj.GetComponent<PatrolingEnemy>())
        {
            obj.GetComponent<PatrolingEnemy>().waypoints = waypoints;
        }
	}
}
