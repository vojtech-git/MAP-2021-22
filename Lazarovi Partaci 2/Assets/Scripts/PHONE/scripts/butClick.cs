using UnityEngine;
using UnityEngine.UI;

public class butClick : MonoBehaviour
{
    public GameObject show;
    public GameObject hide;
    [Header("Označení tlačítek")]
    public Button b1;
    public Button b2;

    [Header("Vyzvánění")]
    public GameObject[] ringtones;

    public void showElement()
    {
        show.SetActive(true);
        hide.SetActive(false);
        b1.Select();
    }

    private void Update()
    {
        /*if (show.activeSelf && Input.GetKeyDown(KeyCode.Backspace))
        {
            if (show.gameObject == GameObject.Find("Vyzvánění"))
            {
                foreach (GameObject tone in ringtones)
                {
                    tone.GetComponent<AudioSource>().Stop();
                }
            }
            hide.SetActive(true);
            show.SetActive(false);
            b2.Select();
        }*/

        if (show.gameObject == GameObject.Find("Vyzvánění"))
        {
            if (show.activeSelf && Input.GetKeyDown(KeyCode.Backspace))
            {
                foreach (GameObject tone in ringtones)
                {
                    tone.GetComponent<AudioSource>().Stop();
                }

                hide.SetActive(true);
                show.SetActive(false);
                b2.Select();
            }
        }
        else
        {
            if (show.activeSelf && Input.GetKeyDown(KeyCode.Backspace))
            {
                hide.SetActive(true);
                show.SetActive(false);
                b2.Select();
            }
        }
    }
}
