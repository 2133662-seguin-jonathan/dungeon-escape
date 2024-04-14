using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Permet d'afficher et controller les boutons pour les UIs
/// </summary>
/// /// Fortement inspiré de FIst Full of Shrimp
/// Source: https://www.youtube.com/watch?v=YISa0PvQTGk
public class ControlleurUI : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    [SerializeField]
    private Canvas handUICanvas;

    private InputAction _menu;

    private InputAction _mouvement;
    // Start is called before the first frame update
    void Start()
    {
        _menu = inputActions.FindActionMap("XRI LeftHand").FindAction("Menu");
        _menu.Enable();
        _menu.performed += ToggleMenu;

        _mouvement = inputActions.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");

        _mouvement.Disable();

        if (!_mouvement.enabled)
            StartCoroutine(DesactiverMouvement());

    }

    private IEnumerator DesactiverMouvement()
    {
        yield return new WaitForSeconds(1f);
        _mouvement.Disable();
    }

    private void OnDestroy()
    {
        _menu.performed -= ToggleMenu;
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        handUICanvas.enabled = !handUICanvas.enabled;
    }

    public void CommencerJeu()
    {
        _mouvement.Enable();
    }
}
