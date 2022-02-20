using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UIUpgradeEnablerer : MonoBehaviour
{
    [Header("Global Volume")]
    public Volume volume;
    public List<GameObject> toggleList;
    public List<GameObject> fadeList;
    public WeaponsCanvas c;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject juaj in toggleList)
        {
            juaj.SetActive(false);
        }
        foreach (GameObject juaj in fadeList)
        {
            juaj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            
            if (c.menuOpen == false)
            {
                disableUI();
            }
            else
            {
                enableUI();
                
            }
        }

    }
    private void enableUI()
    {
        DepthOfField dof;
        if (volume.profile.TryGet<DepthOfField>(out dof))
        {
            dof.SetAllOverridesTo(true);
        }
        foreach (GameObject juaj in toggleList)
        {
            juaj.SetActive(true);
        }
        foreach (GameObject juaj in fadeList)
        {
            juaj.SetActive(true);
        }
    }
    private void disableUI()
    {
        DepthOfField dof;
        if (volume.profile.TryGet<DepthOfField>(out dof))
        {
            dof.SetAllOverridesTo(false);
        }
        foreach (GameObject juaj in toggleList)
        {
            juaj.SetActive(false);
        }

    }
}
