using UnityEngine;
using System.Collections;
using Pathfinding;
public class GameController : MonoBehaviour
{
    public CursorLockMode cursorLock;

    public enum State
    {
        Setup,
        Start,
        InGame,
        Pause,
        EndGame
    }
    public State currentState;

    public GameObject player;
    //Components
    public bool generateLevel;
    private LevelGenerator levelGen;
    private AstarPath pathfinder;

    void Awake()
    {
        Cursor.lockState = cursorLock;
        levelGen = GetComponent<LevelGenerator>();
        pathfinder = GetComponent<AstarPath>();
    }

    void Setup()
    {
        if (generateLevel)
        {
            levelGen.NewLevel(SetupPathfinding);
        }
    }
    void SetupPathfinding()
    {
        AstarPath.active.Scan();
    }
    void NewPlayer()
    {
        Vector3 spawn = levelGen.currentRooms[0].transform.position;
        Quaternion rot = levelGen.currentRooms[0].transform.rotation;

        Instantiate(player, spawn, rot);
    }

    void Update()
    {
        RunState();
    }

    void RunState()
    {
        switch(currentState)
        {
            case State.Setup:
                Setup();
                NewPlayer();
                currentState = State.Start;
                break;
            case State.Start:
                currentState = State.InGame;
                break;
            case State.InGame:
                break;
        }
    }
}
