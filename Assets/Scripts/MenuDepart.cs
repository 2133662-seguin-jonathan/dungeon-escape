using UnityEngine;

/// <summary>
/// Classe pour le menu de d�part.
/// </summary>
public class MenuDepart : MonoBehaviour
{
    /// <summary>
    /// Le canvas du menu de d�part du jeu.
    /// </summary>
    private Canvas _menuDepartCanvas;


    void Start()
    {
        _menuDepartCanvas = GetComponent<Canvas>();
    }

    /// <summary>
    /// Permetd e cacher le menu de d�part.
    /// </summary>
    public void CacherMenuDepart()
    {
        _menuDepartCanvas.enabled = false;
    }
}
