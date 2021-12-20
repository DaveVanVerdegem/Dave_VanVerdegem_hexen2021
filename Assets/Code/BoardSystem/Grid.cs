using DAE.Commons;
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
		private BidirectionalDictionary<TPosition, (int q, int r, int s)> _positions = new BidirectionalDictionary<TPosition, (int q, int r, int s)>(); 
		#endregion

		#region Methods
		public void Register(TPosition position, int q, int r, int s)
		{
			_positions.Add(position, (q, r, s));
		}
		#endregion

		#region Return Methods
		public bool TryGetPositionAt(int q, int r, int s, out TPosition position)
			=> _positions.TryGetKey((q, r, s), out position);

		public bool TryGetCoordinatesAt(TPosition position, out (int q, int r, int s) coordinate)
			=> _positions.TryGetValue(position, out coordinate);

		public List<TPosition> GetPositions()
			=> _positions.Keys.ToList();
		#endregion
	}
}