using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeKeys : MonoBehaviour
{
    public float mSize = 0.0f;
    private void Start()
    {
        InvokeRepeating("ScaleUP", 0.0f, 0.01f);
    }
    void ScaleUP()
    {
        if (mSize == 100.0f)
        {
            InvokeRepeating("ScaleDown", 0.0f, 0.01f);
            CancelInvoke("ScaleUP");
            
        }
        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, mSize++);
    }
    void ScaleDown()
    {
        if (mSize == 0.0f)
        {
            InvokeRepeating("ScaleUP", 0.0f, 0.01f);
            CancelInvoke("ScaleDown");
            
        }
        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, mSize--);
    }
}
