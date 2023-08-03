using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public int Hp; // 체력
    public int Attack; // 공격
    public float Speed; // 속도
    public int nextMove;

    public bool isDead = false; //사망 여부
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 5);
    }

    void FixedUpdate()
     {
         //움직임
         rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

         //발판 확인
         Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
         Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
         RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("platform"));
         if (rayHit.collider == null)
         {
            Turn();
        }
     }

    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);

        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        // Filp Sprite
        if(nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 5);
    }

    public void Hit()
    {
        //Hp = Hp - damage;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void EnemyDie ()
    {


    }
   

}
