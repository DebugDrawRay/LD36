using UnityEngine;
using System.Collections;

public class Action : MonoBehaviour
{
    private InputBus bus;

    void Awake()
    {
        Initialize();
    }
    protected virtual void Initialize()
    {
        bus = GetComponent<InputBus>();
        bus.Actions += Execute;
    }
    void OnDestroy()
    {
        bus.Actions -= Execute;
    }

    protected virtual void Execute(InputState actions) { }
}
