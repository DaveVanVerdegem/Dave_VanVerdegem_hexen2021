using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexagonalGrid 
{
	#region Inspector Fields
	#endregion

	#region Properties

	#endregion

	#region Fields
	private int _radius = 3;
	private List<Hexagon> _hexagons;
	#endregion

	#region Methods
	public void GenerateGrid(Hexagon center)
	{
		_hexagons = new List<Hexagon>();
		_hexagons.Add(center);

		for (int q = -_radius; q <= _radius; q++)
		{
			int r1 = Mathf.Max(-_radius, - q - _radius);
			int r2 = Mathf.Min(_radius, -q + _radius);

			for (int r = r1; r <= r2; r++)
			{
				_hexagons.Add(new Hexagon(q, r, - q - r));
			}
		}
	}
	#endregion
}