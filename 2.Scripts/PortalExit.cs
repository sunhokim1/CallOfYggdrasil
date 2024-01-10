using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalExit : NetworkBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerMovement>().isMe == true)
            {
                other.GetComponent<CharacterController>().enabled = true;
            }
            var netplayer = other.gameObject.GetComponent<NetworkObject>();
            Runner.Despawn(netplayer);
        }
          
    }
}
