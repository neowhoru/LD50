using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int damageCount = 20;
    public float speed = 0.2f;
    public bool moveRight = true;
    public Rigidbody2D rb;

    public enum BULLET_OWNER
    {
        PLAYER,
        ENEMY
    }

    public BULLET_OWNER owner = BULLET_OWNER.PLAYER;

    public GameObject explode;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 scale = transform.localScale;
        if (moveRight)
        {
            rb.velocity = transform.right * speed;
            transform.rotation = new Quaternion(0, 0, 45, 0);
        }
        else
        {
            rb.velocity = -transform.right * speed;
            transform.rotation = new Quaternion(0, 0, -45, 0);
        }

        transform.localScale = scale;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(" Bullet collision with " + other.collider.tag);
        if (other.transform.CompareTag("Ground") || other.transform.CompareTag("Bullet") ||
            other.transform.CompareTag("Enemy") || other.transform.CompareTag("Collectable") ||
            other.transform.CompareTag("Goal"))
        {
            if (owner.Equals(BULLET_OWNER.PLAYER) && !other.transform.CompareTag("Player"))
            {
                if (other.transform.tag.Equals("Enemy"))
                {
                    if (other != null)
                    {
                        Enemy theEnemy = other.transform.GetComponent<Enemy>();
                        theEnemy.TakeDamage(20);
                    }
                }

                // Call Enemy Hit Route
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(" Bullet collision with " + other.transform.tag);
        // Destroy self
        if (!other.transform.CompareTag("Bullet") && !other.transform.CompareTag("Untagged") && !other.transform.CompareTag("Collectable") &&
            !other.transform.CompareTag("Goal"))
        {
            if (owner.Equals(BULLET_OWNER.PLAYER) && !other.transform.CompareTag("Player"))
            {
                Debug.Log("Player is owner and player is not colliding");
                if (other.transform.tag.Equals("Enemy"))
                {
                    Debug.Log("Destroy Enemy");
                    if (other != null)
                    {
                        Enemy theEnemy = other.transform.GetComponent<Enemy>();
                        theEnemy.TakeDamage(20);
                    }
                }

                // Call Enemy Hit Route
                Destroy(gameObject);
            }
        }
    }
}