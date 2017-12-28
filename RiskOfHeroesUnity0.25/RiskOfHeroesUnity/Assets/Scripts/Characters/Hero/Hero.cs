using UnityEngine;
using UnityEngine.UI;

public class Hero : Character
{
    [SerializeField] private Image healthBar;
    [SerializeField] private KeyCode openLootBoxKey;
    [SerializeField] private float timeUntilLootBoxOpened;
    

    private float currentTime;
    private Text healthCount;
    private Animator anim;
    private bool bInLootBoxTrigger;
    private GameObject triggerTemp;
    public Animator getAnim()
    {
        return anim;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        healthCount = healthBar.transform.GetChild(0).GetComponent<Text>();
    }

    public override void Start()
    {
        base.Start();
        bInLootBoxTrigger = false;
    }

    public override float Health
    {
        get { return base.Health; }
        set
        {
            base.Health = value;
            if (base.Health <= 0)
            {
                Destroy(gameObject);
                UI.instance.ToggleDeathPanel();
                base.Health = 0;
            }

           
            float healthRatio = base.Health / base.MaxHealth;
            healthBar.fillAmount = healthRatio;
            healthCount.text = base.Health +"/"+ base.MaxHealth;


        }
    }

    public void FixedUpdate()
    {
        if (bInLootBoxTrigger && Input.GetKey(openLootBoxKey))
        {
            currentTime += Time.deltaTime;

            print(currentTime);

            if (currentTime >= timeUntilLootBoxOpened)
            {
                bInLootBoxTrigger = false;
                triggerTemp.tag = "LootBoxInactive";
            }
        }

        else if (currentTime > 0 && Input.GetKeyUp(openLootBoxKey))
        {
            currentTime = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LootBox")
        {
            triggerTemp = collision.gameObject;
            currentTime = 0;

            bInLootBoxTrigger = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LootBox")
        {
            print("algo falla");
            bInLootBoxTrigger = false;
            triggerTemp = null;
        }
    }

}
