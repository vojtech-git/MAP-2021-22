using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vuk : MonoBehaviour
{
    public GameObject player;
    public GameObject anim;
    public GameObject cam2;
    public GameObject controls;

    public Text repsText;
    public Text cashText;
    public int numOfReps;
    public int cashEarned;

    private void Start()
    {
        numOfReps = 0;
        cashEarned = 0;

        controls.SetActive(true);
        StartCoroutine(africanAmericansInParis());
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Start();

            player.SetActive(false);
            cam2.SetActive(true);
            
            repsText.text = "Number of reps: 0";
            cashText.text = "Cash earned: +0";
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && cam2.activeSelf)
        {
            cam2.SetActive(false);
            player.SetActive(true);

            player.GetComponent<Player>().UseMoney(-cashEarned);

        }

        if (Input.GetKeyDown(KeyCode.Space) && cam2.activeSelf && numOfReps < 100)
        {
            anim.GetComponent<Animator>().SetTrigger("Bench");

            numOfReps++;
            repsText.text = "Number of reps: " + numOfReps;

            switch (numOfReps)
            {
                case 25:
                    cashEarned = 75;
                    cashText.text = "Cash earned: +" + cashEarned;
                    break;
                case 50:
                    cashEarned = 150;
                    cashText.text = "Cash earned: +" + cashEarned;
                    break;
                case 69:
                    cashEarned = 300;
                    cashText.text = "Cash earned: +" + cashEarned;
                    break;
                case 100:
                    cashEarned = 500;
                    cashText.text = "Cash earned: +" + cashEarned;
                    repsText.text = "Number of reps: 100";

                    StartCoroutine(prosimNemlatMeHonzo());
                    break;
            }

        }
    }

    IEnumerator prosimNemlatMeHonzo()
    {
        yield return new WaitForSecondsRealtime(1f);
        anim.GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(5f);
        cam2.SetActive(false);
        player.SetActive(true);

        player.GetComponent<Player>().UseMoney(-cashEarned);

    }

    IEnumerator africanAmericansInParis()
    {
        yield return new WaitForSecondsRealtime(5f);
        controls.SetActive(false);
    }
}
