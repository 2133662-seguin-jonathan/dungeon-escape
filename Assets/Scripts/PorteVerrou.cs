using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de faire verrouiller et déverrouiler la porte
/// </summary>
public class PorteVerrou : MonoBehaviour
{
    private Animator animateur;

    private new AudioSource audio;

    public void Start()
    {
        animateur = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }


    public void OuvrirPorte()
    {
        audio.Play();
        StartCoroutine(JouerAnimation());
        Debug.Log("Test porte ouverte");
    }

    private IEnumerator JouerAnimation()
    {
        yield return new WaitForSeconds(1.25f);
        animateur.SetBool("EstOuvert", true);
    }
}
