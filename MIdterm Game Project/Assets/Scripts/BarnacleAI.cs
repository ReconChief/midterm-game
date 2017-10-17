using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnacleAI : MonoBehaviour
{
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
        //checking the ground in front of the enemy
        Vector2 lineCastPos = enemyPosition.position.toVector2() - enemyPosition.right.toVector2() * enemyWidth + Vector2.up * enemyHeight;

        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
    }
}
