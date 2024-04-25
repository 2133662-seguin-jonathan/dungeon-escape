
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Classe permettant de faire une interraction de bouton avec OpenXR
/// </summary>
/// Forte inspiration de Valem Tutorials
/// Source: https://youtu.be/bts8VkDP_vU?si=lOnRKkudLDKEN2r
/// J'ai ajouter la possibilit� d'arr�ter le bouton � un certain point 
/// et lui permettre � ce moment d'effectuer une action.
public class ButtonFollowVisual : MonoBehaviour
{
    [SerializeField,Tooltip("Le visuel du bouton qui interragi.")]
    private Transform visualTarget;
    [SerializeField,Tooltip("L'axe local du bouton.")]
    private Vector3 localAxis;
    [SerializeField,Tooltip("Le temps que prends le bouton pour se reset.")]
    private float resetSpeed = 5;
    [SerializeField,Tooltip("L'angle de suivie du bouton.")]
    private float followAngleThreshold = 45;
    [SerializeField,Tooltip("Le mat�riel � afficher sur le bouton lorsqu'il est apput� compl�tement.")]
    private Material activatedMaterial;
    [SerializeField,Tooltip("Le mesh de rendu du bouton.")]    
    private MeshRenderer visualMeshRenderer;
    [SerializeField, Tooltip("La r�ponse que donne le bouton � un controlleur d'�nigme.")]
    private string reponseBouton = "1";
    [SerializeField,Tooltip("D�termine s'il fait partie d'une �nigme � plusieur boutons.")]
    private bool multiBoutonEnigme = false;

    [SerializeField, Tooltip("�v�nement se d�clenchant lorsque le bouton est actionn�.")]
    private UnityEvent actionBoutonSansReponse;

    [SerializeField, Tooltip("�v�nement se d�clenchant lorsque le bouton est actionn�.")]
    private UnityEvent<string> actionBoutonAvecReponse;

    /// <summary>
    /// Permet de d�terminer si le bouton est geler sur place.
    /// </summary>
    private bool freeze = false;

    /// <summary>
    /// Indique si le bouton est activ�.
    /// </summary>
    private bool buttonActivated = false;
    /// <summary>
    /// Indique si l'action est ex�cut�.
    /// </summary>
    private bool actionDone = false;

    /// <summary>
    /// La postion locale et initiale du bouton.
    /// </summary>
    private Vector3 initialLocalPos;

    /// <summary>
    /// Le offset du bouton.
    /// </summary>
    private Vector3 offset;

    /// <summary>
    /// L'attache du transform pour le suivi du bouton.
    /// </summary>
    private Transform pokeAttachTransform;

    /// <summary>
    /// Le gestionnaire d'interraction.
    /// </summary>
    private XRBaseInteractable interactable;

    /// <summary>
    /// D�termine si le bouton suit le mouvement de la main du joueuer ou non.
    /// </summary>
    private bool isFollowing = false;

    
    void Start() { 

        initialLocalPos = visualTarget.localPosition;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(ResetHover);
        interactable.selectEntered.AddListener(Freeze);
    }

    /// <summary>
    /// D�termine comment le bouton suit le mouvement de la main du joueur.
    /// </summary>
    /// <param name="hover">L'argument d'interraction pour le suivi du mouvement du joueuer.</param>
    public void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;

            isFollowing = true;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if (pokeAngle > followAngleThreshold)
            {
                isFollowing = false;
                freeze = true;
            }
        }
    }

    /// <summary>
    /// Permet de reset le bouton selon s'il ets appyu� compl�tement ou non.
    /// </summary>
    /// <param name="hover">>L'argument d'interraction du joueur</param>
    public void ResetHover(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            // V�rifie que la position du bouton est enfonc� au maximum
            if (visualTarget.localPosition.y <= -0.092)
            {
                freeze = true;
                isFollowing = false;
                buttonActivated = true;
            } else
            {
                isFollowing = false;
                freeze = false;
            }
        }
    }

    /// <summary>
    /// Permet de reset compl�tement le bouton si besoin apr�s pression compl�te.
    /// </summary>
    public void Reset()
    {
        visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
        isFollowing = false;
        freeze = false;
        buttonActivated = false;
        actionDone = false;
    }


    /// <summary>
    /// Permet de stopper le visuel du bouton.
    /// </summary>
    /// <param name="hover">L'argument d'interraction pour le suivi du mouvement du joueuer.</param>
    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            freeze = true;
        }
    }

    void Update()
    {
        if (freeze)
        {
            if (buttonActivated)
            {
                visualMeshRenderer.material = activatedMaterial;
                if (!actionDone)
                {
                    //Valide s`'il fait partie d'une �nigme � multi boutons.
                    if (multiBoutonEnigme)
                    {
                        actionBoutonAvecReponse?.Invoke(reponseBouton);

                    }else
                    {
                        actionBoutonSansReponse?.Invoke();
                    }
                    
                    Debug.Log("Bouton activ�");
                    actionDone = true;
                }
            }

            return;
        }
            
        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
            
            // Bloque le bouton � cette position vu que c'est celle maximum pour activer celui-ci
            if (visualTarget.localPosition.y <= -0.092)
            {
                freeze = true;
            }
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
        }
    }
}
