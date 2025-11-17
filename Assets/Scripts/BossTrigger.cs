using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject bossHealthBar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            bossHealthBar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            bossHealthBar.SetActive(false);
        }
    }
}
