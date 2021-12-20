using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.BoardSystem;
using System;
using DAE.HexenSystem;
using DAE.SelectionSystem;

namespace DAE.GameSystem
{
	public class GameLoop : Singleton<GameLoop>
	{
		#region Inspector Fields
		[SerializeField] private HexagonalGridHelper _helper = null;
		#endregion

		#region Fields
		private SelectionManager<HexagonTile> _selectionManager;
		private int _currentPlayerID = 0;

		private Board<Piece<HexagonTile>, HexagonTile> _board = new Board<Piece<HexagonTile>, HexagonTile>();

		private Grid<HexagonTile> _grid = new Grid<HexagonTile>();

		private MoveManager<HexagonTile> _moveManager;

		private Piece<HexagonTile> _playerPiece = null;
		#endregion

		#region Life Cycle
		private void Start()
		{
			_selectionManager = new SelectionManager<HexagonTile>();
			

			Board<Piece<HexagonTile>, HexagonTile> board = new Board<Piece<HexagonTile>, HexagonTile>();
			HexagonalGrid hexagonalGrid = new HexagonalGrid(_helper.GridRadius);

			_moveManager = new MoveManager<HexagonTile>(_board, _grid);

			_selectionManager.Selected += (sender, eventArgs) =>
			{
				//List<HexagonTile> tiles = _moveManager.ValidPositionsFor(eventArgs.SelectedItem);
				//foreach (HexagonTile tile in tiles)
				//	tile.Highlight = true;
			};

			_selectionManager.Deselected += (sender, eventArgs) =>
			{
				//List<HexagonTile> tiles = _moveManager.ValidPositionsFor(eventArgs.SelectedItem);
				//foreach (HexagonTile tile in tiles)
				//	tile.Highlight = false;
			};

			PlaceTiles(hexagonalGrid);
			RegisterTiles(hexagonalGrid, _grid);

			SpawnPlayer();
			SpawnEnemies();
		}
		#endregion

		#region Methods
		private static void RegisterTiles(HexagonalGrid hexagonalGrid, Grid<HexagonTile> grid)
		{
			foreach (Hexagon hexagon in hexagonalGrid.Hexagons)
			{
				grid.Register(hexagon.HexagonTile, hexagon.Q, hexagon.R, hexagon.S);
			}
		}

		private void PlaceTiles(HexagonalGrid grid)
		{
			foreach (Hexagon hexagon in grid.Hexagons)
			{
				HexagonTile tile = Instantiate(_helper.HexagonPrefab, hexagon.ToWorldPosition(), Quaternion.identity, transform);
				tile.Hexagon = hexagon;
				hexagon.HexagonTile = tile;


			}
		}

		private void SpawnPlayer()
		{
			_playerPiece = SpawnPiece(_helper.PlayerPiecePrefab, 0, 0, 0);
		}

		private void SpawnEnemies()
		{
			SpawnPiece(_helper.EnemyPiecePrefab, 0, -3, 3);
			SpawnPiece(_helper.EnemyPiecePrefab, 2, -2, 0);
			SpawnPiece(_helper.EnemyPiecePrefab, 0, -1, 1);
			SpawnPiece(_helper.EnemyPiecePrefab, 1, -1, 0);
			SpawnPiece(_helper.EnemyPiecePrefab, -1, 1, 0);
			SpawnPiece(_helper.EnemyPiecePrefab, 3, 0, -3);
			SpawnPiece(_helper.EnemyPiecePrefab, -2, 2, 0);
			SpawnPiece(_helper.EnemyPiecePrefab, 1, 2, -3);
		}


		private Piece<HexagonTile> SpawnPiece(Piece<HexagonTile> piecePrefab, int q, int r, int s)
		{
			if(_grid.TryGetPositionAt(q, r, s, out HexagonTile tile))
			{
				Piece<HexagonTile> piece = Instantiate(piecePrefab, tile.transform.position, Quaternion.identity);
				_board.Place(piece, tile);

				return piece;
			}
			else
			{
				throw new Exception("Given coordinates don't exist!");
			}
		}

		public void Highlight(Card card, HexagonTile hexagonTile)
		{
			UnhighlightAll();

			List<HexagonTile> validPositions = _moveManager.ValidPositionsFor(_playerPiece, card.CardType);

			if(validPositions.Contains(hexagonTile))
			{
				foreach (HexagonTile tile in validPositions)
					tile.Highlight = true;
			}
			else
			{
				foreach (HexagonTile tile in validPositions)
					tile.Highlight = true;
			}
		}

		public void UnhighlightAll()
		{
			List<HexagonTile> tiles = _grid.GetPositions();

			foreach (HexagonTile tile in tiles)
				tile.Highlight = false;
		}

		//private void Select(Piece<HexagonTile> piece)
		//{
		//	if (piece.PlayerID == _currentPlayerID)
		//	{
		//		_selectionManager.DeselectAll();
		//		_selectionManager.Select(piece);
		//	}
		//	else
		//	{
		//		if (_board.TryGetPosition(piece, out HexagonTile tile))
		//		{
		//			Select(tile);
		//		}
		//	}

		//}

		private void Select(HexagonTile tile)
		{


			//if (_selectionManager.HasSelection)
			//{
			//	HexagonTile selectedTile = _selectionManager.SelectedItem;
			//	_selectionManager.Deselect(selectedTile);

			//	//List<HexagonTile> validPositions = _moveManager.ValidPositionsFor(selectedTile);
			//	//if (validPositions.Contains(tile))
			//	//{
			//	//	_moveManager.Move(selectedTile, tile);
			//	//}
			//}

			_selectionManager.Select(tile);
		}

		//private void Select(HexagonTile tile)
		//{
		//	if (_board.TryGetPiece(tile, out Piece<HexagonTile> piece) && piece.PlayerID == _currentPlayerID)
		//	{
		//		Select(piece);
		//	}
		//	else
		//	{
		//		if (_selectionManager.HasSelection)
		//		{
		//			Piece<HexagonTile> selectedPiece = _selectionManager.SelectedItem;
		//			_selectionManager.Deselect(selectedPiece);

		//			List<HexagonTile> validPositions = _moveManager.ValidPositionsFor(selectedPiece);
		//			if (validPositions.Contains(tile))
		//			{
		//				_moveManager.Move(selectedPiece, tile);
		//			}
		//		}
		//	}
		//}

		public void DeselectAll()
			=> _selectionManager.DeselectAll();
		#endregion
	}
}
