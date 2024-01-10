using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellheimExitCameraScripts : NetworkBehaviour
{
    public GameObject exitPortal;
    NetworkManager netManager;
    public CinemachineCollider cc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerMovement>().isMe == true)
            {
                netManager = FindObjectOfType<NetworkManager>();
                cc.enabled = true;
                netManager.Teleport(exitPortal.transform.position);
            }
            var netplayer = other.gameObject.GetComponent<NetworkObject>();
            Runner.Despawn(netplayer);
        }
    }
}
