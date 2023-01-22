using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private PlayerAttack playerAttack;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        //Check if checkpoint available
        if (currentCheckpoint == null)
        {
            //Show game over screen
            uiManager.GameOver();

            return;//Don't execute the rest of this function
        }

        transform.position = currentCheckpoint.position;//Move player to checkpoint position

        //Restore player health and reset animation
        playerHealth.Respawn();

        //Move Camera back to checkpoint room
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    //Activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;//Store activated checkpoint as the current one
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;//Deactive checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear");//Trigger checkpoint animation
            playerHealth.currentHealth = playerHealth.startingHealth;//Restore player health
        }
    }
}
