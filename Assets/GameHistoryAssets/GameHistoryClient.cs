using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class GameHistoryClient : MonoBehaviour {

	public string masterServerHost;
	public int masterServerPort;

	public NetworkClientContainer client;

	public GameHistoryClientMessenger messenger;

	// Use this for initialization
	public virtual void Start () {
		MessageFactory factory = new MessageFactory();
		client 	= new NetworkClientContainer();
		messenger = new GameHistoryClientMessenger(client, factory);
		
		NetworkTransport.Init();
		client.RegisterHandler(MsgType.Connect, OnConnect);
		client.Connect(masterServerHost, masterServerPort);
	}

	protected abstract void OnConnect(NetworkMessage msg);

	public void OnDestroy() {
		if (client != null)
			client.Disconnect();
	}
	
}
