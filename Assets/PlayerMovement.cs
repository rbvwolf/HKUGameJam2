using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private enum MovementState { idle, running, jumping, falling }
    private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState(dirX);
        
    }
    private void UpdateAnimationState(float dirX)
    {
        MovementState state;

        if ((float) dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if ((float) dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("Animator", (int)state);
    }
}
