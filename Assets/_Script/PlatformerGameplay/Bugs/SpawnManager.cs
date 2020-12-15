using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject originalMinion;
    GameObject minion;
    public Transform enemyParent;
    public int maxMinionCount = 5;
    [SerializeField] int currentMinionCount;

    private float posX, posY, posZ;
    float respawnTime = 3f;

    bool isRespawning = false;
    bool isFull = true;

    void Start()
    {
        EventAnnouncer.OnSpawn += MinionSpawning;
        /*
        for (int i = 0; i < maxMinionCount; ++i) 
        {
            posX = Random.Range(-20, 20);
            posY = 10;
            posZ = Random.Range(-20, 20);
            minion = Instantiate(originalMinion, new Vector3(posX, posY, posZ), Quaternion.identity, enemyParent);
            minion.SetActive(true);
            currentMinionCount = maxMinionCount;
            originalMinion.SetActive(false);
        }
        */
    }

    private void MinionSpawning(EventAnnouncer e)
    {
        posX = Random.Range(-20, 20);
        posY = 3;
        posZ = Random.Range(-20, 20);
        if (currentMinionCount < maxMinionCount)
        {
            minion = Instantiate(originalMinion, new Vector3(posX, posY, posZ), Quaternion.identity, enemyParent);
            minion.SetActive(true);
            currentMinionCount += 1;
            print("minion respawned!");
        }
    }    
    
    void Update()
    {

    }
    
}
