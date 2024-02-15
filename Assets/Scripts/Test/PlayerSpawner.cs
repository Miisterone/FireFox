using Fusion;
using UnityEngine;
using System.Collections.Generic;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    public static PlayerSpawner Instance;
    public NetworkRunner runner;
    public GameObject SpecPrefab; // Utilisé pour les clients qui rejoignent
    public Dictionary<PlayerRef, NetworkObject> playerNetworkObjects = new Dictionary<PlayerRef, NetworkObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (runner == null || !runner.IsRunning)
        {
            runner = GetComponent<NetworkRunner>() ?? gameObject.AddComponent<NetworkRunner>();
            
            runner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = SystemInfo.deviceName,
                Scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex,
                PlayerCount = 2 // Ajustez selon le besoin
            });
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (Runner.IsServer)
        {
            // Vérifiez si le joueur qui rejoint n'est pas l'hôte
            if (player != Runner.LocalPlayer)
            {
                Debug.Log($"PlayerSpawner: Spawn SpectatorPlayer[{player.PlayerId}] join");
                NetworkObject spawnedNetworkObject = Runner.Spawn(SpecPrefab, Vector3.zero, Quaternion.identity, player);
        
                if (spawnedNetworkObject != null)
                {
                    DontDestroyOnLoad(spawnedNetworkObject.gameObject);
                    playerNetworkObjects[player] = spawnedNetworkObject;
                }
            }
        }
    }
    
    public void PlayerLeft(PlayerRef player)
    {
        Debug.Log("Player left: " + player.PlayerId);
        if (playerNetworkObjects.TryGetValue(player, out NetworkObject networkObject))
        {
            DestroyPlayerNetworkObject(networkObject);
            playerNetworkObjects.Remove(player);
        }
    }

    private void DestroyPlayerNetworkObject(NetworkObject networkObject)
    {
        if (networkObject != null && networkObject.HasStateAuthority)
        {
            networkObject.Runner.Despawn(networkObject);
        }
    }
}
