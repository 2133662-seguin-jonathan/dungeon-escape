using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI.BodyUI;

/// <summary>
/// Classe permettant de faire une interraction de bouton avec OpenXR
/// </summary>
/// Forte inspiration de Valem Tutorials
/// Source: https://youtu.be/bts8VkDP_vU?si=lOnRKkudLDKEN2r
/// J'ai ajouter la possibilit� d'arr�ter le bouton � un certain point 
/// et lui permettre � ce moment d'effectuer une action.
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

    [SerializeField, Tooltip("Event se d�clenchant lorsque le bouton est actionn�.")]
    private UnityEvent ouvrirVerrou;

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
        interactable.hoverExited.AddListener(Reset);
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

    public void Reset(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            // V�rifie que la position du bouton est enfoncer au maximum
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
                    ouvrirVerrou?.Invoke();
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
