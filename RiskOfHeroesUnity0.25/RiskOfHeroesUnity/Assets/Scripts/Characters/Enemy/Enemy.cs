using System.Collections;
using UnityEngine;

public class Enemy : Character
{

    #region Variables

    protected Rigidbody2D rb;
    private SpriteRenderer sprite;
    private SpriteRenderer healthBar;
    private GameObject damageText;
    

    [Header("Enemy Attributes")]
    [SerializeField] protected float spotRange = 6;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] private float maxSpeed = 3;
    [SerializeField] protected float followSpeed = 5;
    [SerializeField] private float deathTime = 30;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color followColor;
    [SerializeField] protected float offsetY = 0;
    [SerializeField] protected float lengthDetection = 1;
    protected float currentSpeed;

    protected GameObject target;

    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    protected float direction;

    public float Direction
    {
        get { return direction; }
        set { direction = value;
            //flip
            if (direction == -1) transform.eulerAngles = new Vector3(0, 180, 0);
            else transform.eulerAngles = new Vector3(0, 0, 0);
            }
    }

    protected bool focused;

    public bool Focused
    {
        get { return focused; }
        set { focused = value;
            if (focused)
            {
                currentSpeed = followSpeed;
                sprite.color = followColor;
            }
            else
            {
                currentSpeed = maxSpeed;
                sprite.color = normalColor;
            }
        }
    }

    public float SpotRange
    {
        get { return spotRange; }
        set { spotRange = value; }
    }

    #endregion

    private void Awake()
    {   
        //Get Rigidbody
        rb = GetComponent<Rigidbody2D>();
        //Get Enemy Sprite
        sprite = GetComponent<SpriteRenderer>();
        //Get HealthBar
        healthBar = transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        //Load DamageText
        damageText = Resources.Load("DamageText") as GameObject;
    }

    public override void Start()
    {
        base.Start();
        //Find player
        target = GameObject.FindWithTag("Player");
        //Set speed
        currentSpeed = maxSpeed;
        //Either left or right direction (-1/1)
        Direction = Mathf.Sign(Random.Range(-1, 1));

    }
	
	public void Update ()
    {
        //Update velocity
        if (!Dead)
        { 
            rb.velocity = new Vector2(currentSpeed * direction, rb.velocity.y);
            if (focused)
            {
                if(Vector2.Distance(transform.position, target.transform.position) < attackRange)
                {
                    currentSpeed = 0;
                }
                else
                {
                    currentSpeed = followSpeed;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        GameObject other = collision.gameObject;

        //If ground
        if (other.tag == "ground")
        {
            
            //Check if we an obstacle on our direction
            if (Physics2D.Linecast(transform.position + new Vector3(0, offsetY, 0), transform.position + new Vector3(direction * lengthDetection, offsetY, 0), 1 << LayerMask.NameToLayer("Ground")))
            {   
                //If we do then change direction
                if(!focused) Direction = -1 * Mathf.Sign(direction);
                //If we want to jump 
                else rb.AddForce(new Vector2(0f, 600));
            }
        }
    }

    public override float Health
    {
        get { return base.Health; }
        set
        {
            float damageCaused = base.Health;
            base.Health = value;
            damageCaused -=  value;
            if(damageCaused > 0) SpawnDamageText(damageCaused);

            if (base.Health <= 0)
            {
               
                StartCoroutine(Kill());
            }
            else
            {
                float healthRatio = base.Health / base.MaxHealth;
                healthBar.transform.localScale = new Vector3(healthRatio, 0.6f, 1);
            }
           
          
        }
    }

    private IEnumerator Kill()
    {
        Dead = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        sprite.color = Color.grey;
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    private void SpawnDamageText(float value)
    {
        GameObject textDamage =  Instantiate(damageText, transform.position + new Vector3(0, sprite.size.y, 0), Quaternion.identity);
        textDamage.GetComponent<TextMesh>().text = value + "";
    }

}
