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

    }
    public void EnableUI()
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

        // pøidám si sem sviòácky svoje ui doufam že to nic nerozbije
        QuestCanvas.Instance.gameObject.SetActive(false);

        //Debug.Log("Enabling honza ui blur");
    }
    public void DisableUI()
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

        // pøidám si sem sviòácky svoje ui doufam že to nic nerozbije
        QuestCanvas.Instance.gameObject.SetActive(true);

        //Debug.Log("Disabling honza ui blur");
    }
}
