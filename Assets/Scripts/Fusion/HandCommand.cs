using Fusion;

[System.Serializable]
public struct HandCommand : INetworkStruct
{
    public float thumbTouchedCommand;
    public float indexTouchedCommand;
    public float gripCommand;
    public float triggerCommand;
    // Optionnal commands
    public int poseCommand;
    public float pinchCommand;// Can be computed from triggerCommand by default
}