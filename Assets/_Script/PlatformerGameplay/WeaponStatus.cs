using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    //THIS SCRIPT SHOULD BE ATTACHED TO WEAPONS    
    public int damage = 1;  //SET IN EDITOR
    public bool playerMode;  //SET IN EDITOR
    //public bool enemyMode;
    GameObject player;
    GameObject enemy;

    Collider enemyCol;
    Collider playerCol;

    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        playerCol = player.GetComponent<Collider>();
        enemyCol = enemy.GetComponent<Collider>();

        if (playerMode)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
        if(!playerMode)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), enemy.GetComponent<Collider>(), true);
        }
    }

}
