using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] PlayerAttack meleeAttack;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
            
            if(meleeAttack.currentFireballAmount < meleeAttack.totalFireballAmount)
                meleeAttack.currentFireballAmount += 1;
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
