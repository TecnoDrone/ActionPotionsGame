using UnityEngine;

public class ShootPotion : MonoBehaviour
{
  public GameObject projectilePrefab;
  public Vector3 shootPoint;
  public Vector3 _shootPoint => shootPoint + transform.position;
  public float projectileSpeed = 10f;
  public float shootInterval = 0.5f;

  private float nextShootTime = 0f;

  void Update()
  {
    if (Input.GetMouseButton(0) && Time.time >= nextShootTime)
    {
      Shoot();
      nextShootTime = Time.time + shootInterval;
    }
  }

  void Shoot()
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePos.z = 0f;

    Vector2 direction = (mousePos - _shootPoint).normalized;

    GameObject projectile = Instantiate(projectilePrefab, _shootPoint, Quaternion.identity);
    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
    rb.linearVelocity = direction * projectileSpeed;
  }

  void OnDrawGizmos()
  {
    if (_shootPoint != null)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(_shootPoint, 0.1f);
    }
  }
}
