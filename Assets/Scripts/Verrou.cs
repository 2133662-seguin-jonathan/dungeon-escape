using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.Events;

public class Verrou : MonoBehaviour
{
    [SerializeField, Tooltip("Si vrai c'est un verrou de porte, sinon un verrou de coffre.")]
    private bool verrouPorte = true;

    [SerializeField,Tooltip("Event se déclenchant lorsque la bonne clé atteint le verrou")]
    private UnityEvent ouvrirVerrou;

    private void OnTriggerEnter(Collider other)
    {
        // Validation de si le joueur rentre dans la zone 
        if (other.gameObject.tag == "Cle Porte" && verrouPorte)
        {
            Collider collision = GetComponent<Collider>();
            Destroy(other.gameObject);
            ouvrirVerrou?.Invoke();
            collision.enabled = false;

        } 
        else if (other.gameObject.tag == "Cle Coffre" && !verrouPorte)
        {
            Collider collision = GetComponent<Collider>();
            Destroy(other.gameObject);
            ouvrirVerrou?.Invoke();
            collision.enabled = false;

        }
    }
}
