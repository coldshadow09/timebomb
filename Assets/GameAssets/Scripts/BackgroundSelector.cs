using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackgroundSelector: NetworkBehaviour {
	public enum RoomType {
		ROOM_DAY,
		ROOM_NIGHT,
		ROOM_INVALID
	};

	[SyncVar(hook = "SetNewRoom")]
	public RoomType room = RoomType.ROOM_INVALID;

	void Start() {
		if (isServer) {
			string roomString = System.Environment.GetEnvironmentVariable ("ROOM_TYPE");

			switch (roomString) {
			case "DAY":
				room = RoomType.ROOM_DAY;
				break;
			case "NIGHT":
				room = RoomType.ROOM_NIGHT;
				break;
			}
		}

		SetNewRoom (room);
	}

	public void SetNewRoom(RoomType newRoom) {
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		string resourcePath = "";

		switch (newRoom) {
		case RoomType.ROOM_DAY:
			resourcePath = "map1";
			break;
		case RoomType.ROOM_NIGHT:
			resourcePath = "map2";
			break;
		}

		if (resourcePath != "") {
			renderer.sprite = Resources.Load<Sprite> (resourcePath);
		}
	}
}
