using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.UI;
using Photon.Realtime;
//using UnityEngine.Rendering.UI;

public class PlayerMovement : NetworkBehaviour
{
    NetworkCharacterControllerPrototype cc;
    Animator animator;
    CinemachineFreeLook cam;
    CharacterController controller;
    NetworkRunner runner;
    NetworkManager netManager;
    public bool isMe = false;

    public Transform camTarget;
     [Networked] string username { get; set; }

     ////[SerializeField] Image hpbar;
     //[Networked(OnChanged = nameof(OnChangedHp))] public int currentHp { get; set; }
     ////[Networked] public int currentHP { get; set; }

     //[SerializeField] int maxHP = 100;


    [Networked(OnChanged = nameof(OnChangedScore))] public int _score { get; set; } //_score가 변경되면 OnChangedScore함수를 실행

    public Text nickname , score;

    private void Awake()
    {
        cam = FindObjectOfType<CinemachineFreeLook>();
        animator = GetComponent<Animator>();
        cc = GetComponent<NetworkCharacterControllerPrototype>();
        
    }

    //public static void OnChangedHp(Changed<PlayerMovement> changed)// PlayerMovement클래스
    //{
    //    changed.Behaviour.OnChangeHp();
    //}

    //void OnChangeHp()
    //{
    //    hpbar.fillAmount = (float)currentHp / maxHP;

    //    Debug.Log(hpbar.fillAmount);
    //}
    //[Rpc(RpcSources.All,RpcTargets.StateAuthority)]

    //public void RpcDamagePlayer(int damage)
    //{
    //    currentHp -= damage;
    //}
    public static void OnChangedScore(Changed<PlayerMovement> changed) //PlayerMovement클래스에서 변화를 감지했을때
    {
        changed.Behaviour.OnChangedScore(); //텍스트 업데이트 실행
    }

    void OnChangedScore()
    {
        score.text = _score.ToString(); // 스코어 텍스트 업데이트
    }
 


    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            cam.LookAt = camTarget;
            cam.Follow = camTarget;
            username = FindObjectOfType<NetworkManager>().inputName.text;
            isMe = true;
        }
        nickname.text = username;
       // currentHp = maxHP;
       // RpcDamagePlayer(0);
    }

    private void Move()
    {
        if (!HasInputAuthority) return; // 네트워크 오브젝트에 대한 입력 권한이 없으면 이동안되게
        if(GetInput(out NetworkInputData data))
        {
            var moveDir = Vector3.zero;
            moveDir = Camera.main.transform.right * data.dir.x
                      + Camera.main.transform.forward * data.dir.z; //메인카메라 방향을 기준으로 이동방향 잡기
            moveDir.Normalize();
            moveDir.y = 0;
            moveDir.Normalize();
            if(data.dir == Vector3.zero)
            {
                animator.SetBool("isWalk", false);
            }
            else
            {
                animator.SetBool("isWalk", true);
            }

                cc.Move(moveDir * Runner.DeltaTime);

                if (data.isJump) cc.Jump();

        }
    }

    public override void FixedUpdateNetwork()
    {
        nickname.transform.parent.LookAt(Camera.main.transform);
        Move();
    }
}
