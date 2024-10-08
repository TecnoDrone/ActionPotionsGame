using UnityEngine;

public class BasicMovement : MonoBehaviour
{
  public float speed = 5f;
  private Vector2 movement;

  void Update()
  {
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");
  }

  void FixedUpdate()
  {
    transform.Translate(movement * speed * Time.fixedDeltaTime);
  }
}
