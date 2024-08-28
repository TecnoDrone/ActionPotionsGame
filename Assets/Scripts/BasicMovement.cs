using UnityEngine;

public class BasicMovement : MonoBehaviour
{
  public float speed = 5f;          // Public variable for movement speed
  public float jumpHeight = 5f;     // Public variable for jump height

  private Rigidbody2D rb;           // Reference to the Rigidbody2D component
  private GroundChecker groundChecker; // Reference to the GroundChecker component
  public bool isGrounded = true;          // Local variable to track grounded state

  void Start()
  {
    rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the object

    // Find the GroundChecker component in the child object
    groundChecker = GetComponentInChildren<GroundChecker>();
    if (groundChecker != null)
    {
      // Subscribe to the OnGroundedChanged event
      groundChecker.OnGroundedChanged.AddListener(OnGroundedChanged);
    }
  }

  void Update()
  {
    // Horizontal movement
    float move = Input.GetAxis("Horizontal");
    rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

    // Jumping
    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
      rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }
  }

  void OnGroundedChanged(bool grounded)
  {
    // Update the local isGrounded state when the event is triggered
    isGrounded = grounded;
  }
}
