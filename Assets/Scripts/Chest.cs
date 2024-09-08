using UnityEngine;

public class Chest : MonoBehaviour
{
  public bool open = false;
  public Sprite chestOpenSprite;
  public Sprite chestClosedSprite;

  public delegate void ChestOpenedEvent();
  public event ChestOpenedEvent ChestOpened;

  private SpriteRenderer spriteRenderer;

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public void Open()
  {
    if (open) return;

    open = true;
    ChestOpened?.Invoke();
    spriteRenderer.sprite = chestOpenSprite;
  }
}
