
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Classe permettant de faire une interraction de bouton avec OpenXR
/// </summary>
/// Forte inspiration de Valem Tutorials
/// Source: https://youtu.be/bts8VkDP_vU?si=lOnRKkudLDKEN2r
/// J'ai ajouter la possibilité d'arrêter le bouton à un certain point 
/// et lui permettre à ce moment d'effectuer une action.
public class ButtonFollowVisual : MonoBehaviour
{
    [SerializeField]
    private Transform visualTarget;
    [SerializeField]
    private Vector3 localAxis;
    [SerializeField]
    private float resetSpeed = 5;
    [SerializeField]
    private float followAngleThreshold = 45;
    [SerializeField]
    private Material activatedMaterial;
    [SerializeField]    
    private MeshRenderer visualMeshRenderer;
    [SerializeField, Tooltip("La réponse que donne le bouton à un controlleur d'énigme.")]
    private string reponseBouton = "1";
    [SerializeField,Tooltip("Détermine s'il fait partie d'une énigme à plusieur boutons.")]
    private bool multiBoutonEnigme = false;

    [SerializeField, Tooltip("Event se déclenchant lorsque le bouton est actionné.")]
    private UnityEvent actionBoutonSansReponse;
    [SerializeField, Tooltip("Event se déclenchant lorsque le bouton est actionné.")]
    private UnityEvent<string> actionBoutonAvecReponse;

    private bool freeze = false;

    private bool buttonActivated = false;
    private bool actionDone = false;

    private Vector3 initialLocalPos;

    private Vector3 offset;
    private Transform pokeAttachTransform;

    private XRBaseInteractable interactable;
    private bool isFollowing = false;

    // Start is called before the first frame update
    void Start() { 

        initialLocalPos = visualTarget.localPosition;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(ResetHover);
        interactable.selectEntered.AddListener(Freeze);
    }

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

    public void ResetHover(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            // Vérifie que la position du bouton est enfoncer au maximum
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
    /// Permet de reset complètement le bouton si besoin après pression complète.
    /// </summary>
    public void Reset()
    {
        isFollowing = false;
        freeze = false;
        buttonActivated = false;
        actionDone = false;
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            freeze = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze)
        {
            if (buttonActivated)
            {
                visualMeshRenderer.material = activatedMaterial;
                if (!actionDone)
                {
                    // Faire quleque chose
                    if (multiBoutonEnigme)
                    {
                        actionBoutonAvecReponse?.Invoke(reponseBouton);

                    }else
                    {
                        actionBoutonSansReponse?.Invoke();
                    }
                    
                    Debug.Log("Bouton activé");
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
            
            // Bloque le bouton à cette position vu que c'est celle maximum pour activer celui-ci
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
