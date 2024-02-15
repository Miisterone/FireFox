using Fusion;
using UnityEngine;

public class SpecFollower : NetworkBehaviour
{
    public VRPlayerController vrPlayerController;
    private float checkInterval = 1.0f; // Intervalle de vérification en secondes
    private float timeSinceLastCheck = 0.0f;

    void Update()
    {
        timeSinceLastCheck += Time.deltaTime;
        // Vérifie si le vrPlayerController est null ou n'est pas prêt et si l'intervalle de vérification est atteint
        if ((vrPlayerController == null || !vrPlayerController.IsReady) && timeSinceLastCheck >= checkInterval)
        {
            // Réessayer de trouver le VRPlayerController
            GameObject vrPlayerObject = GameObject.FindWithTag("Player");
            if (vrPlayerObject != null) 
            {
                vrPlayerController = vrPlayerObject.GetComponent<VRPlayerController>();
                // Si vrPlayerController est toujours null ou n'est pas prêt, réinitialiser le compteur
                if(vrPlayerController == null || !vrPlayerController.IsReady)
                {
                    timeSinceLastCheck = 0.0f; 
                }
            }
            else
            {
                // Réinitialiser le compteur si le GameObject avec le tag "Player" n'a pas été trouvé
                timeSinceLastCheck = 0.0f;
            }
        }

        // Appliquer la position et la rotation synchronisées si vrPlayerController est prêt
        if (vrPlayerController != null && vrPlayerController.IsReady)
        {
            transform.position = vrPlayerController.Position;
            transform.rotation = vrPlayerController.Rotation;
        }
    }
}