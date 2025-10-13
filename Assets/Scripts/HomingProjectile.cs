using UnityEngine;

public class HomingProjectile : MonoBehaviour {
    public float speed = 6f;
    public float rotateSpeed = 200f;
    public int damage = 1;
    public float lifetime = 5f;
    Transform target;

    void Start(){
        Destroy(gameObject, lifetime);
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if(playerObj != null) target = playerObj.transform;
    }

    void Update(){
        if(target == null){
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            return;
        }

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player")){
            // Aquí se puede invocar daño al jugador
            Destroy(gameObject);
        } else if(col.CompareTag("Ground")){
            Destroy(gameObject);
        }
    }
}
