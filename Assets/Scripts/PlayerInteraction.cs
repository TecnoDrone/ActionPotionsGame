using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
  private Dictionary<int, Interactable> interactablesInRange = new Dictionary<int, Interactable>();

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.F))
    {
      foreach (var interactable in interactablesInRange.Values)
      {
        interactable.Interact(gameObject);
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    Interactable interactable = collision.GetComponent<Interactable>();
    if (interactable != null)
    {
      int instanceId = collision.gameObject.GetInstanceID();
      if (!interactablesInRange.ContainsKey(instanceId))
      {
        interactablesInRange.Add(instanceId, interactable);
      }
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    int instanceId = collision.gameObject.GetInstanceID();
    if (interactablesInRange.ContainsKey(instanceId))
    {
      interactablesInRange.Remove(instanceId);
    }
  }
}
