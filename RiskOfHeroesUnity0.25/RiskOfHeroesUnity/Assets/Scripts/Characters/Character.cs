using UnityEngine;

public class Character : MonoBehaviour {

    [Header("Attributes")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float damage = 15;
    private float health;

    public virtual float Health
    {
        get { return health; }
        set { health = value; }
    }


    public virtual float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }


    public virtual float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private bool dead;

    public virtual bool Dead
    {
        get { return dead; }
        set { dead = value; }
    }

    public virtual void Start()
    {
        Health = MaxHealth;
    }
}
