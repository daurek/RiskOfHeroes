using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {


    [Header("Boss Attributes")]
    [SerializeField] private float jumpDamage;
    [SerializeField] private float jumpPushForce;
    private bool fightStarted;
    private bool jumpAbility;

    public override void Start () {
        base.Start();	
	}
	
	new void Update () {
        if (!Dead && !jumpAbility)
        {   
            rb.velocity = new Vector2(currentSpeed * direction, rb.velocity.y);
            if ((focused || fightStarted) && target)
            {
                fightStarted = true;
                if (Vector2.Distance(transform.position, target.transform.position) < attackRange)
                {
                    currentSpeed = 0;
                }
                else if (Vector2.Distance(transform.position, target.transform.position) > spotRange + 5)
                {
                    jumpAbility = true;
                    StartCoroutine(JumpAbility());
                }
                else
                {
                    currentSpeed = followSpeed;
                }
            }
        }


    }

    private IEnumerator JumpAbility()
    {
        Vector2 playerPos = target.transform.position;
        GetComponent<BoxCollider2D>().enabled = false;
        rb.AddForce(Vector2.up * 5000);

        yield return new WaitForSeconds(2);
        GetComponent<BoxCollider2D>().enabled = true;
        rb.Sleep();
        transform.position = playerPos;
        if (target) { 
            float distanceToBoss = Vector2.Distance(transform.position, target.transform.position);
            if (distanceToBoss < 5)
            {
                int direction;
                if (transform.position.x > target.transform.position.x) direction = -1;
                else direction = 1;

                float jumpDamageTemp = jumpDamage;
                float jumpPushForceTemp = jumpPushForce;

                if (distanceToBoss < 2)
                {
                    jumpDamageTemp *= 1.2f;
                    jumpPushForceTemp *= 1.5f;
                }

                target.GetComponent<Rigidbody2D>().AddForce(direction * Vector2.right * jumpPushForceTemp);
                target.GetComponent<Hero>().Health -= jumpDamageTemp;
            }
        }
        yield return new WaitForSeconds(1);
        jumpAbility = false;
    }
}
