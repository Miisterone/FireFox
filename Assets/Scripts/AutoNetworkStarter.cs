using Fusion;
using UnityEngine;

public class AutoNetworkStarter : MonoBehaviour
{
    private NetworkRunner runner;
    
    void Awake()
    {
        runner = GetComponent<NetworkRunner>();
        runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = SystemInfo.deviceName,
            Scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex,
            PlayerCount = 2
        });
    }
}
