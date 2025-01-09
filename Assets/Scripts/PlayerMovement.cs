using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedJump;
    private Rigidbody2D body;
    private Animator anim;
    private Vector2 initialScale;
    public bool collidedStop;
    private bool grounded;

    public AudioSource footstepsSound;
    private void Awake()
    {
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        initialScale = transform.localScale;

    }
        
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when facing left/right.
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector2(initialScale.x * -1, initialScale.y);
            
        }


        else if (horizontalInput < -0.01f)
        {
            transform.localScale = initialScale;
            

        }

        if (Mathf.Abs(horizontalInput) > 0.01f && grounded) // Player is moving and grounded
        {
            if (!footstepsSound.isPlaying) // Prevent restarting the sound if it's already playing
            {
                footstepsSound.Play();
            }
        }
        else // Player is stationary or not grounded
        {
            footstepsSound.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Space) == true && grounded == true)
        {
            Jump();
        }

        
            

        //sets animation parameters
        
        //anim.SetBool("run", horizontalInput != 0);
        //anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.AddForce(new Vector2(0, speedJump));
        //anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        if (collision.gameObject.tag == "book1")
        {
            collidedStop = true;
        }
    }
}