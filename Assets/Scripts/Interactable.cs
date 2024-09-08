using UnityEngine;

public class Interactable : MonoBehaviour
{
  public delegate void Interacted();
  public event Interacted interacted;

  public void Interact()
  {
    interacted?.Invoke();
  }
}
