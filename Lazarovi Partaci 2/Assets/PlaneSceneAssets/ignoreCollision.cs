using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreCollision : MonoBehaviour
{
    public BoxCollider asteroidCollider;
    private SphereCollider bulletCollider;
    // Start is called before the first frame update
    void Start()
    {
        bulletCollider = this.GetComponent<SphereCollider>();
        Physics.IgnoreCollision(asteroidCollider, bulletCollider, true);
    }
}
