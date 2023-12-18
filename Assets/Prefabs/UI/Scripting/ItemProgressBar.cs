using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemProgressBar : MonoBehaviour
{
    private Slider progressBar;
    private UIDocument inGameDoc;
    public IEnumerator FillProgressBarOverTime(float durationInSeconds)
    {
        float elapsedTime = 0f;

        while (elapsedTime < durationInSeconds)
        {
            elapsedTime += Time.deltaTime;

            // Calcula el progreso actual en base al tiempo transcurrido
            float progress = Mathf.Clamp01(elapsedTime / durationInSeconds);

            // Actualiza el valor de la barra de progreso
            progressBar.value = progress;
            //Debug.Log(elapsedTime);

            yield return null;
        }

        // Asegúrate de que la barra de progreso esté completamente llena al final
        progressBar.value = 1f;
    }

    // Ejemplo de uso
    private void Update()
    {
        inGameDoc = GetComponent<UIDocument>();
        progressBar = inGameDoc.rootVisualElement.Q("PowerUpBar") as Slider;
        if(progressBar != null )
        {
            StartCoroutine(FillProgressBarOverTime(10f)); // Llena la barra de progreso en 10 segundos (ajusta según tus necesidades)
        }
        else
        {
            Debug.Log("No va la barra");
        }
    }
}
