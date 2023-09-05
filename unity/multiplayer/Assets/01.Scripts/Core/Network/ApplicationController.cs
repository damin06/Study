using System.Threading.Tasks;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] private ClientSingletone _clientPrefab;
    [SerializeField] private HostSingletone _hostPrefab;


    private async void Start()
    {
        DontDestroyOnLoad(gameObject);

        await LaunchInMode(
            SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null);
    }

    private async Task LaunchInMode(bool isDedicatedServer)
    {
        if(isDedicatedServer)
        {

        }else
        {
            HostSingletone hostSingletone = Instantiate(_hostPrefab);
            hostSingletone.CreateHost(); //게임매니저 만들고 준비

            ClientSingletone clientSingletone = Instantiate(_clientPrefab);
            bool authenticated = await clientSingletone.CreateClient();

            if(authenticated)
            {
                ClientSingletone.Instance.GameManager.GotoMenu();
            }
        }
    }
}
