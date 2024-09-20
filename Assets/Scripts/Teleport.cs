using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Teleport : MonoBehaviour
{
  public GameObject Destination;

  public delegate void OnEntityEnter();
  public event OnEntityEnter entityEntered;

  private Interactable interactable;

  void Start()
  {
    interactable = GetComponent<Interactable>();
    interactable.interacted += HandleEntityEntered;
  }

  public void HandleEntityEntered(GameObject entity)
  {
    entityEntered?.Invoke();
    entity.transform.position = Destination.transform.position;
  }
}
