using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    [SerializeField] Vector3 originPos;
    [SerializeField] Vector3 nextPos;
    private float posX, posY, posZ;

    [SerializeField] int maxMinionCount = 20;
    [SerializeField] int minionSpawned;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        EventAnnouncer.OnSpawn += SpawnMinion;
        MinionBehavior.OnDie += DieCount;
        originPos = transform.position;
    }

    private void SpawnMinion(EventAnnouncer m)
    {
        if(minionSpawned <= maxMinionCount)
        {
            objectPooler.SpawnFromPool("Minion", transform.position, Quaternion.identity);
            minionSpawned += 1;
            MoveSpawner();
        }

    }

    private void MoveSpawner()
    {
        posX = Random.Range(-10, 10);
        posY = 0;
        posZ = Random.Range(-10, 10);
        nextPos = new Vector3(posX, posY, posZ);
        transform.position = nextPos;
    }

    private void DieCount(MinionBehavior mb)
    {
        minionSpawned -= 1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
