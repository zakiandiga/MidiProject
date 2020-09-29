using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStatus : MonoBehaviour
{
    public float maxHealth = 5f;
    public float healthPoints;
    public bool isDead = false;
    bool isHit = false;
    Animator anim;
    //NavMeshAgent agent;

    void Start()
    {
        healthPoints = maxHealth;
        anim = GetComponent<Animator>();
        //agent = GetComponent<NavMeshAgent>();
    }

    void MinionDead()
    {        
        anim.SetBool("isDie", true);
        //GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<MinionBehavior>().enabled = false;
        print("MINION DEAD!!!!");
        Destroy(gameObject, 3);
        //spawnManager.currentMinionCount -= 1;
    }

    void OnTriggerEnter(Collider col)
    {
        
        if(col.gameObject.tag == "playerWeapon" && isHit == false)
        {
            isHit = true;
            healthPoints -= col.gameObject.GetComponent<WeaponStatus>().damage;
            //print("ENEMY HIT " + healthPoints + " DAMAGE");
            anim.SetTrigger("gotHit"); //ANIMATION
            //print("OUCH");                        
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(isHit == true)
        {
            isHit = false;
        }
    }

    void Update()
    {
        if(healthPoints <= 0 && isDead == false)
        {
            isDead = true;
            MinionDead();
        }
    }
}
