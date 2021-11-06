using UnityEngine;

public class phoneRingtone : MonoBehaviour
{
    public GameObject[] ringtones;
    public GameObject newRingtone;

    public void setRingtone()
    {
        foreach (GameObject tone in ringtones)
        {
            tone.GetComponent<AudioSource>().Stop();
            tone.SetActive(false);
        }
        newRingtone.SetActive(true);
        newRingtone.GetComponent<AudioSource>().Play();
    }
}
