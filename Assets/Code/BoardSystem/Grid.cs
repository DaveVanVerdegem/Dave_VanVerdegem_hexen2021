using DAE.Commons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.BoardSystem
{
	public class Grid<TTile>
	{
		#region Fields
		// TODO: Can this be simplified to <TTile, TPosition> ? (where TPosition : (int q, int r, int s))
		private BidirectionalDictionary<TTile, (int q, int r, int s)> _tiles = new BidirectionalDictionary<TTile, (int q, int r, int s)>(); 
		#endregion

		#region Methods
		public void Register(TTile tile, int q, int r, int s)
		{
			_tiles.Add(tile, (q, r, s));
		}
		#endregion

		#region Return Methods
		public bool TryGetTileAt(int q, int r, int s, out TTile tile)
			=> _tiles.TryGetKey((q, r, s), out tile);

		public bool TryGetCoordinatesAt(TTile tile, out (int q, int r, int s) coordinate)
			=> _tiles.TryGetValue(tile, out coordinate);

		public List<TTile> GetTiles()
			=> _tiles.Keys.ToList();
		#endregion
	}
}