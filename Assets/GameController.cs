using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public CursorLockMode cursorLock;

    void Awake()
    {
        Cursor.lockState = cursorLock;
    }
}
