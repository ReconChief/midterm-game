using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : MonoBehaviour
{
    public float speed = 1f;
    Rigidbody2D enemyBody;
    Transform enemyPosition;

    float enemyWidth;
    float enemyHeight;

    public LayerMask enemyMask;

    void Start()
    {
        SpriteRenderer enemySprite = this.GetComponent<SpriteRenderer>();

        enemyPosition = this.transform;
        enemyBody = this.GetComponent<Rigidbody2D>();

        enemyWidth = enemySprite.bounds.extents.x; //gets the width of the enemy sprite
        enemyHeight = enemySprite.bounds.extents.y; ///gets the height of the enemy sprite
    }

    void FixedUpdate()
    {
        //checking the groun in front of the enemy
        Vector2 lineCastPos = enemyPosition.position.toVector2() - enemyPosition.right.toVector2() * enemyWidth + Vector2.up * enemyHeight;

        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);

        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

        //always move foward

        Vector2 enemyVelocity = enemyBody.velocity;
        enemyVelocity.x = enemyPosition.right.x * -speed;
        enemyBody.velocity = enemyVelocity;

        if (!isGrounded)
        {
            Vector3 currRot = enemyPosition.eulerAngles; //property of rotation
            currRot.y += 180; //rotating sprite
            enemyPosition.eulerAngles = currRot;
        }
    }
}
