using UnityEngine;

public class Tank : MonoBehaviour
{
    // The bullet prefab
    public GameObject bulletPrefab;

    // The delay time between generating bullets
    public float shootingDelay = 1f;

    // The time when the last bullet was generated
    private float lastShootTime;

    public float rotationSpeed = 5f;

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
            float distance = Vector2.Distance(transform.position, walker.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWalkerDirection = (walker.transform.position - transform.position).normalized;
                walkerDistance = walker;
            }
        }

        float angle = Mathf.Atan2(closestWalkerDirection.y, closestWalkerDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if (closestDistance <= 5f)
        {
            // Call the Seek method on the bullet script component with the walker's transform as the argument
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            if (bullet != null)
            {
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.Seek(walkerDistance);
                }
            }

            // Update the last shoot time
            lastShootTime = Time.time;
        }
    }
}
