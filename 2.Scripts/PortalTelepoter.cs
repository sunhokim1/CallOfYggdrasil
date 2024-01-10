using Fusion;
using UnityEngine;

public class PortalTeleporter : NetworkBehaviour
{
    public GameObject exitPortal;
    NetworkManager netManager;
    
    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<PlayerMovement>().isMe == true)
                {
                    netManager = FindObjectOfType<NetworkManager>();
                    netManager.Teleport(exitPortal.transform.position);
                }
                var netplayer = other.gameObject.GetComponent<NetworkObject>();
                Runner.Despawn(netplayer);
            }
      
    }
}