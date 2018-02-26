using System;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationPlayerMoveRight : MonoBehaviour
{
	/* This is a script to move the player the the right during tests to ensure to imitate player movement in integration tests */
	void Start()
	{

		//IntegrationTest.Pass(gameObject);
	}

	void Update()
	{
		transform.Translate(1, 0, 0);
	}
}
