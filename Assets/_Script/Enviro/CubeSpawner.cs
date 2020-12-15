using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    [SerializeField] Vector3 originPos;
    public float platformLvl = 1;

    [SerializeField] private int noteCounted = 0;
    [SerializeField] private float beats;
    private float maxBass = 5;

    [SerializeField] private Vector3 moveBufferX = new Vector3(2, 0, 0);
    [SerializeField] private Vector3 moveBufferZ = new Vector3(0, 0, 2);



    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        EventAnnouncer.OnKick += KickDrum;
        originPos = transform.position;
    }

    private void KickDrum(EventAnnouncer m)
    {
        noteCounted += 1;

        objectPooler.SpawnFromPool("Step", transform.position, Quaternion.identity);
        MoveSpawner();

        //condition reacted to kickdrum based on bassCounted value
        //Instantiate cube or pooling cube
    }

    private void MoveSpawner()
    {
        if(noteCounted >= 0 && noteCounted <= 5)
        {
            transform.position += moveBufferX;
        }
        if (noteCounted > 5 && noteCounted <= 10)
        {
            transform.position += moveBufferZ;
        }
        if(noteCounted >10 && noteCounted <= 15)
        {
            transform.position -= moveBufferX;
        }
        if (noteCounted >15 && noteCounted <= 19)
        {
            transform.position -= moveBufferZ;
        }
        if(noteCounted >= 20)
        {
            transform.position = originPos;
            noteCounted = 0;
            platformLvl += 1;
            if(platformLvl <= 5)
            {
                originPos.y += 0.5f;
                transform.position = originPos;
            }
            if(platformLvl >5)
            {
                platformLvl = 1;
                originPos.y = 0.25f;
                transform.position = originPos;
            }
        }


    }



}
