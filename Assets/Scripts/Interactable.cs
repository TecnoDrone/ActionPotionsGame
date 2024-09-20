using UnityEngine;

public class Interactable : MonoBehaviour
{
  public delegate void Interacted(GameObject entity);
  public event Interacted interacted;

  public void Interact(GameObject entity)
  {
    interacted?.Invoke(entity);
  }
}
