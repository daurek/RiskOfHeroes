using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        GetComponent<CircleCollider2D>().radius = enemy.SpotRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Player")
        {
            enemy.Focused = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Player")
        {   
            Transform targetTransform = other.transform;
            if (enemy.Focused)
            {
                if (targetTransform.position.x < transform.position.x)
                {
                    enemy.Direction = -1;
                }
                else
                {
                    enemy.Direction = 1;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Player")
        {
            enemy.Focused = false;
        }
    }
}
