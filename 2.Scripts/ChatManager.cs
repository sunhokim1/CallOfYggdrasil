using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Fusion;
using Photon.Chat;
using ExitGames.Client.Photon;
using UnityEngine.UI;



public class ChatManager : MonoBehaviour, IChatClientListener
{
    public string userName;  //닉네임 로그인하면서 가져올거
    public InputField chatinputField;  // 입력칸 연결
    public Text currentChannelTxt;  //채팅창 텍스트
    public Text outputTxt;  // 디버그문자 표시용 텍스트 연결
    string currentChannelName;  // 입력한 채널 이름
    ChatClient chatClient;  // 챗 서버에 접속하는 클라이언트

    public ScrollRect scrollRect; //스크롤뷰를 연결

    private void Start()
    {
        userName = FindObjectOfType<NetworkManager>().inputName.text;  // 유저네임 가져오기
        currentChannelName = "channel";  //채널 이름 정하기
        chatClient = new ChatClient(this); //채팅 클라이언트 생성
        chatClient.UseBackgroundWorkerForSending = true; //백그라운드로 들어가도 연결이 안끊기게

        var appIdChat = Fusion.Photon.Realtime.PhotonAppSettings.Instance.AppSettings.AppIdChat;
        //포톤 챗 아이디 가져오기
        chatClient.Connect(appIdChat, "1,0", new AuthenticationValues(userName));
        // 챗 연결
    }



    #region IChatClientListener implementation

    void OnApplicationQuit()
    {
        if ((chatClient != null)) chatClient.Disconnect();
        
    }
    private void OnDisable()
    {
        if(chatClient != null) chatClient.Disconnect();
    }

    void AddLine(string line)
    {
        outputTxt.text += line = "\r\n";
    }


    public void OnGetMessages(string channelName, string[] senders, object[] messages) 
    {
        if (channelName.Equals(currentChannelName)) //채널 이름을 비교해서 텍스트를 가져옴
        {
            if (string.IsNullOrEmpty(currentChannelName)) return;
            ChatChannel channel = null;
            if(!chatClient.TryGetChannel(currentChannelName,out channel))
            {
                Debug.Log("채널 찾기 실패"+ currentChannelName);
                return;
            }
            currentChannelTxt.text = channel.ToStringMessages();
            Canvas.ForceUpdateCanvases(); //캔버스 정보 업데이트
            scrollRect.verticalNormalizedPosition = 0; //스크롤 포지션 0으로
            
        }
    }

    private void Update()
    {
        chatClient.Service();
        if (Input.GetKeyDown(KeyCode.Return)) OnEnterSend();
    }

    public void OnEnterSend()
    {
        if (string.IsNullOrEmpty(chatinputField.text)) return;
        chatClient.PublishMessage(currentChannelName,chatinputField.text);
        chatinputField.text = "";

        chatinputField.Select();
        chatinputField.ActivateInputField();//다시 인풋필드에 커서 활성화 
    }
    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
    {
       if(level ==ExitGames.Client.Photon.DebugLevel.ERROR)
        {
            Debug.LogError(message);
        }
       else if(level == ExitGames.Client.Photon.DebugLevel.WARNING) 
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.Log(message);
        }
    }

    public void OnDisconnected()
    {
        Debug.Log("서버에 연결이 끊겼습니다.");
        AddLine("서버 연결이 끊어졌습니다.");
    }

    public void OnConnected()
    {
        Debug.Log("서버에 연결되었습니다.");
        AddLine("서버에 연결되었습니다.");
        chatClient.Subscribe(new string[] { currentChannelName }, 10);
    }

    public void OnChatStateChange(ChatState state)
    {
       
    }



    public void OnPrivateMessage(string sender, object message, string channelName)
    {
       
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
       
    }

    public void OnUnsubscribed(string[] channels)
    {
      
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
       
    }

    public void OnUserSubscribed(string channel, string user)
    {
        AddLine($"채널입장{channel[0]}");
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    
    }

    #endregion
}

