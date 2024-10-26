using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour
{

    public void PlayButton()
    {
       SceneManager.LoadScene("SampleScene");
    }

    public void MenuButton()
    {
       SceneManager.LoadScene("Menu");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Ending");
        }
    }
}