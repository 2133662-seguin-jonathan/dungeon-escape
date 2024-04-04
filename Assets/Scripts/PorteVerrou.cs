using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de faire verrouiller et déverrouiler la porte
/// </summary>
public class PorteVerrou : MonoBehaviour
{
    private Animator animateur;

    public void Start()
    {
        animateur = GetComponent<Animator>();
    }


    public void OuvrirPorte()
    {
        animateur.SetBool("EstOuvert", true);
        Debug.Log("Test porte ouverte");
    }
}
