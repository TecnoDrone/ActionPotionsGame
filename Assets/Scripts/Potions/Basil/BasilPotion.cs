using UnityEngine;

public class BasilPotion : Potion
{
  public int damage;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.CompareTag("Enemy")) return;

    var enemy = collision.GetComponent<BasicEnemy>();
    enemy.TakeDamage(damage);

    Destroy(gameObject);
  }
}