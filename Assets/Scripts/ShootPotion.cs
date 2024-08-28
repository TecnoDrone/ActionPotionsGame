using UnityEngine;

public class ShootPotion : MonoBehaviour
{
  public Vector3 shootPoint = Vector3.zero; // The point relative to the transform.position from where the projectile is shot
  public float projectileSpeed = 10f; // Speed of the projectile
  public float shootInterval = 1f;    // Time in seconds between each shot

  private GameObject spawnedProjectile;
  private Rigidbody2D spawnedRb;
  private PotionsCrafter potionsCrafter;
  private float lastShootTime;

  void Start()
  {
    potionsCrafter = GetComponent<PotionsCrafter>();
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0) && Time.time >= lastShootTime + shootInterval)
    {
      var potion = potionsCrafter.BrewPotion<BasilRecepy>();
      Spawn(potion);
    }

    if (Input.GetMouseButtonUp(0) && spawnedProjectile != null)
    {
      Shoot();
    }
  }

  void Spawn(GameObject potion)
  {
    if (potion == null) return;
    Vector3 shootPosition = transform.position + shootPoint;
    spawnedProjectile = Instantiate(potion, shootPosition, Quaternion.identity);
    spawnedProjectile.transform.SetParent(transform); // Set the projectile as a child of this object
    spawnedRb = spawnedProjectile.GetComponent<Rigidbody2D>();
    spawnedRb.bodyType = RigidbodyType2D.Kinematic;
    spawnedRb.linearVelocity = Vector2.zero;
  }

  void Shoot()
  {
    Vector3 targetPosition = GetMouseWorldPosition();
    Vector2 velocity = CalculateLaunchVelocity(spawnedProjectile.transform.position, targetPosition);
    spawnedRb.linearVelocity = velocity;
    spawnedProjectile.transform.SetParent(null); // Detach the projectile from this object
    spawnedRb.bodyType = RigidbodyType2D.Dynamic;
    lastShootTime = Time.time;
    spawnedProjectile = null;
  }

  Vector3 GetMouseWorldPosition()
  {
    Vector3 mouseScreenPosition = Input.mousePosition;
    mouseScreenPosition.z = Mathf.Abs(Camera.main.transform.position.z);
    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    return mouseWorldPosition;
  }

  Vector2 CalculateLaunchVelocity(Vector3 start, Vector3 targetPosition)
  {
    Vector3 direction = targetPosition - start;
    float distance = direction.magnitude;
    direction.Normalize();

    float gravityScale = spawnedRb.gravityScale; // Get the gravity scale of the projectile
    float gravity = Mathf.Abs(Physics2D.gravity.y) * gravityScale; // Calculate effective gravity

    float timeToReachTarget = distance / projectileSpeed;
    float verticalVelocity = (targetPosition.y - start.y) / timeToReachTarget + 0.5f * gravity * timeToReachTarget;
    Vector2 velocity = new Vector2(direction.x * projectileSpeed, verticalVelocity);

    return velocity;
  }

  void OnDrawGizmos()
  {
    Vector3 shootPosition = transform.position + shootPoint;
    Gizmos.color = Color.red;
    Gizmos.DrawSphere(shootPosition, 0.1f);
  }
}
