using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public string id;
    public override void Spawned()
    {
        StartCoroutine(FireMove());
    }

    IEnumerator FireMove()
    {
        float timer = 5;
        while (timer >= 0)
        {
            transform.position += transform.forward * Runner.DeltaTime * 10;
            timer -= Runner.DeltaTime;
            if (timer <= 0) break;
            yield return null;
        }
        Runner.Despawn(GetComponent<NetworkObject>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Runner.LocalPlayer) return;
        if(other.TryGetComponent(out NetworkObject netObg)) //��Ʈ�� ������Ʈ�� �ִ°�
        {
            if (other.TryGetComponent(out PlayerMovement player)) //�÷��̾� ��ũ��Ʈ�� ������
            {
                if (id != player.Runner.UserId)
                {
                    print($"{id} :{player.Runner.UserId}");
                   // player.RpcDamagePlayer(10); //�÷��̾� ������
                    Debug.Log("hit");
                    Runner.Despawn(GetComponent<NetworkObject>());
                }
            }
            else
                Runner.Despawn(GetComponent<NetworkObject>());
        }
        else
            Runner.Despawn(GetComponent<NetworkObject>()); //ź�� �����


    }
}
