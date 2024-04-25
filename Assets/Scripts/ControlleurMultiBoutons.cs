using UnityEngine;
using UnityEngine.Events;

public class ControlleurMultiBoutons : MonoBehaviour
{
    [SerializeField, Tooltip("La r�posne de l'�nigme.")]
    private string[] reponseEnigme;

    /// <summary>
    /// La r�ponse du joueur face � l'�nigme.
    /// </summary>
    private string[] reponseJoueur;

    /// <summary>
    /// O� est rendu le joueur � l'�nigme.
    /// </summary>
    private int compteur = 0;

    [SerializeField,Tooltip("�v�nemwent qui permet d'indiquer que l'�nigme ets r�ussite.")]
    private UnityEvent enigmeReussi;

    [SerializeField, Tooltip("�v�nemwent qui permet d'indiquer que l'�nigme ets rat�e.")]
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
    /// Permet de recevoir l'info d'un bouton de l'�nigme et l'ajoute � la r�ponse du joueur.
    /// </summary>
    /// <param name="reponseBouton">La r�ponse que donne le bouton actionner par le joueur.</param>
    public void RecevoirInfoBouton(string reponseBouton)
    {

        reponseJoueur[compteur] = reponseBouton;

        //Valide si le joueuer � compl�ter sa r�ponse.
        if (compteur == reponseJoueur.Length - 1) {
            //Valide si l'�nigme est r�ussite ou non.
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
                Debug.Log("Reussite �nigme");
            } 
            else
            {
                // Remets l'�nigme ets les boutons � z�ro
                compteur = 0;
                for (int i = 0; i < reponseJoueur.Length; i++)
                {
                    reponseJoueur[i] = "";
                }
                enigmeRater?.Invoke();
                Debug.Log("Rater �nigme");
            }
        } else
        {
            reponseJoueur[compteur] = reponseBouton;
            compteur++;
        }
        
    }
}
