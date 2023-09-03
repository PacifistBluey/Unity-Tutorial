using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private float horizontalInput;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;//How much time the player can hang in the air before jumping
    private float coyoteCounter;//How much time passed since the player ran off the edge

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float WallJumpX;
    [SerializeField] private float WallJumpY;
    private float wallJumpCooldown;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    //Core parameters
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private NPC_Interaction npc;

    

    private void Awake()
    {
        //Grab references for components from player
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if (!inDialogue())
        {
            horizontalInput = Input.GetAxis("Horizontal");

            //Flip player when moving horizontal
            if (horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);


            //Set animator parameters
            anim.SetBool("run", horizontalInput != 0);
            anim.SetBool("grounded", isGrounded());

            //Jump
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            //Adjustable jump height
            if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
                body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

            if (onWall())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 7;
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

                if (isGrounded())
                {
                    coyoteCounter = coyoteTime;//Reset coyote counter when on the ground
                    jumpCounter = extraJumps;//Reset jump counter to extra jump value
                }
                else
                    coyoteCounter -= Time.deltaTime;//Start decreasing coyote counter when not on the ground
            }
        }
        else
            anim.SetBool("run", false);
    }

    private void Jump()
    {
        if (coyoteCounter < 0 && !onWall() && jumpCounter <= 0) return;
        //if coyote is 0 or less and not on the wall, don't execute code

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            wallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                //if not on the ground and coyote counter bigger than 0, do a normal jump
                if (coyoteCounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if (jumpCounter > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            //reset coyote counter to zero
            coyoteCounter = 0;
        }
    }

    private void wallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * WallJumpX, WallJumpY));
        wallJumpCooldown = 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    private bool inDialogue()
    {
        if (npc != null)
            return npc.DialogueActive();
        else
            return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npc = collision.gameObject.GetComponent<NPC_Interaction>();

            if (Input.GetKey(KeyCode.Space))
                npc.ActivateDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
    }
}