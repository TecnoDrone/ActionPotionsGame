using System;
using UnityEngine;

public class Erb : MonoBehaviour
{
  public ErbKind erbKind;
  public float growthRate = 1f; // Rate at which growth occurs
  public float growth = 0f;    // Internal growth value (public)
  public bool grown { get; private set; } = false; // Boolean indicating if growth is complete

  // Event that gets triggered when Growth reaches 100
  public event Action OnGrown;

  private Vector3 initialPosition;
  private float spriteHeight;

  void Start()
  {
    // Store the initial position of the GameObject
    initialPosition = transform.position;
    tag = "Erb";

    // Calculate the sprite's height
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
    {
      spriteHeight = spriteRenderer.bounds.size.y;
    }
    else
    {
      Debug.LogWarning("No SpriteRenderer found. Sprite height cannot be calculated.");
      spriteHeight = 0f; // Default to zero if no SpriteRenderer is found
    }
  }

  void Update()
  {
    // Update Growth over time based on growthRate
    Grow(growthRate * Time.deltaTime);
    // Update the scale and position based on Growth
    UpdateScaleAndPosition();
  }

  public void Grow(float amount)
  {
    if (amount < 0f) return;

    // Increase Growth and clamp to the range [0, 100]
    growth = Mathf.Clamp(growth + amount, 0f, 100f);

    // Check if Growth has reached 100
    if (growth >= 100f)
    {
      // Set grown to true and trigger the OnGrown event
      if (!grown)
      {
        grown = true;
        OnGrown?.Invoke();
      }
    }
  }

  public void ResetGrowth()
  {
    // Reset Growth to 0 and update the state
    growth = 0f;
    grown = false;
    UpdateScaleAndPosition();
  }

  private void UpdateScaleAndPosition()
  {
    // Map Growth (0 to 100) to scale.y (0 to 1)
    float scaleY = growth / 100f;
    transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);

    // Adjust position.y to keep the bottom of the sprite aligned with the initial position
    float newY = initialPosition.y + (spriteHeight * (scaleY - 1)) / 2;
    transform.position = new Vector3(transform.position.x, newY, transform.position.z);
  }
}
