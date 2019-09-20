using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableManager", menuName = "Scriptable Objects/Managers/Interactable Manager")]
public class InteractableManager : ScriptableObject
{
    [SerializeField] private List<Interactable> interactables = new List<Interactable>();

    public void Initialize()
    {
        interactables.Clear();
    }

    private void SetInteraction()
    {
        interactables[0].ActivateInteraction();
    }

    public void GetOtherInteraction()
    {
        Interactable aux = interactables[0];
        interactables.Remove(interactables[0]);
        aux.DesactivateInteraction();
        RegisterInteractable(aux);
        SetInteraction();
    }

    public void RegisterInteractable(Interactable interaction)
    {
        interactables.Add(interaction);

        if (interactables.Count == 1)
            SetInteraction();
    }

    public void UnregisterInteractable(Interactable interaction)
    {
        if(interactables[0] == interaction && interactables.Count != 1)
        {
            GetOtherInteraction();
        }

        interaction.DesactivateInteraction();
        interactables.Remove(interaction);
    }
}
