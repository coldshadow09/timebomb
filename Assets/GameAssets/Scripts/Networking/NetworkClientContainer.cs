using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkClientContainer {

	private NetworkClient client;

	public NetworkClientContainer() {
		client = new NetworkClient();
	}

	public virtual void RegisterHandler(short msgType, NetworkMessageDelegate handler) {
		client.RegisterHandler(msgType, handler);
	}

	public virtual void Connect(string host, int port) {
		client.Connect(host, port);
	}

	public virtual void Disconnect() {
		client.Disconnect();
	}

	public virtual void Send(short type, MessageBase msg) {
		client.Send(type, msg);
	}

}
