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
        if (!HasInputAuthority) return; //��Ʈ��ũ ������Ʈ�� ���� ������ ������ �̵��ȵǰ�
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
