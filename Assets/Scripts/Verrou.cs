using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Classe d'un verrou de porte.
/// </summary>
public class Verrou : MonoBehaviour
{
    [SerializeField, Tooltip("Si vrai c'est un verrou de porte, sinon un verrou de coffre.")]
    private bool verrouPorte = true;

    [SerializeField,Tooltip("Évènement se déclenchant lorsque la bonne clé atteint le verrou")]
    private UnityEvent ouvrirVerrou;

    /// <summary>
    /// Permet d'ouvrir la porte lorsque la bonne clé rendre dans la zone prévu du verrou. 
    /// </summary>
    /// <param name="other">La collision d'un objet.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Validation de si la clé rentre dans la zone 
        if (other.gameObject.tag == "Cle Porte" && verrouPorte)
        {
            Collider collision = GetComponent<Collider>();
            Destroy(other.gameObject);
            ouvrirVerrou?.Invoke();
            collision.enabled = false;

        } 
    }
}
