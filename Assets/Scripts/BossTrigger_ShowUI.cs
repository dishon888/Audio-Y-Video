using UnityEngine;

public class BossTrigger_ActivateUI : MonoBehaviour
{
    public GameObject bossHealthUI;   // Arrastra el panel padre del UI
    public string playerTag = "Player";

    private bool alreadyActivated = false; // Para que solo se active una vez

    private void Start()
    {
        if (bossHealthUI != null)
            bossHealthUI.SetActive(false); // Oculto al inicio
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyActivated) return;     // Ya activado, no volver a hacerlo

        if (other.CompareTag(playerTag))
        {
            bossHealthUI.SetActive(true); // Aparece y queda activo
            alreadyActivated = true;
        }
    }
}
