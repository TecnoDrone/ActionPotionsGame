using UnityEngine;

[RequireComponent (typeof(Interactable))]
public class Chest : MonoBehaviour
{
  public bool open = false;
  public Sprite chestOpenSprite;
  public Sprite chestClosedSprite;

  public delegate void ChestOpenedEvent();
  public event ChestOpenedEvent ChestOpened;

  private SpriteRenderer spriteRenderer;
  private Interactable interactable;

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    interactable = GetComponent<Interactable>();
    interactable.interacted += Open;
  }
  public void Open()
  {
    if (open) return;

    open = true;
    ChestOpened?.Invoke();
    spriteRenderer.sprite = chestOpenSprite;
  }
}
