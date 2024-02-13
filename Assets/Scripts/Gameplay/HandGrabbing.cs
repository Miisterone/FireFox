using UnityEngine;

public class HandGrabbing : MonoBehaviour
{
    private GameObject grabbedObject;
    private bool isGrabbing = false;

    void Update()
    {
        // Détecter la saisie (par exemple, en utilisant le pouce et l'index)
        if (!isGrabbing && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5f && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f)
        {
            // Vérifiez si la main est proche d'un objet saisissable
            // Si oui, définissez "grabbedObject" sur cet objet et définissez "isGrabbing" sur true
            Debug.Log("Grabbing " + grabbedObject.name);
        }

        // Détecter le lâcher
        if (isGrabbing && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) < 0.5f && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) < 0.5f)
        {
            // Lâchez l'objet
            grabbedObject = null;
            isGrabbing = false;
            Debug.Log("Releasing " + grabbedObject.name);
        }
    }
}
