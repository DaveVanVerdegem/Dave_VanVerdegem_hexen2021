using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalGrid 
{
	#region Inspector Fields
	#endregion

	#region Properties

	#endregion

	#region Fields
	private int _range = 3;
	#endregion

	#region Methods
	public void GenerateGrid(Hexagon center)
	{
		List<Hexagon> _hexagons = new List<Hexagon>();
		_hexagons.Add(center);

		for (int q = -_range; q < _range; q++)
		{
			int maximumQ = Mathf.Max(-_range, -q - _range);
			int minimumQ = Mathf.Min(_range, -q + _range);

			for (int r = minimumQ; r < maximumQ; r++)
			{
				int s = -q - r;

				_hexagons.Add(Hexagon.Add(center, new Hexagon(q, r, s)));
			}
		}
	}
	#endregion
}