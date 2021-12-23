using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.HexenSystem.Moves
{
	class ConfigurableMove<TTile> : MoveBase<TTile> where TTile : MonoBehaviour, ITile
	{
		#region Properties
		public delegate List<TTile> PositionsCollector(Board<Piece<TTile>, TTile> board, Grid<TTile> grid, Piece<TTile> piece);
		#endregion

		#region Fields
		private PositionsCollector _collectPositions;
		#endregion

		#region Constructors
		public ConfigurableMove(Board<Piece<TTile>, TTile> board, Grid<TTile> grid, PositionsCollector positionsCollector) : base(board, grid)
		{
			_collectPositions = positionsCollector;
		}
		#endregion

		#region Methods
		public override List<TTile> Positions(Piece<TTile> piece)
			=> _collectPositions(_board, _grid, piece); 
		#endregion
	}
}
