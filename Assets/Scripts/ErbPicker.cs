using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ErbPicker : MonoBehaviour
{
  // Dictionary to hold detected Erb objects
  private Dictionary<int, Erb> detectedErbs = new Dictionary<int, Erb>();

  // List to hold Erb objects in the bag for Inspector visualization
  public Dictionary<ErbKind, int> bag = new();

  // Maximum number of elements allowed in the bag
  public int bagLimit = 10;

  public UnityEvent<ErbEvent> OnBagErbsChanged;

  void Start()
  {
    OnBagErbsChanged ??= new UnityEvent<ErbEvent>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Erb"))
    {
      if (other.TryGetComponent(out Erb erb))
      {
        // Add the detected Erb to the dictionary
        detectedErbs[other.GetInstanceID()] = erb;
        //Debug.Log("Erb detected and added to list: " + other.name);
      }
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Erb"))
    {
      // Remove the Erb from the dictionary when it exits
      detectedErbs.Remove(other.GetInstanceID());
      //Debug.Log("Erb removed from list: " + other.name);
    }
  }

  private void Update()
  {
    // Check for button release
    if (Input.GetKeyUp(KeyCode.F))
    {
      MoveToBag();
    }
  }

  private void MoveToBag()
  {
    // Check how many slots are available in the bag
    int availableSlots = bagLimit - bag.Count;

    // Only move elements if there are available slots
    if (availableSlots > 0)
    {
      foreach ((int id, Erb erb) in detectedErbs)
      {
        if (availableSlots <= 0) break;

        // Check if the Erb's growth is 100 before moving
        if (erb.growth >= 100f)
        {
          // Add the element to the bagList with the new unique ID
          if (bag.ContainsKey(erb.erbKind))
          {
            bag[erb.erbKind]++;
          }
          else
          {
            bag.Add(erb.erbKind, 1);
          }

          //should optimize this having a dictionary for detected erbs as well
          OnBagErbsChanged.Invoke(new ErbPickedEvent(erb.erbKind, 1));

          // Reset the growth of the picked-up Erb
          erb.ResetGrowth();
          //Debug.Log("Erb moved to bag and growth reset: " + erb.name);
          availableSlots--;
        }
      }
    }
  }

  public void RemoveFromBag(ErbKind erbKind, int amount)
  {
    if (!bag.TryGetValue(erbKind, out int currentAmount)) return;
    if (bag[erbKind] == 0) return;

    bag[erbKind] = Mathf.Max(currentAmount - amount, 0);
    OnBagErbsChanged?.Invoke(new ErbUsedEvent(erbKind, amount));

    Debug.Log($"BAG: {erbKind} -> {bag[erbKind]}");
  }
}

public abstract class ErbEvent
{
  public ErbKind erbKind;
  public int amount;

  public ErbEvent(ErbKind erbKind, int amount)
  {
    this.erbKind = erbKind;
    this.amount = amount;
  }
}

public class ErbUsedEvent : ErbEvent
{
  public ErbUsedEvent(ErbKind erbKind, int amount) : base(erbKind, amount)
  {
  }
}

public class ErbPickedEvent : ErbEvent
{
  public ErbPickedEvent(ErbKind erbKind, int amount) : base(erbKind, amount)
  {
  }
}
