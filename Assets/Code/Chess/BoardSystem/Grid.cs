using DAE.Commons;
using DAE.HexagonalSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.BoardSystem
{
	public class Grid<TPosition>
	{
		#region Fields
		// TODO: Can this be simplified to <TTile, TPosition> ? (where TPosition : (int q, int r, int s))
		private BidirectionalDictionary<TPosition, (int x, int y)> _positions = new BidirectionalDictionary<TPosition, (int x, int y)>(); 
		#endregion

		#region Constructors
		public Grid()
		{
		} 
		#endregion

		#region Methods
		public void Register(TPosition position, int x, int y)
		{
			_positions.Add(position, (x, y));
		}
		#endregion

		#region Return Methods
		public bool TryGetPositionAt(int x, int y, out TPosition position)
			=> _positions.TryGetKey((x, y), out position);

		public bool TryGetCoordinatesAt(TPosition position, out (int x, int y) coordinate)
			=> _positions.TryGetValue(position, out coordinate);

		public List<TPosition> GetPositions()
			=> _positions.Keys.ToList();
		#endregion
	}
}