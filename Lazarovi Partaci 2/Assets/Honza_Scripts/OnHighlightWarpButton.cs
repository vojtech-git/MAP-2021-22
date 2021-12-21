using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnHighlightWarpButton : MonoBehaviour
{
    public GameObject planets1;
    public GameObject planets2;
    public GameObject planets3;
    public void OnMouseOver()
    {
        planets1.SetActive(true);
        planets2.SetActive(false);
        planets3.SetActive(false);
    }
}
