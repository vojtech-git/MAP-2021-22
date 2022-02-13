using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metal_detector : MonoBehaviour
{
    public Material svetla;
    Color zelena;

    private void Start()
    {
        zelena = svetla.GetColor("_EmissionColor");
    }

    private void OnTriggerEnter(Collider other)
    {
        //svetla.SetColor("_EmissionColor", Color.red);

        StartCoroutine(svetilka());
    }

    private IEnumerator svetilka()
    {
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", zelena);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", zelena);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", zelena);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", zelena);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", zelena);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.3f);
        svetla.SetColor("_EmissionColor", zelena);
    }
}
