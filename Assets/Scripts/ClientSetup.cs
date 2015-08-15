using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ClientSetup : MonoBehaviour {
	public GameObject Player;
	public GameObject Bolt;

	NetworkClient myClient;

	public void Start()
	{
		ClientScene.RegisterPrefab(Player);
		ClientScene.RegisterPrefab (Bolt);
		
		myClient = new NetworkClient();
		Debug.Log("oollolo");
		//myClient.RegisterHandler(MsgType.Connect, OnConnected);
		myClient.Connect("127.0.0.1", 7777);
	}

}