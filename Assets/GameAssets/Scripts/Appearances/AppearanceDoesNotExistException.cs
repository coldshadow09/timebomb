using UnityEngine;
using System;
using System.Collections;

public class AppearanceDoesNotExistException : Exception {
	public AppearanceDoesNotExistException(string error) : base(error) {}
}
