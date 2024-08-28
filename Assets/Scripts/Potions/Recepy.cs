using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Recepy
{
  public GameObject potion;
  public Dictionary<ErbKind, ErbCheck> erbsRequired;
  public bool isCraftable => erbsRequired.All(e => e.Value.check);
}

public class ErbCheck
{
  public int amount;
  public bool check;

  public ErbCheck(int amount, bool check = false)
  {
    this.amount = amount;
    this.check = check;
  }
}