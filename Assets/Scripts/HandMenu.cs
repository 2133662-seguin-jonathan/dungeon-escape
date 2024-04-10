using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Permet de controller le menu de la main.
/// </summary>
/// Fortement inspiré de FIst Full of Shrimp
/// Source: https://www.youtube.com/watch?v=YISa0PvQTGk
public class HandMenu : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    [SerializeField]
    private AudioMixer controlleurAudio;

    [SerializeField]
    private Slider sliderMaitre;
    [SerializeField]
    private Slider sliderMusique;
    [SerializeField]
    private Slider sliderSFX;

    [SerializeField,Tooltip("Valeur seulement entre 0.001 et 1")]
    private float volumeMusiqueDefaut = 0.103f;

    [SerializeField, Tooltip("Valeur seulement entre 0.001 et 1")]
    private float volumeMaitreDefaut = 1f;

    [SerializeField, Tooltip("Valeur seulement entre 0.001 et 1")]
    private float volumeSFXDefaut = 0.301f;


    private Canvas _handUICanvas;
    private InputAction _menu;


    // Start is called before the first frame update
    private void Start()
    {
        _handUICanvas = GetComponent<Canvas>();
        _menu = inputActions.FindActionMap("XRI LeftHand").FindAction("Menu");
        _menu.Enable();
        _menu.performed += ToggleMenu;

        sliderMaitre.value = volumeMaitreDefaut;
        sliderMusique.value = volumeMusiqueDefaut;
        sliderSFX.value = volumeSFXDefaut;

        SetMaitreAudio();
        SetMusiqueAudio();
        SetSFXAudio();
    }

    private void OnDestroy()
    {
        _menu.performed -= ToggleMenu;
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        _handUICanvas.enabled = !_handUICanvas.enabled;
    }

    public void SetMaitreAudio()
    {
        float audio = sliderMaitre.value;
        controlleurAudio.SetFloat("maitre", Mathf.Log10(audio)*20);
    }

    public void SetMusiqueAudio()
    {
        float audio = sliderMusique.value;
        controlleurAudio.SetFloat("musique", Mathf.Log10(audio) * 20);
    }

    public void SetSFXAudio()
    {
        float audio = sliderSFX.value;
        controlleurAudio.SetFloat("sfx", Mathf.Log10(audio) * 20);
    }

    public void RecommencerJeu()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
