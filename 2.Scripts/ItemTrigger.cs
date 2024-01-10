using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ItemTrigger : NetworkBehaviour
{
    [Range(0, 100)]
    public float rotateSpd = 120f;
    public Vector3 RotateDirection = new Vector3(0,1,0);

    private void Update()
    {
        transform.Rotate(RotateDirection * rotateSpd * Time.deltaTime , Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerMovement>()) return;
        if (other.GetComponent<PlayerMovement>().HasInputAuthority)
        {
            other.GetComponent<PlayerMovement>()._score += 100;

            RPC_DestroyItem();
        }
    }

    [Rpc]

    void RPC_DestroyItem()
    {
        Destroy(gameObject);
    }
}
