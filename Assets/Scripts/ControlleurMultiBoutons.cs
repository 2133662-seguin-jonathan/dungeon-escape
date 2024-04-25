using UnityEngine;
using UnityEngine.Events;

public class ControlleurMultiBoutons : MonoBehaviour
{
    [SerializeField, Tooltip("La réposne de l'énigme.")]
    private string[] reponseEnigme;

    /// <summary>
    /// La réponse du joueur face à l'énigme.
    /// </summary>
    private string[] reponseJoueur;

    /// <summary>
    /// Où est rendu le joueur à l'énigme.
    /// </summary>
    private int compteur = 0;

    [SerializeField,Tooltip("Évènemwent qui permet d'indiquer que l'énigme ets réussite.")]
    private UnityEvent enigmeReussi;

    [SerializeField, Tooltip("Évènemwent qui permet d'indiquer que l'énigme ets ratée.")]
    private UnityEvent enigmeRater;
    

    void Start()
    {
        reponseJoueur = new string[reponseEnigme.Length];

        for (int i = 0; i < reponseJoueur.Length; i++)
        {
            reponseJoueur[i] = "";
        }
    }

    /// <summary>
    /// Permet de recevoir l'info d'un bouton de l'énigme et l'ajoute à la réponse du joueur.
    /// </summary>
    /// <param name="reponseBouton">La réponse que donne le bouton actionner par le joueur.</param>
    public void RecevoirInfoBouton(string reponseBouton)
    {

        reponseJoueur[compteur] = reponseBouton;

        //Valide si le joueuer à compléter sa réponse.
        if (compteur == reponseJoueur.Length - 1) {
            //Valide si l'énigme est réussite ou non.
            bool valider = true;
            for (int i = 0; i < reponseJoueur.Length; i++)
            {
                if (reponseJoueur[i] != reponseEnigme[i])
                {
                    valider = false;
                }
            }
            if (valider)
            {
                enigmeReussi?.Invoke();
                Debug.Log("Reussite Énigme");
            } 
            else
            {
                // Remets l'énigme ets les boutons à zéro
                compteur = 0;
                for (int i = 0; i < reponseJoueur.Length; i++)
                {
                    reponseJoueur[i] = "";
                }
                enigmeRater?.Invoke();
                Debug.Log("Rater Énigme");
            }
        } else
        {
            reponseJoueur[compteur] = reponseBouton;
            compteur++;
        }
        
    }
}
