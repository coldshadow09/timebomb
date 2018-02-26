using UnityEngine;
using System;
using System.Collections;

public class DuplicatePlayerProfileException : Exception {
	public DuplicatePlayerProfileException(string error) : base(error) {}
}
