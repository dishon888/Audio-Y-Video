using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public int maxHealth = 10;
    int currentHealth;

    [Header("Ataques")]
    public GameObject projectilePrefab;
    public Transform[] firePoints;
    public float fireInterval = 2f;
    float nextFire;

    [Header("UI")]
    public HealthBar healthBar;

    Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null) healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time > nextFire && projectilePrefab != null)
        {
            foreach (var p in firePoints)
            {
                if (p != null)
                {
                    Instantiate(projectilePrefab, p.position, Quaternion.identity);
                }
            }
            nextFire = Time.time + fireInterval;
        }
    }

    public void TakeDamage(int d)
    {
        if (currentHealth <= 0) return; // evita da�o extra si ya muri�

        currentHealth -= d;
        if (healthBar != null) healthBar.SetHealth(currentHealth);
        if (animator != null) animator.SetTrigger("Hurt");

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (animator != null) animator.SetTrigger("Die");
        Destroy(gameObject, 1.2f); // tiempo para reproducir animaci�n
    }
}
