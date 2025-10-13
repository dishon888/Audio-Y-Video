using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 3f;

    void Start(){ Destroy(gameObject, lifetime); }

    void Update(){ transform.Translate(Vector2.right * speed * Time.deltaTime); }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Enemy")){
            var boss = col.GetComponent<EnemyBoss>();
            if(boss != null) boss.TakeDamage(damage);
            Destroy(gameObject);
        } else if(col.CompareTag("Ground")){
            Destroy(gameObject);
        }
    }
}
