using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    #region Variables
    GameObject player;
    #endregion

    #region Unity Methods
    private void Start()
    {
        player = gameObject.transform.parent.gameObject; // Gets the parent object of the ground check object.
    }

    private void Update()
    {
        CheckGround(); // Calls the CheckGround method every frame to constantly check if the player is grounded.
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Checks if the player is grounded, and sets the isGrounded variable in the Movement2D script.
    /// </summary>
    private void CheckGround()
    {
        float radius = 0.2f; // The radius of the circle cast
        float distance = 0.2f; // The distance of the circle cast
        Vector2 origin = transform.position; // The position of the ground check object
        Vector2 direction = Vector2.down; // The direction of the raycast
        LayerMask layerMask = LayerMask.GetMask("Ground", "Platform"); // The layers the raycast should hit

        /* CircleCast is a 2D sphere cast that checks for colliders in a circular area.
            RaycastHit2D is a struct that stores information about the raycast hit. 
            Raycasts are basically invisible rays that are used to detect objects in the scene. */
        RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);

        // If the raycast hits a collider, the player is grounded. Otherwise, the player is not grounded.
        if (hit.collider != null)
        {
            player.GetComponent<Movement2D>().isGrounded = true;
        }
        else
        {
            player.GetComponent<Movement2D>().isGrounded = false;
        }
    }
    #endregion
}
