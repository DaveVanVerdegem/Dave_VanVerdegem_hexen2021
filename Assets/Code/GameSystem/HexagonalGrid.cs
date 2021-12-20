using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.GameSystem
{
	public class HexagonalGrid
	{
		#region Properties
		public List<Hexagon> Hexagons => _hexagons;
		#endregion

		#region Fields
		private List<Hexagon> _hexagons = new List<Hexagon>();
		#endregion

		#region Constructors
		public HexagonalGrid(int radius)
		{
			for (int q = -radius; q <= radius; q++)
			{
				int r1 = Mathf.Max(-radius, -q - radius);
				int r2 = Mathf.Min(radius, -q + radius);

				for (int r = r1; r <= r2; r++)
				{
					_hexagons.Add(new Hexagon(q, r, -q - r));
				}
			}
		}
		#endregion
	} 
}
