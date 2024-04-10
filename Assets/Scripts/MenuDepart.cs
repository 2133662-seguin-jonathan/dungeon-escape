using UnityEngine;

public class MenuDepart : MonoBehaviour
{

    private Canvas _menuDepartCanvas;


    // Start is called before the first frame update
    void Start()
    {
        _menuDepartCanvas = GetComponent<Canvas>();
    }

    public void CacherMenuDepart()
    {
        _menuDepartCanvas.enabled = false;
    }
}
