using UnityEngine;
using System;

[RequireComponent(typeof(Interactable))]
public class Terrain : MonoBehaviour
{
  private Interactable interactable;

  void Start()
  {
    interactable = GetComponent<Interactable>();
    interactable.interacted += SpawnErb;
  }

  public void SpawnErb(GameObject entity)
  {
    var erbKind = ErbKind.Basil;
    var erbPrefabName = erbKind switch
    {
      ErbKind.Basil => ErbKind.Basil.ToString(),
      _ => throw new NotImplementedException()
    };

    var erb = Resources.Load($"Erbs/{erbPrefabName}");
    Instantiate(erb, transform.position, default);
  }
}
