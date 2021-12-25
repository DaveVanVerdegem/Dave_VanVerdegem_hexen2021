using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtensions
{
	#region Methods
	public static int Modulo(this int integer, int modulo)
	{
		return ((integer % modulo) + modulo) % modulo;
	}
	#endregion
}
