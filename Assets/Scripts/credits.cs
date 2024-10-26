using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    // Elementos a desactivar al activar los créditos
    public GameObject[] elementsToDisable;

    // Elementos de los créditos
    public GameObject[] creditsTexts; // Textos de los créditos
    public GameObject creditsButton; // Botón de los créditos

    // Método que se ejecuta al presionar el botón de créditos
    public void OnCreditsButtonPress()
    {
        // Desactiva los elementos actuales
        foreach (GameObject element in elementsToDisable)
        {
            element.SetActive(false);
        }

        // Activa los elementos de los créditos
        foreach (GameObject text in creditsTexts)
        {
            text.SetActive(true);
        }
        creditsButton.SetActive(true);
    }

    // Método para regresar al menú principal desde los créditos
    public void BackToMenuFromCredits()
    {
        // Reactiva los elementos que fueron desactivados
        foreach (GameObject element in elementsToDisable)
        {
            element.SetActive(true);
        }

        // Desactiva los elementos de los créditos
        foreach (GameObject text in creditsTexts)
        {
            text.SetActive(false);
        }
        creditsButton.SetActive(false);
    }
}
