using UnityEngine;
using System.Collections;

public class BossPatternAI : MonoBehaviour {
    public enum State { Idle, Volley, HomingSalvo, Dash }
    public State currentState = State.Idle;

    public GameObject projectilePrefab;
    public GameObject homingPrefab;
    public Transform[] firePoints;
    public Transform dashTarget;
    public float dashSpeed = 8f;

    public int maxHealth = 30;
    int currentHealth;
    public HealthBar healthBar;
    Animator animator;

    float stateTimer = 0f;
    public float stateDuration = 4f;

    void Start(){
        currentHealth = maxHealth;
        if(healthBar != null) healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        StartCoroutine(StateLoop());
    }

    IEnumerator StateLoop(){
        while(currentHealth > 0){
            currentState = ChooseState();
            stateTimer = 0f;
            while(stateTimer < stateDuration){
                switch(currentState){
                    case State.Volley:
                        VolleyAttack();
                        break;
                    case State.HomingSalvo:
                        HomingSalvo();
                        break;
                    case State.Dash:
                        yield return DashAttack();
                        break;
                }
                stateTimer += 1f;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }

    State ChooseState(){
        float hpRatio = (float)currentHealth / maxHealth;
        if(hpRatio > 0.66f) return State.Volley;
        if(hpRatio > 0.33f) return State.HomingSalvo;
        return State.Dash;
    }

    void VolleyAttack(){
        foreach(var p in firePoints){
            Instantiate(projectilePrefab, p.position, Quaternion.identity);
        }
        if(animator != null) animator.SetTrigger("Attack");
    }

    void HomingSalvo(){
        foreach(var p in firePoints){
            Instantiate(homingPrefab, p.position, Quaternion.identity);
        }
        if(animator != null) animator.SetTrigger("Attack");
    }

    IEnumerator DashAttack(){
        if(dashTarget == null) yield break;
        if(animator != null) animator.SetTrigger("DashStart");
        float t = 0f;
        Vector3 start = transform.position;
        Vector3 targetPos = dashTarget.position;
        while(t < 0.6f){
            t += Time.deltaTime * dashSpeed;
            transform.position = Vector3.Lerp(start, targetPos, t);
            yield return null;
        }
        if(animator != null) animator.SetTrigger("DashEnd");
    }

    public void TakeDamage(int d){
        currentHealth -= d;
        if(healthBar != null) healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0) Die();
    }

    void Die(){
        if(animator != null) animator.SetTrigger("Die");
        Destroy(gameObject, 1.2f);
    }
}
