using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions 
{
	private static Vector3Int[] _directions =
{
		new Vector3Int(1, 0, -1),
		new Vector3Int(1, -1, 0),
		new Vector3Int(0, -1, 1),
		new Vector3Int(-1, 0, 1),
		new Vector3Int(-1, 1, 0),
		new Vector3Int(0, 1, -1),
	};

	#region Methods
	public static Vector3Int Get(int direction /* 0 to 5 */)
	{
		if (direction < 0 && direction >= 6)
			throw new ArgumentOutOfRangeException("Direction should be between 0 to 5.");

		return _directions[direction];
	}
	#endregion
}
