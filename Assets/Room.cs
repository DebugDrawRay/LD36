using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
    public Transform[] anchors;
    [SerializeField]
    public Orientation[] orientations;
    public Transform child;

    [System.Serializable]
    public class Orientation
    {
        public float rotation;
        public Vector3 offset;
        public Transform point;
    }
}
