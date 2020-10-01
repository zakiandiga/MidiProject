using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionBehavior : MonoBehaviour
{
    float atkRoutine = 3f;
    float atkDelay = 0.5f;
    float atkStart = 0.2f;
    float patrolDelay;
    float patrolSpeed = 10f;
    float chasingSpeed = 20f;

    float chasingRadius = 16f;
    float patrolRadius = 8f;

    public GameObject weaponOne;

    [SerializeField] private bool allowGrow = false;
    BoxCollider horn;
    GameObject player;
    NavMeshAgent agent;

    bool isAttack = false;
    
    bool detectPlayer = false;
    bool isChasing = false;
    bool isRun = false;
    
    bool isPatrol = false; //STATE MACHINE
    bool isWalk = false;

    [SerializeField] bool isGrow = false;

    Vector3 chasePos;
    Vector3 patrolPos;
    Vector3 originScale = new Vector3 (0.6f, 0.6f, 0.6f);
    Vector3 growScale = new Vector3(2,2,2);
    Vector3 growing;
    Vector3 shrinking;

    float lerpScale = 10;

    Animator anim;

    void Start()
    {
        horn = weaponOne.GetComponent<BoxCollider>();  //Horn Collider script on Head Game Object!!
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        //Subscribe to EventAnnouncer events
        EventAnnouncer.OnGiantGrowthOn += Growing;
        EventAnnouncer.OnGiantGrowthOff += Shrinking;
    }



    void OnDestroy()
    {
        EventAnnouncer.OnGiantGrowthOn -= Growing;
        EventAnnouncer.OnGiantGrowthOff -= Shrinking;
    }

    void Growing(EventAnnouncer p)
    {
        transform.localScale = growScale;
        Debug.Log("Enemy Growing!!");
    }

    
    void Shrinking(EventAnnouncer p)
    {
        transform.localScale = originScale;
        Debug.Log("Enemy shrinking!!");
    }
            
    

    IEnumerator MinionPatrol()
    {
        float walkTime = Random.Range(2, 5);
        float walkWait = Random.Range(2, 5);
        float patrolX, patrolZ;
        patrolX = transform.position.x + Random.Range(-5f, 5f);
        patrolZ = transform.position.z + Random.Range(-5f, 5f);
        patrolPos = new Vector3(patrolX, transform.position.y, patrolZ);
        agent.SetDestination(patrolPos);
        //print("I am patroling again");
        isPatrol = true;
        
        yield return new WaitForSeconds(walkWait);
        isWalk = true;
        agent.speed = patrolSpeed;
        anim.SetBool("isWalk", true);
        //print("I am patroling toward " + patrolPos);
        
        yield return new WaitForSeconds(walkTime);
        isWalk = false;
        agent.speed = 0;
        anim.SetBool("isWalk", false);
        //print("I am ready for the next patrol");

        isPatrol = false;
    }

    IEnumerator ChasePlayer()
    {
        float checkTime = 0.6f;
        isPatrol = false;
        isWalk = false;
        Vector3 dirToPlayer = transform.position - player.transform.position;
        chasePos = transform.position - dirToPlayer;
        agent.SetDestination(chasePos);

        isChasing = true;
        
        isRun = true;
        agent.speed = chasingSpeed;
        anim.SetBool("isRun", true);
        //print("CHASING PLAYER!!!");
        yield return new WaitForSeconds(checkTime);

        isChasing = false;
    }

    //PLAYER DETECTION --> CHANGE TO NOTE IN OBSERVER
    
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player" && isChasing == false)
        {
            detectPlayer = true;
            GetComponent<SphereCollider>().radius = chasingRadius;
            //print("ENEMY DETECTED!");
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            detectPlayer = false;
            anim.SetBool("isRun", false);
            GetComponent<SphereCollider>().radius = patrolRadius;
            //print("ENEMY IS TOO FAR, STOP CHASING");
        }
    }
    


            
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("collide" + col.gameObject.tag);

        if (col.gameObject.tag == "Player")
        {            
            agent.SetDestination(transform.position);
            MinionStab();
        }

        if (col.gameObject.tag == "PlayerWeapon")
        {
            Destroy(this.gameObject);
            Debug.Log("Bug die");

        }


    }

    IEnumerator BugAttackDelay() //Testing attack function
    {
        isAttack = true;
        agent.speed = 0;
        yield return new WaitForSeconds(atkStart);
        anim.SetTrigger("StabAtk");
        horn.enabled = true;
        //yield return new WaitForSeconds(atkStart);
        yield return new WaitForSeconds(atkDelay);
        //BugAttack();
        horn.enabled = false;
        isAttack = false;
    }

    void MinionStab()
    {
        
        StartCoroutine(BugAttackDelay());
    }

    void Update()
    {
        if(isPatrol == false && detectPlayer == false && isAttack == false) //PATROL BEHAVIOR WHEN PLAYER NOT DETECTED
        {
            StartCoroutine(MinionPatrol());
        }

        if(isChasing == false && detectPlayer == true && isAttack == false) //CHASING BEHAVIOR WHEN PLAYER DETECTED
        {
            StartCoroutine(ChasePlayer());
        }



        //if (isAttack == false) //and player is in range
        //{
        //StartCoroutine(AttackRoutine());
        //    MinionStab();
        //}
    }
          
    



    //This attack routine should be removed after the enemy has attack behavior
    //IEnumerator AttackRoutine()
    //{

    //    yield return new WaitForSeconds(atkRoutine);
    //    isAttack = false;

    //}
}
