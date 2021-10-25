using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 center;
    public GameObject prefabObject;
    public Vector3 size;
    public Quaternion min;
    public int pepa = 100;
    void Start()
    {
      spawnMore();
    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.L)){
            spawnObject();
        }
    }

    private void spawnMore(){
        for (int i = 0; i < 50; i++)
        {
            spawnObject();
        }
    }

    public void spawnObject(){
        Vector3 pos = center + new Vector3(Random.Range(-size.x/2, size.x/2 ), Random.Range(-size.y / 2, size.y/2 ),Random.Range(-size.z / 2, size.z/2 ));
        Instantiate(prefabObject, pos, Quaternion.identity);
    }   

    void OnDrawGizmosSelected(){
        Gizmos.color = new Color(1,0,0,5f);
        Gizmos.DrawCube(center, size);
    }
}
