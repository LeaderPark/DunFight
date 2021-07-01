using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Stat")]
    public float hp = 100;
    
    [Header("Player Move && anim")]
    public Transform groundChkFront;  // 바닥 체크 position 
    public Transform groundChkBack;   // 바닥 체크 position 
    public Transform RightwallChk;
    public Transform LeftwallChk;
    public Transform doublejumppoint;
    public Transform AtkPos;
    public Transform FallChk;
    public float wallJumpPower = 1f;
    public float movePower = 1f;
    public float jumpPower = 1f;
    public Vector2 boxSize;
    private bool isJumping = false;
    private bool isDoubleJumping = false;
    private bool isWallJump = false;
    private bool isUnBeatTime = false;
    private float slidingSpeed = 0.7f;
    private float doublepoint;

    public float wallchkDistance;
    public float groundChkDistance;
    public LayerMask WhatIsWall;
    public LayerMask WhatIsGround;
    
    private bool isRightWall;
    private bool isLeftWall;
    private bool isGround;
    private bool isFall;
    private bool isEnter;
    private float isRight = 1;

    [Header("Atk")]
    public int damage = 20;
    public int atkNum;
    public int speed;
    public float minPos;
    public float maxPos;
    public Slider slider;
    public Text text;
    private RectTransform pass;
    private bool isAtk = false;
    private bool isJumpingAtk = true;
    
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer sprite;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        //slider.value = 0;
    }

    void Update()
    {
        slider.value = hp;
        this.text.text = hp + " / 100";
        
        
        bool ground_front = Physics2D.Raycast(groundChkFront.position, Vector2.down, groundChkDistance, WhatIsGround);
        bool ground_back = Physics2D.Raycast(groundChkBack.position, Vector2.down, groundChkDistance, WhatIsGround);

        isRightWall = Physics2D.Raycast(RightwallChk.position, Vector2.right * isRight, wallchkDistance, WhatIsWall);
        isLeftWall = Physics2D.Raycast(LeftwallChk.position, Vector2.left * isRight, wallchkDistance, WhatIsWall);

        if (!isGround && (ground_front || ground_back))
            rigid.velocity = new Vector2(rigid.velocity.x, 0);

        // 앞 또는 뒤쪽의 바닥이 감지되면 isGround 변수를 참으로!
        if (ground_front || ground_back)
            isGround = true;
        else
            isGround = false;

        animator.SetBool("isGround", isGround);

        if(isRightWall || isLeftWall){
            animator.SetBool("isSliding", true);
        }
        else{
            animator.SetBool("isSliding", false);
        }

        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping") && !DataManager.Instance.isDie)
        {
            animator.SetBool("isJumping", true);
            animator.SetTrigger("doJumping");
            isJumping = true;
            Jump();
        }
        else if(isDoubleJumping && Input.GetButtonDown("Jump") && doublepoint > 2.8f && !DataManager.Instance.isDie)
        {
            animator.SetBool("isDoubleJumping", true);
            animator.SetTrigger("doDoubleJumping");
            DoubleJump();
        }

        if(animator.GetBool("isJumping") && Input.GetKeyDown(KeyCode.A) && isJumpingAtk && !DataManager.Instance.isDie)
        {
            isJumpingAtk = false;
            Atk();  
        }
        else if(Input.GetKeyDown(KeyCode.A) && !isAtk && !DataManager.Instance.isDie && isJumpingAtk)
        {   
            isAtk = true;
            Atk();
        }

        if(hp <= 0)
        {
            Die();
        }

        if(isGround && rigid.velocity.y < 1)
        {
            isJumpingAtk = true;
            animator.SetBool ("isJumping", false);
            animator.SetBool ("isDoubleJumping", false);
        }

        RaycastHit2D hit =  Physics2D.Raycast(doublejumppoint.position, Vector2.down, 3, WhatIsGround);
        if(hit){
            doublepoint = ((Vector2)transform.position - hit.point).magnitude;
            // if(doublepoint > 3f)
            //     Debug.Log(doublepoint);
        }

        if(doublepoint > 4f)
        {
            isFall = true; 
            // animator.SetTrigger("doFall");
            // Debug.Log("낙하");
            Debug.Log(isFall);
        }

        if(isEnter)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                DataManager.Instance.isFade = true;
                Debug.Log("dd");
            }
        }
    }

    void FixedUpdate()
    {
        if(DataManager.Instance.isDie)
            return;
        
        Move();
        if(isRightWall)
        {
            isWallJump = false;
            Slide();
            // if(Input.GetAxisRaw("Jump") != 0)
            // {
            //     animator.SetTrigger("doWallJump");
            //     isWallJump = true;
            //     Invoke("FreezeX", 0.8f);
            //     rigid.velocity = new Vector2(-isRight * wallJumpPower, 0.9f * wallJumpPower);
            //     isRight = isRight * -1;
            //     sprite.flipX = true;
            // }
        }

        if(isLeftWall)
        {
            isWallJump = false;
            Slide();
            // if(Input.GetAxisRaw("Jump") != 0)
            // {
            //     animator.SetTrigger("doWallJump");
            //     isWallJump = true;
            //     Invoke("FreezeX", 0.8f);
            //     rigid.velocity = new Vector2(-isRight * wallJumpPower, 0.9f * wallJumpPower);
            //     isRight = isRight * -1;
            //     sprite.flipX = false;
            // }
        }
    }

    void FreezeX()
    {
        isWallJump = false;
    }

    void Move()
    {
        Vector3 moveVelcity = Vector3.zero;

        if(Input.GetAxisRaw ("Horizontal") == 0)
        {
            animator.SetBool("isMoveing", false);
        }
        else if(Input.GetAxisRaw ("Horizontal") < 0 && !isWallJump)
        {
            moveVelcity = Vector3.left;
            isRight *= -1;
            sprite.flipX = true;
            animator.SetBool("isMoveing", true);
        }
        else if(Input.GetAxisRaw ("Horizontal") > 0 && !isWallJump)
        {
            moveVelcity = Vector3.right;
            isRight *= 1;
            sprite.flipX = false;
            animator.SetBool("isMoveing", true);
        }

        transform.position += moveVelcity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if(!isJumping)
            return;

        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2 (0, jumpPower);
        rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);
        isJumping = false;
        isDoubleJumping = true;
    }

    void DoubleJump()
    {
        if(!isDoubleJumping)
            return;

        rigid.velocity = Vector2.zero; 
        Vector2 jumpVelocity = new Vector2 (0, jumpPower);
        rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);
        isDoubleJumping = false;
    }

    void PlayAnimation(int atkNum)
    {
        animator.SetFloat("Blend", atkNum);
        animator.SetTrigger("doAtk");
    }

    void SetAtk()
    {
        slider.value = 0;
        minPos = pass.anchoredPosition.x;
        maxPos = pass.sizeDelta.x + minPos;
        // StartCoroutine(ComboAtk());
    }

    void Die()
    {
        DataManager.Instance.isDie = true;
        animator.SetTrigger("doDie");
    }

    void Slide()
    {
        rigid.velocity = new Vector2(rigid.velocity.x , rigid.velocity.y * slidingSpeed);
        if(isGround)
        {
            animator.Play("PlayerIdle");
        }
    }

    void Atk()
    {
        animator.SetTrigger("doAtk");
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(AtkPos.position, boxSize, 0);
        foreach(Collider2D collider2D in collider2Ds)
        {
            if(collider2D.CompareTag("ENEMY"))
            {
                collider2D.GetComponent<Enemy>().TakeDamage(damage);
                Debug.Log("피격");
            }
            else if(collider2D.CompareTag("BOSSMOB"))
            {
                collider2D.GetComponent<Boss>().TakeDamage(damage);
                Debug.Log("피격");
            }
        }
        StartCoroutine(delay());
        
    }

    // IEnumerator ComboAtk()
    // {
    //     yield return null;
    //     while(!(Input.GetKeyDown(KeyCode.A) || slider.value == slider.maxValue))
    //     {
    //         if(animator.GetBool("isJumping"))
    //         {
    //             isAtk = false;
    //             goto that;
    //         }
    //         else
    //         {
    //             slider.value += Time.deltaTime * speed;
    //             yield return null;
    //         }
    //     }
    //     if(slider.value >= minPos && slider.value <= maxPos)
    //     {
    //         PlayAnimation(atkNum++);
    //         if(atkNum < 3)
    //         {
    //             SetAtk();
    //         }
    //         else
    //         {
    //             atkNum = 0;
    //             SetAtk();
    //             // isAtk = false;
    //         }
    //         if(atkNum > 2)
    //         {
    //             atkNum = 0;
    //         }
    //     }
    //     else
    //     {
    //         PlayAnimation(0);
    //         isAtk = false;
    //         atkNum = 0;
    //     }

    //     that:
    //     slider.value = 0;
    //     Atk();
    //     yield return null;
    // }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if(col.gameObject.CompareTag("ENEMY") && !(rigid.velocity.y < -10f) && !isUnBeatTime)
        {
            Vector2 attack = Vector2.zero;
            if(col.gameObject.transform.position.x > transform.position.x)
                attack = new Vector2 (-2f, 7f);
            else
                attack = new Vector2 (2f, 7f);

            rigid.AddForce(attack, ForceMode2D.Impulse);

            hp -= 20; //데미지 

            if(hp > 1)
            {
                isUnBeatTime = true;
                StartCoroutine(UnBeatTime());
            }
        }
    }

    IEnumerator UnBeatTime()
    {
        int countTime = 0;

        while(countTime < 10)
        {
            if(countTime%2 == 0)
                sprite.color = new Color32(255,255,255,90);
            else
                sprite.color = new Color32(255,255,255,180);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        sprite.color = new Color32(255,255,255,255);

        isUnBeatTime = false;

        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundChkFront.position, Vector2.down * groundChkDistance);
        Gizmos.DrawRay(groundChkBack.position, Vector2.down * groundChkDistance);
        Gizmos.DrawRay(RightwallChk.position, Vector2.right * isRight * wallchkDistance);
        Gizmos.DrawRay(LeftwallChk.position, Vector2.left * isRight * wallchkDistance);
        Gizmos.DrawRay(doublejumppoint.position, Vector2.down * 3);
        Gizmos.DrawWireCube(AtkPos.position, boxSize);
        Gizmos.DrawRay(FallChk.position, Vector2.down * 2.4f);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("ENTER"))
        {
            isEnter = true;
        }
        else if(col.gameObject.CompareTag("BOSS") && !isUnBeatTime)
        {
            Vector2 attack = Vector2.zero;
            if(col.gameObject.transform.position.x > transform.position.x)
                attack = new Vector2 (-2f, 7f);
            else
                attack = new Vector2 (2f, 7f);

            rigid.AddForce(attack, ForceMode2D.Impulse);

            hp -= 20; //데미지 

            if(hp > 1)
            {
                isUnBeatTime = true;
                StartCoroutine(UnBeatTime());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col) 
    {
        if(col.gameObject.CompareTag("ENTER"))
        {
            isEnter = false;
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        isAtk = false;
    }
}
