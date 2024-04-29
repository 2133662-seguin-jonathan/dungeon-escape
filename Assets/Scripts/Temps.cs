using UnityEngine;
using TMPro;

/// <summary>
/// Timer permmetant de d�terminer le temps qu'� mis le joueur � fin du jeu.
/// FOrtement inspir� de Rehope Games
/// Source: https://www.youtube.com/watch?v=POq1i8FyRyQ
/// </summary>
public class Temps : MonoBehaviour
{
    [SerializeField,Tooltip("Le text mesh pro qui affiche le chronom�tre.")]
    private TextMeshProUGUI timerText;

    /// <summary>
    /// Le temps �coul� depuis le d�but du jeu jauqu'� la fin de la partie.
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// Bool�en permettant de contr�ler le la pause du chronom�tre.
    /// </summary>
    private bool timerStop = true;

    // Update is called once per frame
    void Update()
    {
        if (!timerStop)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    /// <summary>
    /// Permets de faire commencer le chronom�tre.
    /// </summary>
    public void StartTimer()
    {
        timerStop = false;
    }

    /// <summary>
    /// Arr�te le chronom�tre lorsque le joueur atteint la fin du jeu.
    /// </summary>
    /// <param name="other">Le collider qui rentre dans la zone.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!timerStop)
            {
                timerStop = true;
            }
        }
    }
}
