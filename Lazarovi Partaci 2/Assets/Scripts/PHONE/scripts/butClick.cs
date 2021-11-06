using UnityEngine;
using UnityEngine.UI;

public class butClick : MonoBehaviour
{
    public GameObject show;
    public GameObject hide;
    [Header("Oznaèení tlaèítek")]
    public Button b1;
    public Button b2;

    [Header("Vyzvánìní")]
    public GameObject[] ringtones;

    public void showElement()
    {
        show.SetActive(true);
        hide.SetActive(false);
        b1.Select();
    }

    private void Update()
    {
        if (show.activeSelf && Input.GetKeyDown(KeyCode.Backspace))
        {
            if (show.gameObject == GameObject.Find("Vyzvánìní"))
            {
                foreach (GameObject tone in ringtones)
                {
                    tone.GetComponent<AudioSource>().Stop();
                }
            }
            hide.SetActive(true);
            show.SetActive(false);
            b2.Select();
        }
    }
}
