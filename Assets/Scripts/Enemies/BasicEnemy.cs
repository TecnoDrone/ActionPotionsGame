using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
  public int life;

  public void TakeDamage(int amount)
  {
    life = Mathf.Max(0, life - amount);

    if (life <= 0) Death();
  }

  public void Death()
  {
    Destroy(gameObject);
  }
}
