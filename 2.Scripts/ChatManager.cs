using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Fusion;
using Photon.Chat;
using ExitGames.Client.Photon;
using UnityEngine.UI;



public class ChatManager : MonoBehaviour, IChatClientListener
{
    public string userName;  //�г��� �α����ϸ鼭 �����ð�
    public InputField chatinputField;  // �Է�ĭ ����
    public Text currentChannelTxt;  //ä��â �ؽ�Ʈ
    public Text outputTxt;  // ����׹��� ǥ�ÿ� �ؽ�Ʈ ����
    string currentChannelName;  // �Է��� ä�� �̸�
    ChatClient chatClient;  // ê ������ �����ϴ� Ŭ���̾�Ʈ

    public ScrollRect scrollRect; //��ũ�Ѻ並 ����

    private void Start()
    {
        userName = FindObjectOfType<NetworkManager>().inputName.text;  // �������� ��������
        currentChannelName = "channel";  //ä�� �̸� ���ϱ�
        chatClient = new ChatClient(this); //ä�� Ŭ���̾�Ʈ ����
        chatClient.UseBackgroundWorkerForSending = true; //��׶���� ���� ������ �Ȳ����

        var appIdChat = Fusion.Photon.Realtime.PhotonAppSettings.Instance.AppSettings.AppIdChat;
        //���� ê ���̵� ��������
        chatClient.Connect(appIdChat, "1,0", new AuthenticationValues(userName));
        // ê ����
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
        if (channelName.Equals(currentChannelName)) //ä�� �̸��� ���ؼ� �ؽ�Ʈ�� ������
        {
            if (string.IsNullOrEmpty(currentChannelName)) return;
            ChatChannel channel = null;
            if(!chatClient.TryGetChannel(currentChannelName,out channel))
            {
                Debug.Log("ä�� ã�� ����"+ currentChannelName);
                return;
            }
            currentChannelTxt.text = channel.ToStringMessages();
            Canvas.ForceUpdateCanvases(); //ĵ���� ���� ������Ʈ
            scrollRect.verticalNormalizedPosition = 0; //��ũ�� ������ 0����
            
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
        chatinputField.ActivateInputField();//�ٽ� ��ǲ�ʵ忡 Ŀ�� Ȱ��ȭ 
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
        Debug.Log("������ ������ ������ϴ�.");
        AddLine("���� ������ ���������ϴ�.");
    }

    public void OnConnected()
    {
        Debug.Log("������ ����Ǿ����ϴ�.");
        AddLine("������ ����Ǿ����ϴ�.");
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
        AddLine($"ä������{channel[0]}");
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    
    }

    #endregion
}

