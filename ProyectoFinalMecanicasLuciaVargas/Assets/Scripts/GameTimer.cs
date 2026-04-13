using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("Tiempo")]
    public float timeRemaining = 120f; // segundos (ej: 2 minutos)
    public bool timerRunning = true;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                EndGame();
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        if (timerText != null)
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        Debug.Log("? Tiempo terminado");

        // Aquí puedes parar el juego
        // Time.timeScale = 0f;

        // Mostrar pantalla final, etc.
    }
}