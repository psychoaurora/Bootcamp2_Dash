using UnityEngine;

public class Movement2D : MonoBehaviour
{
    // Regions help visually organize your code into sections
    #region Variables
    // Headers are like titles for the Unity inspector
    [Header("Movement Variables")]

    /* In C# if you do not specify a variable modifier (i.e. public, private, protected), it defaults to private
    The private variable modifier stops other scripts from accessing those variables */
    Rigidbody2D rb;
    SpriteRenderer sprite;

    // SerializeField allows you to see private variables in the inspector while keeping them private
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 18f;
    public bool isGrounded;

    bool jumpRequested; // A boolean to check if the player has requested a jump.
    float movement; // The horizontal movement of the player
    #endregion // Marks the end of the region

    #region Unity Methods
    // Start is called before the first frame update
    private void Start()
    {
        // GetComponent is used to get the component of the object this script is attached to
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Input.GetAxisRaw returns a float value of either -1 or 1 based on if the player is pressing left or right. It is 0 if the player is not pressing anything.
        // Use GetAxis instead if you want smooth movement with acceleration and deceleration.
        movement = Input.GetAxisRaw("Horizontal");

        UpdateSpriteDirection();

        // If the player presses the space key, set jumpRequested to true
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true;
        }
    }

    // FixedUpdate is used for physics calculations and is called 50 times a second
    private void FixedUpdate()
    {
        // You don't need to multiply the movement by Time.deltaTime because the physics calculations are already frame-rate independent
        rb.linearVelocity = new Vector2(movement * moveSpeed, rb.linearVelocity.y);

        // Handle the jump request
        if (jumpRequested)
        {
            Jump();
            jumpRequested = false; // Reset the jump request flag
        }
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// This method is used to update the sprite direction based on the player's movement
    /// </summary>
    private void UpdateSpriteDirection()
    {
        // Flips the sprite based on the direction the player is moving
        if (movement > 0f)
        {
            sprite.flipX = true;
        }
        else if (movement < 0f)
        {
            sprite.flipX = false;
        }
    }

    /// <summary>
    /// This method is used to make the player jump
    /// </summary>
    private void Jump()
    {
        // If the player is not grounded, return out of method 
        if (!isGrounded)
        {
            return;
        }

        // If the player is grounded and space is pressed, set the y velocity of the player to the jumpforce
        Debug.Log("Player Jumped");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Sets the y velocity of the player to the jumpforce. Preserves the x velocity.

    }
    #endregion
}