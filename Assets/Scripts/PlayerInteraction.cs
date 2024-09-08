using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
  public Interactable interactable;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.gameObject.TryGetComponent<Interactable>(out var interactable))
      return;

    this.interactable = interactable;
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    this.interactable = null;
  }

  private void Update()
  {
    if (Input.GetKeyUp(KeyCode.F))
    {
      if (interactable != null) interactable.Interact();
    }
  }
}