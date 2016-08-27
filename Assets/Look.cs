using UnityEngine;
using System.Collections;

public class Look : Action
{
    [Header("Properties")]
    public float sensitivity = 1f;

    [Header("Axis")]
    public Transform xAxis;
    public Transform yAxis;

    //Current Rotations
    private float xRot;
    private float yRot;
    protected override void Initialize()
    {
        base.Initialize();
        
    }

    protected override void Execute(InputState actions)
    {
        xRot += actions.secondaryDirection.x * sensitivity;
        yRot += -actions.secondaryDirection.y * sensitivity;
        yRot = Mathf.Clamp(yRot, -90, 90);
        if (xRot > 360)
        {
            xRot = 0;
        }
        if (xRot < 0)
        {
            xRot = 360;
        }
        xRot = Mathf.Clamp(xRot, 0, 360);

        Quaternion xQ = Quaternion.Euler(xAxis.localRotation.eulerAngles.x, xRot, xAxis.localRotation.eulerAngles.z);
        Quaternion yQ = Quaternion.Euler(yRot, yAxis.localRotation.y, yAxis.localRotation.z);

        xAxis.localRotation = Quaternion.Slerp(xAxis.localRotation, xQ, 1);
        yAxis.localRotation = Quaternion.Slerp(yAxis.localRotation, yQ, 1);
    }
}
