using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SendFollowerStructure
{
    public FollowerEntity followerToSend;
    /// <summary>
    /// if shoud go to point - true. if should return - false.
    /// </summary>
    public bool shouldGo;
}
