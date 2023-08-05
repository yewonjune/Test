using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public int Hp; // ü��
    public int Attack; // ����
    public float Speed; // �ӵ�
    public int nextMove;

    public GameObject prfHpBar;
    public GameObject canvas;

    RectTransform hpBar;

    public float height = 0.8f;

    public bool isDead = false; //��� ����
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 5);
    }

    void FixedUpdate()
    {
        //������
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //���� Ȯ��
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
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
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        // ����ũ ����
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
        // ü�¹� ����
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
    }

    void Update()
    {
        // ü�¹� ���� ��ǥ�� UI ��ǥ�� �ٲ��ִ� �Լ�
        Vector3 _hpBarPos =
            Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));

        //��ũ�� ��ǥ�� �ٲ� ������ ü�¹� �̵�
        hpBar.position = _hpBarPos;
    }

    void EnemyDie()
    {


    }


}
