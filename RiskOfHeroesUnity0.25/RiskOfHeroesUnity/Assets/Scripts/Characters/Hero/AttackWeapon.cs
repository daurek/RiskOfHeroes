using System.Collections.Generic;
using UnityEngine;

public class AttackWeapon : MonoBehaviour
{
    /// <summary>
    /// Enemies inside the collider 
    /// </summary>
    List<GameObject> targets = new List<GameObject>();
    List<GameObject> knockBackTargets = new List<GameObject>();

    [SerializeField] private KeyCode attackKey;
    [SerializeField] private float attackCoolDown = 0.5f;
    [SerializeField] private Vector2 knockbackForce = new Vector2(500, 150);
    private float nextAttackCooldown = 0;
    private Hero hero;
    private bool knocking;

    private void Awake()
    {
        hero = GetComponentInParent<Hero>();
    }

    void Update () {
        //Check if Key has been pressed and cooldown is over
        if (Input.GetKeyDown(attackKey) && nextAttackCooldown <= Time.time && !hero.getAnim().GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            hero.getAnim().SetTrigger("BasicAttack");
            //Add the cooldown
            nextAttackCooldown = Time.time + attackCoolDown;
            //If there are enemies inside the collider
            if(targets.Count > 0)
            {   
                //Loop through them
                for (int i = 0; i < targets.Count; i++)
                {
                    Enemy enemy = targets[i].GetComponent<Enemy>();
                    //If the position is empty (enemy dead)
                    if (enemy.Dead)
                    {   
                        //Erase from list
                        targets.Remove(targets[i]);
                    }
                    //If the enemy is alive then reduce damage
                    else
                    {   
                        
                        enemy.Health -= hero.Damage;
                        knockBackTargets.Add(enemy.gameObject);
                        knocking = true;
                    }
                }
            }
        }
	}

    private void FixedUpdate()
    {   
        //Used only for knocking enemies back, has to be on fixed update because physics, input remains on Update only
        if (knocking)
        {
            knocking = false;
            foreach (var target in knockBackTargets)
            {
                if (target.gameObject)
                {
                    Rigidbody2D targerRb = target.GetComponent<Rigidbody2D>();
                    if (!target.GetComponent<Enemy>().Dead)
                    {
                        targerRb.AddForce(hero.GetComponent<Movement>().Direction * knockbackForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    }
                    else
                    {
                        targerRb.AddForce(hero.GetComponent<Movement>().Direction * (knockbackForce/3) * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    }
                }
            }
            knockBackTargets.Clear();
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Enemy")
        {
            if (!targets.Contains(other) && !other.GetComponent<Enemy>().Dead)
            {
                targets.Add(other);
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy")
        {
            if (targets.Contains(other))
            {
                targets.Remove(other);
            }
            
        }
    }

   
}
