using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.HexenSystem.Moves
{
	class ConfigurableMove<TPosition> : MoveBase<TPosition> where TPosition : MonoBehaviour, ITile
	{
		#region Properties
		public delegate List<TPosition> PositionsCollector(Board<Piece<TPosition>, TPosition> board, Grid<TPosition> grid, Piece<TPosition> piece);
		#endregion

		#region Fields
		private PositionsCollector _collectPositions;
		#endregion

		#region Constructors
		public ConfigurableMove(Board<Piece<TPosition>, TPosition> board, Grid<TPosition> grid, PositionsCollector positionsCollector) : base(board, grid)
		{
			_collectPositions = positionsCollector;
		}
		#endregion

		#region Methods
		public override List<TPosition> Positions(Piece<TPosition> piece)
			=> _collectPositions(_board, _grid, piece); 
		#endregion
	}
}
