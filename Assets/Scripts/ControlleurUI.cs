using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Permet d'afficher et controller les boutons pour les UIs
/// </summary>
/// /// Fortement inspiré de FIst Full of Shrimp
/// Source: https://www.youtube.com/watch?v=YISa0PvQTGk
public class ControlleurUI : MonoBehaviour
{
    [SerializeField,Tooltip("Le gestionnaire d'interraction des manettes.")]
    private InputActionAsset inputActions;

    [SerializeField,Tooltip("Le canvas de l'interface sur la main gauche du joueur.")]
    private Canvas handUICanvas;

    /// <summary>
    /// La touche permettant d'interragire avec le menu de la main gauche.
    /// </summary>
    private InputAction _menu;

    /// <summary>
    /// La touche permettant d'interragir avec les mouvements.
    /// </summary>
    private InputAction _mouvement;
    

    void Start()
    {
        _menu = inputActions.FindActionMap("XRI LeftHand").FindAction("Menu");
        _menu.Enable();
        _menu.performed += ToggleMenu;

        _mouvement = inputActions.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");

        // Désactive les mouvements une seule fois lors du lancement de partie.
        _mouvement.Disable();

        if (!_mouvement.enabled)
            StartCoroutine(DesactiverMouvement());

    }

    /// <summary>
    /// Permet de désactiver les mouvements.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DesactiverMouvement()
    {
        yield return new WaitForSeconds(1f);
        _mouvement.Disable();
    }

    /// <summary>
    /// Détruit les trucs qui restent stocker dans la ram.
    /// </summary>
    private void OnDestroy()
    {
        _menu.performed -= ToggleMenu;
    }

    /// <summary>
    /// Permet d'afficher ou cacher le menu de la main gauche.
    /// </summary>
    /// <param name="context"></param>
    public void ToggleMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
            handUICanvas.enabled = !handUICanvas.enabled;
    }

    /// <summary>
    /// Permet d'activer les mouvement lorsque le jeu commence.
    /// </summary>
    public void CommencerJeu()
    {
        _mouvement.Enable();
    }
}
