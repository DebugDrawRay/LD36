using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelGenerator : MonoBehaviour
{
    public GameObject capRoom;
    public GameObject[] availableRooms;
    public List<Room> currentRooms = new List<Room>();
    public int roomCount;
    private int currentCount;
    void Awake()
    {
        Setup();
    }

    void Setup()
    {
        currentRooms.Add(Instantiate(capRoom).GetComponent<Room>());
        currentRooms[0].transform.position = Vector3.zero;
        Build(currentRooms[0]);
    }

    void Build(Room room)
    {
        if (currentCount < roomCount)
        {
            for (int i = 0; i < room.anchors.Length; i++)
            {
                Transform trans = room.anchors[i];
                if (trans.childCount <= 0)
                {
                    int rand = Random.Range(0, availableRooms.Length);
                    Room newRoom = Instantiate(availableRooms[rand]).GetComponent<Room>();

                    newRoom.transform.position = trans.position;
                    newRoom.transform.rotation = trans.rotation;
                    newRoom.transform.SetParent(trans);

                    rand = Random.Range(0, newRoom.orientations.Length);
                    Room.Orientation orien = newRoom.orientations[rand];
                    Transform child = newRoom.child;

                    child.localRotation = Quaternion.Euler(0, orien.rotation, 0);
                    child.localPosition = orien.offset;

                    GameObject empty = Instantiate(new GameObject());
                    empty.transform.SetParent(orien.point);

                    currentRooms.Add(newRoom);
                    currentCount++;

                    Build(newRoom);
                }
                else
                {
                    currentCount++;
                }
            }
        }
        else
        {
            for (int i = 0; i < room.anchors.Length; i++)
            {
                Transform trans = room.anchors[i];
                if (trans.childCount <= 0)
                {
                    int rand = Random.Range(0, availableRooms.Length);
                    Room newRoom = Instantiate(capRoom).GetComponent<Room>();

                    newRoom.transform.position = trans.position;
                    newRoom.transform.rotation = trans.rotation;
                    newRoom.transform.SetParent(trans);

                    rand = Random.Range(0, newRoom.orientations.Length);
                    Room.Orientation orien = newRoom.orientations[rand];
                    Transform child = newRoom.child;

                    child.localRotation = Quaternion.Euler(0, orien.rotation, 0);
                    child.localPosition = orien.offset;

                    currentRooms.Add(newRoom);

                }
            }
        }
    }
}
