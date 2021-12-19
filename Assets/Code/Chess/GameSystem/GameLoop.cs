using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.BoardSystem;
using System;
using DAE.HexagonalSystem;

namespace DAE.GameSystem
{
	public class GameLoop : Singleton<GameLoop>
	{
		#region Inspector Fields
		[SerializeField] private HexagonalGridHelper _helper = null;
		#endregion

		#region Properties

		#endregion

		#region Fields

		#endregion

		#region Life Cycle
		private void Start()
		{
			HexagonalGrid hexagonalGrid = new HexagonalGrid(_helper.GridRadius);
			Grid<Hexagon> grid = new Grid<Hexagon>();

			RegisterTiles(hexagonalGrid, grid);
			PlaceTiles(grid);
		}
		#endregion

		#region Methods
		private static void RegisterTiles(HexagonalGrid hexagonalGrid, Grid<Hexagon> grid)
		{
			foreach (Hexagon hexagon in hexagonalGrid.Hexagons)
			{
				grid.Register(hexagon, hexagon.Q, hexagon.R);
			}
		}

		private void PlaceTiles(Grid<Hexagon> grid)
		{
			List<Hexagon> hexagons = grid.GetPositions();

			foreach (Hexagon hexagon in hexagons)
			{
				Instantiate(_helper.HexagonPrefab, hexagon.ToWorldPosition(), Quaternion.identity, transform);
			}
		}
		#endregion
	} 
}


//private void Start()
//{
//	Board<Piece<Tile>, Tile> board = new Board<Piece<Tile>, Tile>();
//	Grid<Tile> grid = new Grid<Tile>(8, 8);
//	ReplayManager replayManager = new ReplayManager();

//	_gameStateMachine = new StateMachine<GameStateBase>();
//	_gameStateMachine.Register(GameStateBase.PlayingState, new PlayingGameState(_gameStateMachine, board, grid, replayManager));
//	_gameStateMachine.Register(GameStateBase.ReplayingState, new ReplayGameState(_gameStateMachine, replayManager));

//	_gameStateMachine.InitialState = GameStateBase.PlayingState;

//	ConnectTiles(board, grid);
//	ConnectPieces(board, grid);
//}

//private void ConnectTiles(Board<Piece<Tile>, Tile> board, Grid<Tile> grid)
//{
//	Tile[] tiles = FindObjectsOfType<Tile>();
//	foreach (Tile tile in tiles)
//	{
//		(int x, int y) = _positionHelper.WorldToGridPosition(board, grid, tile.transform.localPosition);

//		tile.Clicked += (source, eventArgs) => Select(eventArgs.Tile);

//		grid.Register(tile, x, y);
//	}
//}

//private void ConnectPieces(Board<Piece<Tile>, Tile> board, Grid<Tile> grid)
//{
//	PieceView[] pieceViews = FindObjectsOfType<PieceView>();
//	foreach (PieceView pieceView in pieceViews)
//	{
//		Piece<Tile> piece = new Piece<Tile>();
//		piece.PieceType = pieceView.PieceType;
//		piece.PlayerID = pieceView.PlayerID;

//		pieceView.Model = piece;

//		var (x, y) = _positionHelper.WorldToGridPosition(board, grid, pieceView.transform.localPosition);

//		if (grid.TryGetPositionAt(x, y, out Tile tile))
//		{
//			board.Place(piece, tile);
//		}
//	}
//}

//private void Select(Piece<Tile> piece)
//	=> _gameStateMachine.CurrentState.Select(piece);

//private void Select(Tile tile)
//	=> _gameStateMachine.CurrentState.Select(tile);

//public void Backward()
//	=> _gameStateMachine.CurrentState.Backward();

//public void Forward()
//	=> _gameStateMachine.CurrentState.Forward();