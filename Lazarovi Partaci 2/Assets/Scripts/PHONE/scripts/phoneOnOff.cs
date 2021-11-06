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
            Debug.Log("zapnutí");
            turnOn();
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && homePage.activeSelf == true)
        {
            Debug.Log("vypnutí");
            turnOff();
            StartCoroutine(wait());
        }
    }

    void turnOff()
    {
        if (animator != null)
        {
            bool isOpen = animator.GetBool("open");

            animator.SetBool("open", false);
        }
        OnOff = false;
    }

    void turnOn()
    {
        Start();

        if (animator != null)
        {
            bool isOpen = animator.GetBool("open");

            animator.SetBool("open", true);
        }
        OnOff = true;
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(time);
        phoneUI.SetActive(false);
    }
}
