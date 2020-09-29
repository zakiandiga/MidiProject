using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator anim; //Move this

    float inputX;
    float inputY;
    public KeyCode walk;
    public KeyCode sprint;
    float moveSpeed;
    public float jumpSpeed = 10f;
    public static bool isWalk = false;
    public static bool isMove = false;
    public PlayerStatus status;

    [SerializeField] float turnSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float throttle;
 
    //public float gravity;
    public float allowMove;
    public Transform cameraController;
    
    private Vector3 moveDir = Vector3.zero;
    CharacterController control;

    public GameObject weaponEquip; //OPEN THIS LATER

    BoxCollider weapon;  //In case adding equip weapon feature
    public static float weaponDamage = 10f; //In case need to be accessed by damage calculation script
    float atkSpeed = 1.3f; //Sync this with animation!
    
    void Start()
    {
        control = GetComponent<CharacterController>(); //Change to rigidbody
        //rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        status = GetComponent<PlayerStatus>();
        throttle = runSpeed; //start initial throttle at run speed

        weapon = weaponEquip.GetComponent<BoxCollider>(); //Check this existance

        ParalyzeHandler.OnParalyzeOn += ParalyzeOn;
        ParalyzeHandler.OnParalyzeOff += ParalyzeOff;
    }

    void ParalyzeOn (ParalyzeHandler p)
    {
        Debug.Log("PARALYZED");

    }

    void ParalyzeOff(ParalyzeHandler p)
    {
        Debug.Log("RECOVERED");
    }


    void PlayerMove()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        Vector3 forward = cameraController.forward;
        Vector3 right = cameraController.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        if(isWalk) //WALK MODE
        {
            var newRot = Vector3.zero;
            moveDir = forward * inputY + right * inputX;
            transform.eulerAngles = new Vector3(0, cameraController.eulerAngles.y, cameraController.eulerAngles.z);
        }


        if (isWalk == false) //RUN MODE (DEFAULT)
        {
            moveDir = forward * inputY + right * inputX;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), turnSpeed * Time.deltaTime);
        }
        
    }
    
    void InputMagnitude()
    {
        //Add state isGrounded
        
        //Input value from pressing move button/key
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        //calculation
        moveSpeed = (new Vector2(inputX, inputY).sqrMagnitude);

        if (moveSpeed > allowMove)
        {
            PlayerMove();
            isMove = true; //in case later need action with move condition
            anim.SetBool("isMove", true);
        }

        if (moveSpeed <= allowMove)
        {
            isMove = false;
            anim.SetBool("isMove", false); //ANIMATOR toggle off move
        }
    }

    //AtkSpeed function
    IEnumerator AttackDelay()    
    {
      
        yield return new WaitForSeconds(atkSpeed);
        weapon.enabled = false;
    }


    void WalkRun()  //CONVERT TO STATE MACHINE
    {
        if(Input.GetKey (walk))
        {
            isWalk = true;
            throttle = walkSpeed;
            anim.SetBool("isRun", false); //ANIMATOR walk toggle
        }
        if(Input.GetKey(sprint) && isWalk == false && status.currentStamina > 0)  //SPRINT COMMAND
        {
            //isSprint = true;
            status.isSprint = true;
            throttle = sprintSpeed;
            anim.SetBool("isSprint", true);
        }
        if(Input.GetKeyUp(sprint) || status.currentStamina < 0)
        {
            throttle = runSpeed;
            status.isSprint = false;
            anim.SetBool("isSprint", false);            
        }

        if(Input.GetKeyUp(walk))
        {
            isWalk = false;
            throttle = runSpeed;
            anim.SetBool("isRun", true); //ANIMATOR walk toggle
        }
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && weapon.enabled == false) //Attack
        {
            weapon.enabled = true;
            anim.SetTrigger("atk1"); //ANIMATOR
            StartCoroutine(AttackDelay());
        }            
        


        if (control.isGrounded)
        {
            InputMagnitude();

            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger("jump"); //ANIMATOR jump trigger
                moveDir.y = jumpSpeed;
            }
        }

        moveDir.y += Physics.gravity.y * Time.deltaTime;
        control.Move((moveDir * throttle) * Time.deltaTime);

    }

    void FixedUpdate()
    {
        //WalkRun(); //Move this

        /*
        if (control.isGrounded)
        {
            InputMagnitude();
            
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger("jump"); //ANIMATOR jump trigger
                moveDir.y = jumpSpeed;
            }
        }
        */

        //moveDir.y += Physics.gravity.y * gravity * Time.deltaTime;
        //control.Move((moveDir * throttle) * Time.deltaTime);

        //rb.MovePosition(rb.position + moveDir * throttle * Time.deltaTime);
    }
}
