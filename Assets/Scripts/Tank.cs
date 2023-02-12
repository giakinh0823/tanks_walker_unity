using UnityEngine;

public class Tank : MonoBehaviour
{
    // The bullet prefab
    [SerializeField]
    public GameObject bulletPrefab;
    [SerializeField]
    public Transform gunHead;

    // The delay time between generating bullets
    public float shootingDelay = 1f;
    public float rotationSpeed = 5f;

    // The time when the last bullet was generated
    private float lastShootTime;


    public AudioClip shootingSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time <= lastShootTime + shootingDelay)
        {
            return;
        }
            // Get an array of all game objects with the "Player" tag
        GameObject[] walkers = GameObject.FindGameObjectsWithTag("Player");

        Vector2 closestWalkerDirection = Vector2.zero;
        float closestDistance = Mathf.Infinity;
        GameObject walkerDistance = null;

        foreach (GameObject walker in walkers)
        {
            float distance = Vector2.Distance(gunHead.position, walker.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWalkerDirection = (walker.transform.position - transform.position).normalized;
                walkerDistance = walker;
            }
        }

        if(walkerDistance != null)
        {
            RotatoTank(closestWalkerDirection);
            if (closestDistance <= 5f)
            {
                Shoot(walkerDistance);
                // Update the last shoot time
                lastShootTime = Time.time;
            }
        }
    }

    private void RotatoTank(Vector2 closestTargetDirection)
    {
        float angle = Mathf.Atan2(closestTargetDirection.y, closestTargetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void Shoot(GameObject _target)
    {
        // Call the Seek method on the bullet script component with the walker's transform as the argument
        GameObject bullet = Instantiate(bulletPrefab, gunHead.position, gunHead.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            audioSource.PlayOneShot(shootingSound);
            bulletScript.Seek(_target);
        }
    }
}
