using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed = 1.0f;
    public float maxSpeed = 3.0f;
    SpriteRenderer sprite;
    Animator anim;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (Input.GetButtonUp("Horizontal"))
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

    private void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        if (inputX > 0) { sprite.flipX = false; }
        else if(inputX < 0) { sprite.flipX = true; }

        rigid.AddForce(inputX * Vector2.right * speed, ForceMode2D.Impulse);

        if(rigid.velocity.x >= maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);  // 오른쪽 속도 제한
        }else if(rigid.velocity.x <= maxSpeed * -1)
        {
            rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);  // 왼쪽 속도 제한
        }
        bool inputY = Input.GetButton("Jump");
        if (inputY)
        {
            rigid.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        }


    }
}
