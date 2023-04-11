using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManger : MonoBehaviour
{

    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    // Update is called once per frame
    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value+15, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}
