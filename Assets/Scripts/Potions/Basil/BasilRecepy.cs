using System.Collections.Generic;
using UnityEngine;

public class BasilRecepy : Recepy
{
  public BasilRecepy()
  {
    erbsRequired = new Dictionary<ErbKind, ErbCheck>
    {
      { ErbKind.Basil, new ErbCheck(3) }
    };

    potion = Resources.Load<GameObject>("Potions/BasilPotion");
  }
}