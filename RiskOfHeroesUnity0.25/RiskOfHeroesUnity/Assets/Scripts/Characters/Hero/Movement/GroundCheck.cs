using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private Hero hero;
    private bool grounded = false;

    public bool Grounded
    {
        get { return grounded; }
        set { grounded = value; }
    }

    private void Awake()
    {
        hero = GetComponentInParent<Hero>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {   
            grounded = true;
            hero.getAnim().SetBool("Grounded", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = false;
            hero.getAnim().SetBool("Grounded", false);
        }
    }
}
