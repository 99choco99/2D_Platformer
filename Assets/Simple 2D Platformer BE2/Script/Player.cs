using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxSpeed;
    public float jumpPower;
    public float maxHealth;
    float health;


    SpriteRenderer sprite;
    Animator anim;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        health = maxHealth;
    }

    private void Update()
    {

        if (Input.GetButtonDown("Vertical") && !anim.GetBool("Isjump"))
        {
            anim.SetBool("Isjump",true);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        if (!Input.GetButton("Horizontal"))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            anim.SetBool("Isinput", true);
        }
        else
        {
            anim.SetBool("Isinput", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
        }
    }


    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 9;
        sprite.color = new Color(1, 1, 1, 0.4f);

        int dir = targetPos.x - transform.position.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dir, 1), ForceMode2D.Impulse);

        Invoke("OffDamage",1);
    }

    void OffDamage()
    {
        gameObject.layer = 8;
        sprite.color = new Color(1, 1, 1, 1);
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        if (inputX > 0) { sprite.flipX = false; }
        else if(inputX < 0) { sprite.flipX = true; }

        rigid.AddForce(inputX * Vector2.right, ForceMode2D.Impulse);
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);  // 오른쪽 속도 제한
        }
        else if(rigid.velocity.x < maxSpeed * -1)
        {
            rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);  // 왼쪽 속도 제한
        }

        if (rigid.velocity.y < 0)
        {
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayhit.collider != null)
            {
                if (rayhit.distance < 0.5f)
                {
                    anim.SetBool("Isjump", false);
                }
            }
        }
    }
}
