using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerUsername : NetworkBehaviour {
    private GameObject[] players;
    private Color labelColor;
    void Start() {
        players = GameObject.FindGameObjectsWithTag("Player");
        labelColor = new Color(Random.value, Random.value, Random.value); // Colour of label
    }
    void OnGUI() {
        string username = "Player";
        string score = "Score";
        GUIStyle label = new GUIStyle("label");
        label.alignment = TextAnchor.UpperCenter;
        label.font.material.color = labelColor;
        foreach (GameObject player in players) {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(player.transform.position);
            // If a player is within view
            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1) {
                GUI.Label(new Rect(screenPoint.x - 50, Screen.height - (screenPoint.y + 80), 150, 30), username + System.Environment.NewLine + score, label);
            }
        }
    }
}

