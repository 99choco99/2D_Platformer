using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anim;
    public int nextMove;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        if(nextMove == -1)
        {
            sprite.flipX = false;
            anim.SetBool("IsWalking", true);
        }
        else if(nextMove == 1)
        {
            sprite.flipX = true;
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1 ,LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            nextMove = nextMove * -1;
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
}
