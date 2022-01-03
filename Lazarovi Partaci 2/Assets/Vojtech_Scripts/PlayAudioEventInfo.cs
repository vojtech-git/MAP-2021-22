using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayAudioEventInfo : QuestEventInfo
{
    public int talkQuestID;
    public AudioSource[] audioSources;
}
