using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.BoardSystem;
using System;
using DAE.HexagonalSystem;
using DAE.HexenSystem;
using DAE.SelectionSystem;

namespace DAE.GameSystem
{
	public class GameLoop : Singleton<GameLoop>
	{
		#region Inspector Fields
		[SerializeField] private HexagonalGridHelper _helper = null;

		[SerializeField] private Piece<HexagonTile> _playerPiece = null;
		[SerializeField] private Piece<HexagonTile> _enemyPiece = null;
		#endregion

		#region Fields
		private SelectionManager<Piece<HexagonTile>> _selectionManager;
		private int _currentPlayerID = 0;

		private Board<Piece<HexagonTile>, HexagonTile> _board = new Board<Piece<HexagonTile>, HexagonTile>();

		private Grid<Hexagon> _grid = new Grid<Hexagon>();
		#endregion

		#region Life Cycle
		private void Start()
		{
			_selectionManager = new SelectionManager<Piece<HexagonTile>>();

			Board<Piece<HexagonTile>, HexagonTile> board = new Board<Piece<HexagonTile>, HexagonTile>();
			HexagonalGrid hexagonalGrid = new HexagonalGrid(_helper.GridRadius);

			_selectionManager.Selected += (sender, eventArgs) =>
			{
				eventArgs.SelectedItem.Activate = true;
			};

			_selectionManager.Deselected += (sender, eventArgs) =>
			{
				eventArgs.SelectedItem.Activate = false;
			};

			RegisterTiles(hexagonalGrid, _grid);
			PlaceTiles(_grid);

			SpawnPlayer();
			SpawnEnemies();
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
				HexagonTile tile = Instantiate(_helper.HexagonPrefab, hexagon.ToWorldPosition(), Quaternion.identity, transform);
				tile.Hexagon = hexagon;
				hexagon.HexagonTile = tile;
			}
		}

		private void SpawnPlayer()
		{
			SpawnPiece(_playerPiece, 0, 0);
		}

		private void SpawnEnemies()
		{
			SpawnPiece(_enemyPiece, 0, -3);
			SpawnPiece(_enemyPiece, 2, -2);
			SpawnPiece(_enemyPiece, 0, -1);
			SpawnPiece(_enemyPiece, 1, -1);
			SpawnPiece(_enemyPiece, 1, -3);
			SpawnPiece(_enemyPiece, 3, 0);
			SpawnPiece(_enemyPiece, -2, 2);
			SpawnPiece(_enemyPiece, 1, 2);
		}


		private void SpawnPiece(Piece<HexagonTile> piecePrefab, int x, int y)
		{
			if(_grid.TryGetPositionAt(x, y, out Hexagon hexagon))
			{
				Piece<HexagonTile> piece = Instantiate(piecePrefab, hexagon.HexagonTile.transform.position, Quaternion.identity);
				_board.Place(piece, hexagon.HexagonTile);
			}
			else
			{
				throw new Exception("Given coordinates don't exist!");
			}
		}
		#endregion
	}
}
