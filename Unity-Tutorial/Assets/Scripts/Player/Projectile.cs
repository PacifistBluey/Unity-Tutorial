using UnityEngine;

public class Projectile : MonoBehaviour
{
    //variables
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        //Get components from fireball
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //fireball speed
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //explode fireball when colliding
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    //fireball direction
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        float localScaleX = transform.localScale.x;

        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //Pool fireball
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}