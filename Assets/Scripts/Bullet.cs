using UnityEngine;

public class Bullet : MonoBehaviour
{
    // The speed at which the bullet moves towards the target
    public float speed = 0.2f;
    public float damage = 1f;

    // The target transform that the bullet will seek
    private GameObject target;

    private void Update()
    {
        // Check if the target is not null
        if (target != null)
        {
            // Calculate the distance between the bullet and the target
            float distance = Vector2.Distance(transform.position, target.transform.position);

            // Check if the distance is less than 5 units
            if (distance < 5f)
            {
                // Increase the speed if the distance is less than 5 units
                speed = 10f;
            }

            // Move the bullet towards the target based on the speed
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            // Check if the distance is less than 0.5 units
            if (distance < 0.5f)
            {
                // Destroy the bullet game object
                Destroy(gameObject);
                Walker walker = target.GetComponent<Walker>();
                if (walker != null)
                {
                    walker.TakeDamage(damage);
                }
            }
        }
        else
        {
            // If the target is null, destroy the bullet game object
            Destroy(gameObject);
        }
    }

    // Method to set the target transform for the bullet to seek
    public void Seek(GameObject _target)
    {
        target = _target;
    }
}
