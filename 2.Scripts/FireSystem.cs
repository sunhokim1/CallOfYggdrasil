using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSystem : NetworkBehaviour 
{
    [SerializeField] NetworkPrefabRef bulletPrefab;
    [SerializeField] Transform firePos;
    bool isFiring;
    float timer = 0;
    
    void Fire()
    {
        if (!HasInputAuthority) return; //네트워크 오브젝트에 대한 권한이 없으면 이동안되게
        if (GetInput(out NetworkInputData data)) 
        {
            isFiring = data.fire;
            if (isFiring && timer <= 0)
            {
                Runner.Spawn(bulletPrefab,firePos.position,firePos.rotation, Runner.LocalPlayer);
                timer = 0.3f;
            }

        }
    }

    public override void FixedUpdateNetwork()
    {
        Fire();
        if(timer > 0) 
        { 
            timer -= Runner.DeltaTime; 
        }
    }


}
