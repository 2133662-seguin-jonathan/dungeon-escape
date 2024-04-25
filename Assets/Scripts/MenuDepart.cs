using UnityEngine;

/// <summary>
/// Classe pour le menu de départ.
/// </summary>
public class MenuDepart : MonoBehaviour
{
    /// <summary>
    /// Le canvas du menu de départ du jeu.
    /// </summary>
    private Canvas _menuDepartCanvas;


    void Start()
    {
        _menuDepartCanvas = GetComponent<Canvas>();
    }

    /// <summary>
    /// Permetd e cacher le menu de départ.
    /// </summary>
    public void CacherMenuDepart()
    {
        _menuDepartCanvas.enabled = false;
    }
}
