using System;
using System.Collections.Generic;
using UnityEngine;

public class PotionsCrafter : MonoBehaviour
{
  private ErbPicker erbPicker;

  private Dictionary<Type, Recepy> recepies = new();

  private void Start()
  {
    recepies.Add(typeof(BasilRecepy), new BasilRecepy());

    if (TryGetComponent(out erbPicker))
    {
      erbPicker.OnBagErbsChanged.AddListener(HandleBagErbsChanged);
    }
  }

  public GameObject BrewPotion<T>() where T : Recepy, new()
  {
    if (!recepies.ContainsKey(typeof(T))) return null;
    if (!recepies[typeof(T)].isCraftable) return null;

    var recepy = new T();

    foreach ((var erbKind, var amount) in recepy.erbsRequired)
    {
      erbPicker.RemoveFromBag(erbKind, amount.amount);
    }

    Debug.Log($"Brewed potion {recepy.potion.name}");
    return recepy.potion;
  }

  private void HandleBagErbsChanged(ErbEvent erbEvent)
  {
    var erbsLeft = erbPicker.bag[erbEvent.erbKind];

    foreach ((var recepyType, var recepy) in recepies)
    {
      if (!recepy.erbsRequired.ContainsKey(erbEvent.erbKind)) continue;  

      if (erbsLeft >= recepy.erbsRequired[erbEvent.erbKind].amount)
      {
        recepy.erbsRequired[erbEvent.erbKind].check = true;
      }
      else 
      {
        recepy.erbsRequired[erbEvent.erbKind].check = false;
      }

      Debug.Log($"Updated recepy {recepyType.Name}: " +
        $"{erbEvent.erbKind} required = {recepy.erbsRequired[erbEvent.erbKind].amount}. " +
        $"Amount = {erbsLeft}. " +
        $"Checked = {recepy.erbsRequired[erbEvent.erbKind].check}");
    }
  }
}
