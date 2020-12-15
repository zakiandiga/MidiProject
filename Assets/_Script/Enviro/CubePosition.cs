using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePosition : MonoBehaviour, IPooledObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnObjectSpawn()
    {
        float posX = Random.Range(-5, 5);
        float posZ = Random.Range(-5, 5);
        float posY = Random.Range(0, 2f);
        Vector3 pos = new Vector3(posX, posY, posZ);
        transform.position = pos;

    }
}
