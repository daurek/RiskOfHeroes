using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private float attackCoolDown = 0.5f;
    private float nextAttackCooldown = 0;
    private Enemy enemy;
    private bool playerInTrigger;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void Update()
    {   
        //Check if player inside trigger and if cd is over
        if (playerInTrigger && nextAttackCooldown <= Time.time)
        {
            //Add the cooldown
            nextAttackCooldown = Time.time + attackCoolDown;
            enemy.Target.GetComponent<Hero>().Health -= enemy.Damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Player")
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Player")
        {
            playerInTrigger = false;
        }
    }
}
