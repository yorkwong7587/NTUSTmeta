using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct RigInput : INetworkInput
{
    public Vector3 playAreaPosition;
    public Quaternion playAreaRotation;
    public Vector3 leftHandPosition;
    public Quaternion leftHandRotation;
    public Vector3 rightHandPosition;
    public Quaternion rightHandRotation;
    public Vector3 headsetPosition;
    public Quaternion headsetRotation;
    public HandCommand leftHandCommand;
    public HandCommand rightHandCommand;
}
