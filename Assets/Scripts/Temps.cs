using UnityEngine;
using TMPro;

/// <summary>
/// Timer permmetant de déterminer le temps qu'à mis le joueur à fin du jeu.
/// FOrtement inspiré de Rehope Games
/// Source: https://www.youtube.com/watch?v=POq1i8FyRyQ
/// </summary>
public class Temps : MonoBehaviour
{
    [SerializeField,Tooltip("Le text mesh pro qui affiche le chronomètre.")]
    private TextMeshProUGUI timerText;

    /// <summary>
    /// Le temps écoulé depuis le début du jeu jauqu'à la fin de la partie.
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// Booléen permettant de contrôler le la pause du chronomètre.
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
    /// Permets de faire commencer le chronomètre.
    /// </summary>
    public void StartTimer()
    {
        timerStop = false;
    }

    /// <summary>
    /// Arrête le chronomètre lorsque le joueur atteint la fin du jeu.
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
