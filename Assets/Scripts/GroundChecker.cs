using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
  public bool isGrounded { get; private set; } = true;  // Property to track if the player is grounded

  // Event to trigger when isGrounded changes
  public UnityEvent<bool> OnGroundedChanged;

  private BoxCollider2D trigger;

  void Start()
  {
    OnGroundedChanged ??= new UnityEvent<bool>();
    if (TryGetComponent(out trigger)) 
    {
      trigger.isTrigger = true;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.CompareTag("Ground")) return;
    // Set grounded to true when the trigger is entered
    if (!isGrounded)
    {
      SetGroundedState(true);
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (!collision.CompareTag("Ground")) return;
    // Set grounded to false when the trigger is exited
    if (isGrounded)
    {
      SetGroundedState(false);
    }
  }

  private void SetGroundedState(bool grounded)
  {
    if (isGrounded != grounded)
    {
      isGrounded = grounded;
      OnGroundedChanged.Invoke(isGrounded); // Trigger the event
    }
  }
}
