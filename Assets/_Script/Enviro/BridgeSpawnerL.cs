using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BridgeSpawnerL : MonoBehaviour
{
    ObjectPooler objectPooler;
    [SerializeField] Vector3 originPos;
    //public float platformLvl = 1;

    [SerializeField] private int noteCounted = 0;
    [SerializeField] private float beats;
    private float maxBass = 5;

    [SerializeField] private Vector3 moveBufferX = new Vector3(5, 0, 0);
    [SerializeField] private Vector3 moveBufferZ = new Vector3(0, 0, 5);

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        EventAnnouncer.OnKick += KickDrum;
        originPos = transform.position;
    }

    private void KickDrum(EventAnnouncer m)
    {
        noteCounted += 1;

        objectPooler.SpawnFromPool("BridgeL", transform.position, Quaternion.identity);
        MoveSpawner();

        //condition reacted to kickdrum based on bassCounted value
        //Instantiate cube or pooling cube
    }

    private void MoveSpawner()
    {
        if(noteCounted >=0 && noteCounted <= 8)
        {
            transform.position += moveBufferZ;
        }
        if(noteCounted >=8)
        {
            transform.position = originPos;
            noteCounted = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
