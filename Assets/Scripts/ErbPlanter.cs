using System.Collections.Generic;
using UnityEngine;

public class ErbPlanter : MonoBehaviour
{
  public Dictionary<ErbKind, int> seeds = new();
  private Terrain closestTerrain = null;

  public void Start()
  {
    seeds.Add(ErbKind.Basil, 1);
  }

  private void Update()
  {
    if (Input.GetKeyUp(KeyCode.F))
    {
      PlantSeed();
    }
  }

  private void PlantSeed()
  {
    if (closestTerrain == null || seeds.Count == 0) return;

    foreach (var seed in seeds)
    {
      if (seed.Value > 0)
      {
        seeds[seed.Key]--;
        closestTerrain.SpawnErb(seed.Key);
        Debug.Log($"Planted {seed.Key}. Remaining: {seeds[seed.Key]}");

        if (seeds[seed.Key] <= 0)
        {
          seeds.Remove(seed.Key);
        }
        break;
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    TryGetClosestTerrain(other);
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    TryGetClosestTerrain(other);
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Terrain") && other.TryGetComponent(out Terrain terrain))
    {
      if (terrain == closestTerrain)
      {
        closestTerrain = null; // Clear closest terrain if it leaves the trigger
      }
    }
  }

  private void TryGetClosestTerrain(Collider2D other)
  {
    if (other.CompareTag("Terrain") && other.TryGetComponent(out Terrain terrain))
    {
      if (closestTerrain == null || IsCloser(terrain))
      {
        closestTerrain = terrain;
      }
    }
  }

  private bool IsCloser(Terrain terrain)
  {
    return Vector3.Distance(transform.position, terrain.transform.position) <
           Vector3.Distance(transform.position, closestTerrain.transform.position);
  }
}
