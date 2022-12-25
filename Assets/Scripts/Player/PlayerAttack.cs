using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Melee Attributes")]
    [SerializeField] private Transform attackPoint;

    [Header("Fireball Attributes")]
    [SerializeField] private int fireballAmount;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

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
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {

        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        //pool fireballs
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
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
}
