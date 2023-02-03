using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Melee Attributes")]
    [SerializeField] private AttackPoint attackPoint;
    [SerializeField] private AudioClip attackSound;

    [Header("Fireball Attributes")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] public float totalFireballAmount;
    public float currentFireballAmount;

    [Header("Cooldown Variables")]
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;

    //Refrences
    private Animator anim;
    private PlayerMovement playerMovement;

    private void Awake()
    {
       anim = GetComponent<Animator>();
       playerMovement = GetComponent<PlayerMovement>();
       currentFireballAmount = totalFireballAmount;
    }

    private void Update()
    {
        
        if (cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            //Melee attack
            if (Input.GetKey(KeyCode.LeftAlt))
                anim.SetTrigger("meleeAttack");
            else
                anim.ResetTrigger("meleeAttack");

            //Range attack
            if (Input.GetKey(KeyCode.Z) && currentFireballAmount != 0)
                anim.SetTrigger("attack");
            else
                anim.ResetTrigger("attack");
        }
            

        

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);

        //pool fireballs
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        currentFireballAmount -= 1;
    }

    private int FindFireball()
    {
        for(int i = 0; i < fireballs.Length; i++)
        {
            if(!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void MeleeAttack()
    {
        attackPoint.Activate();
    }

    private void DontAttack()
    {
        attackPoint.Deactivate();
    }
}
