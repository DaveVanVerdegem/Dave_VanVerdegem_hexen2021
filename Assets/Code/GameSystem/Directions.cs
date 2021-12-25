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
			direction = direction.Modulo(6);

		return _directions[direction];
	}

	public static int Get(Vector3Int direction)
	{
		for (int i = 0; i < _directions.Length; i++)
		{
			if (direction == _directions[i])
				return i;
		}

		return 0;
	}
	#endregion
}
