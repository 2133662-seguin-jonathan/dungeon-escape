using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de déverrouiler la porte
/// </summary>
public class PorteVerrou : MonoBehaviour
{
    /// <summary>
    /// L'animateur de la porte.
    /// </summary>
    private Animator animateur;

    /// <summary>
    /// LA source audio du sond e la porte qui s'ouvre.
    /// </summary>
    private new AudioSource audio;

    public void Start()
    {
        animateur = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Permet d'ouvrir la porte.
    /// </summary>
    public void OuvrirPorte()
    {
        audio.Play();
        StartCoroutine(JouerAnimation());
        Debug.Log("Test porte ouverte");
    }

    /// <summary>
    /// Permet de jouer l'animation d'ouverture avec un délai pour mieux synchroniser le son avec celle-ci.
    /// </summary>
    /// <returns></returns>
    private IEnumerator JouerAnimation()
    {
        yield return new WaitForSeconds(1.25f);
        animateur.SetBool("EstOuvert", true);
    }
}
