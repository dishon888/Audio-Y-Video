using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public AudioSource audioSource; // 🎵 arrastra aquí tu componente con la canción

    public void Jugar()
    {
        StartCoroutine(ReproducirYEntrar());
    }

    IEnumerator ReproducirYEntrar()
    {
        audioSource.Play();              // Reproduce la canción
        yield return new WaitForSeconds(2f); // Espera 2 segundos (ajusta el tiempo)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
