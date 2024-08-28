using UnityEngine;
using System;

//for now, no way to remove plant
public class Terrain : MonoBehaviour
{
  public void SpawnErb(ErbKind erbKind)
  {
    var erbPrefabName = erbKind switch
    {
      ErbKind.Basil => ErbKind.Basil.ToString(),
      _ => throw new NotImplementedException()
    };

    var erb = Resources.Load($"Erbs/{erbPrefabName}");
    Instantiate(erb, transform.position, default);
  }
}
