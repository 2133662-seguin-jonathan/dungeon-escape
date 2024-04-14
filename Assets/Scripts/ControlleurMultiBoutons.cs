using UnityEngine;
using UnityEngine.Events;

public class ControlleurMultiBoutons : MonoBehaviour
{
    [SerializeField]
    private string[] reponseEnigme;

    private string[] reponseJoueur;

    private int compteur = 0;

    [SerializeField]
    private UnityEvent enigmeReussi;

    [SerializeField]
    private UnityEvent enigmeRater;
    // Start is called before the first frame update
    void Start()
    {
        reponseJoueur = new string[reponseEnigme.Length];

        for (int i = 0; i < reponseJoueur.Length; i++)
        {
            reponseJoueur[i] = "";
        }
    }

    public void RecevoirInfoBouton(string reponseBouton)
    {

        reponseJoueur[compteur] = reponseBouton;

        if (compteur == reponseJoueur.Length - 1) {
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
