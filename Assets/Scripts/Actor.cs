using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
    [Header("Status")]
    public float health;
    protected float currentHealth;

    //Input
    protected InputBus bus;
    protected InputState actions;

    void Awake()
    {
        bus = GetComponent<InputBus>();
        actions = new InputState();
    }
}
