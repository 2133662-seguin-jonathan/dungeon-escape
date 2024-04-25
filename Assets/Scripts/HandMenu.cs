using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
/// <summary>
/// Permet de controller le menu de la main.
/// </summary>
/// Fortement inspiré de Fist Full of Shrimp
/// Source: https://www.youtube.com/watch?v=YISa0PvQTGk
/// 
public class HandMenu : MonoBehaviour
{

    [SerializeField,Tooltip("Le controlleur de son du jeu.")]
    private AudioMixer controlleurAudio;

    [SerializeField,Tooltip("Le slider permmettant de cahnger le volume principal.")]
    private Slider sliderMaitre;
    [SerializeField, Tooltip("Le slider permmettant de cahnger le volume de la musique.")]
    private Slider sliderMusique;
    [SerializeField, Tooltip("Le slider permmettant de cahnger le volume des SFX.")]
    private Slider sliderSFX;

    [SerializeField,Tooltip("Valeur seulement entre 0.001 et 1")]
    private float volumeMusiqueDefaut = 0.103f;

    [SerializeField, Tooltip("Valeur seulement entre 0.001 et 1")]
    private float volumeMaitreDefaut = 1f;

    [SerializeField, Tooltip("Valeur seulement entre 0.001 et 1")]
    private float volumeSFXDefaut = 0.301f;

    
    private void Start()
    {
        sliderMaitre.value = volumeMaitreDefaut;
        sliderMusique.value = volumeMusiqueDefaut;
        sliderSFX.value = volumeSFXDefaut;

        SetMaitreAudio();
        SetMusiqueAudio();
        SetSFXAudio();
    }


    /// <summary>
    /// Actualise le volume priincipal.
    /// </summary>
    public void SetMaitreAudio()
    {
        float audio = sliderMaitre.value;
        controlleurAudio.SetFloat("maitre", Mathf.Log10(audio)*20);
    }

    /// <summary>
    /// Actualise le volume de la musique.
    /// </summary>
    public void SetMusiqueAudio()
    {
        float audio = sliderMusique.value;
        controlleurAudio.SetFloat("musique", Mathf.Log10(audio) * 20);
    }

    /// <summary>
    /// Actualise le volume des SFX.
    /// </summary>
    public void SetSFXAudio()
    {
        float audio = sliderSFX.value;
        controlleurAudio.SetFloat("sfx", Mathf.Log10(audio) * 20);
    }

    /// <summary>
    /// Permet de recommencer le jeu de zéro.
    /// </summary>
    public void RecommencerJeu()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    /// <summary>
    /// Permet de quitter et fermer le jeu.
    /// </summary>
    public void QuitterJeu()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
          Application.Quit();
        #endif
    }
}
