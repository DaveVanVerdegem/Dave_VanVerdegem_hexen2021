using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexagonalGrid 
{
	#region Properties
	public List<Hexagon> Hexagons { get; private set; }
	#endregion

	#region Fields
	private int _radius = 3;
	#endregion

	#region Methods
	public void GenerateGrid()
	{
		Hexagons = new List<Hexagon>();

		for (int q = -_radius; q <= _radius; q++)
		{
			int r1 = Mathf.Max(-_radius, - q - _radius);
			int r2 = Mathf.Min(_radius, -q + _radius);

			for (int r = r1; r <= r2; r++)
			{
				Hexagons.Add(new Hexagon(q, r, - q - r));
			}
		}
	}
	#endregion
}