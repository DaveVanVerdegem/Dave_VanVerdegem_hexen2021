using DAE.BoardSystem;
using DAE.Commons;
using DAE.HexenSystem.Moves;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace DAE.HexenSystem
{
	public class MoveManager<TTile> where TTile : MonoBehaviour, ITile
	{
		#region Properties

		#endregion

		#region Fields
		private MultiValueDictionary<CardType, IMove<TTile>> _moves = new MultiValueDictionary<CardType, IMove<TTile>>();
		private MultiValueDictionary<CardType, IMove<TTile>> _targets = new MultiValueDictionary<CardType, IMove<TTile>>();

		private Board<Piece<TTile>, TTile> _board;
		private Grid<TTile> _grid;
		#endregion

		#region Constructors
		public MoveManager(Board<Piece<TTile>, TTile> board, Grid<TTile> grid)
		{
			_board = board;

			_board.PieceMoved += (sender, eventArgs) => eventArgs.Piece.MoveTo(eventArgs.ToPosition);
			_board.PiecePlaced += (sender, eventArgs) => eventArgs.Piece.PlaceAt(eventArgs.AtPosition);
			_board.PieceTaken += (sender, eventArgs) => eventArgs.Piece.TakeFrom(eventArgs.FromPosition);

			_grid = grid;

			_moves.Add(
				CardType.Slash,
					new ConfigurableMove<TTile>(board, grid,
						(b, g, p) => new MovementHelper<TTile>(b, g, p)
										.Direction(0)
										.Direction(1)
										.Direction(2)
										.Direction(3)
										.Direction(4)
										.Direction(5)
										.CollectValidPositions()));

			//_moves.Add(PieceType.King,
			//    new ConfigurableMove<TPosition>(board, grid,
			//            (b, g, p) => new MovementHelper<TPosition>(b, g, p)
			//                            .North(1)
			//                            .NorthEast(1)
			//                            .East(1)
			//                            .SouthEast(1)
			//                            .South(1)
			//                            .SouthWest(1)
			//                            .West(1)
			//                            .NorthWest(1)
			//                            .CollectValidPositions()));

			//_moves.Add(PieceType.Queen,
			//    new ConfigurableMove<TPosition>(board, grid,
			//            (b, g, p) => new MovementHelper<TPosition>(b, g, p)
			//                           .North()
			//                           .NorthEast()
			//                           .East()
			//                           .SouthEast()
			//                           .South()
			//                           .SouthWest()
			//                           .West()
			//                           .NorthWest()
			//                           .CollectValidPositions()));
		}
		#endregion

		#region Methods
		public List<TTile> ValidPositionsFor(Piece<TTile> piece, CardType cardType)
		{
			List<TTile> result = _moves[cardType]
				.Where((m) => m.CanExecute(piece))
				.SelectMany((m) => m.Positions(piece))
				.ToList();

			return result;
		}

		public List<TTile> ValidPositionsFor(Piece<TTile> piece)
		{
			throw new NotImplementedException();

			//var result = _moves[piece.PieceType]
			//                .Where((m) => m.CanExecute(piece))
			//                .SelectMany((m) => m.Positions(piece))
			//                .ToList();

			//return result;
		}

		public void Move(Piece<TTile> piece, TTile position)
		{
			throw new NotImplementedException();

			//var move = _moves[piece.PieceType]
			//    .Where(m => m.CanExecute(piece))
			//    .Where(m => m.Positions(piece).Contains(position))
			//    .First();

			//move.Execute(piece, position);
		}
		#endregion
	} 
}
