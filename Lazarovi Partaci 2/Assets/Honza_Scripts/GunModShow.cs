using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunModShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject mod;
    public Material prew;
    public Material xray;
    public void OnPointerEnter(PointerEventData eventData)
    {
        mod.GetComponent<Renderer>().material = xray;
        mod.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mod.GetComponent<Renderer>().material = prew;
    }

    // Start is called before the first frame update
    void Start()
    {
        prew = mod.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
