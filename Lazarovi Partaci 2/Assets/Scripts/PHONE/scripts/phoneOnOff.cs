using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneOnOff : MonoBehaviour
{
    private Animator animator;

    public GameObject homePage;
    public GameObject phoneUI;

    public float time = 0.5f;

    private bool OnOff;

    public phoneGettingCalled phoneGettingCalled;

    // Start is called before the first frame update
    void Start()
    {
        OnOff = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && OnOff == false)
        {
            phoneUI.SetActive(true);
            Debug.Log("zapnut?");
            turnOn();
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && homePage.activeSelf == true)
        {
            Debug.Log("vypnut?");
            turnOff();
            StartCoroutine(wait());
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && OnOff == false)
        {
            phoneUI.SetActive(true);
            Debug.Log("zapnut?");
            turnOn();

            StartCoroutine(test());

            Debug.Log("story zvon? telefon");
            phoneGettingCalled.StoryGettingCalled1();
        }
    }

    public void Volani(int questNumber)
    {
        phoneUI.SetActive(true);
        Debug.Log("zapnut?");
        turnOn();

        StartCoroutine(test());

        Debug.Log("story zvon? telefon");
        if (questNumber == 0)
        {
            phoneGettingCalled.StoryGettingCalled1();
        }
        else if (questNumber == 1)
        {
            phoneGettingCalled.StoryGettingCalled2();
        }
        else if (questNumber == 2)
        {
            phoneGettingCalled.StoryGettingCalled3();
        }
    }

    public void turnOff()
    {
        if (animator != null)
        {
            bool isOpen = animator.GetBool("open");

            animator.SetBool("open", false);
        }
        OnOff = false;
    }

    public void turnOn()
    {
        Start();

        if (animator != null)
        {
            bool isOpen = animator.GetBool("open");

            animator.SetBool("open", true);
        }
        OnOff = true;
    }

    public void xdLmaoJanProchazka()
    {
        StartCoroutine(wait());
    }
    
    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(time);
        phoneUI.SetActive(false);
    }

    IEnumerator test()
    {
        yield return new WaitForSecondsRealtime(1f);
    }
}
