using UnityEngine;
using UnityEngine.UI;

public class butClick : MonoBehaviour
{
    public GameObject show;
    public GameObject hide;
    [Header("Ozna�en� tla��tek")]
    public Button b1;
    public Button b2;

    [Header("Vyzv�n�n�")]
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
            if (show.gameObject == GameObject.Find("Vyzv�n�n�"))
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
