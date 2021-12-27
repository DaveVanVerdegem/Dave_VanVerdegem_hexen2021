using System.Collections.Generic;
using UnityEngine;
using DAE.BoardSystem;
using System;
using DAE.GameSystem;
using DAE.GameSystem.Cards;

namespace DAE.GameSystem
{
	public class GameLoop : Singleton<GameLoop>
	{
		#region Inspector Fields
		[SerializeField] private HexagonalGridHelper _helper = null;

		[SerializeField] private List<BaseCard<Piece<HexagonTile>,HexagonTile>> _cardPrefabs = new List<BaseCard<Piece<HexagonTile>, HexagonTile>>();
		[SerializeField] private Transform _deckTransform = null;
		#endregion

		#region Properties
		public Piece<HexagonTile> PlayerPiece => _playerPiece;
		public BaseCard<Piece<HexagonTile>, HexagonTile> SelectedCard => _selectedCard;
		#endregion

		#region Fields
		private Board<Piece<HexagonTile>, HexagonTile> _board = new Board<Piece<HexagonTile>, HexagonTile>();
		private Grid<HexagonTile> _grid = new Grid<HexagonTile>();
		private Piece<HexagonTile> _playerPiece = null;
		private Deck<BaseCard<Piece<HexagonTile>, HexagonTile>, Piece<HexagonTile>, HexagonTile> _deck;
		private BaseCard<Piece<HexagonTile>, HexagonTile> _selectedCard;
		#endregion

		#region Life Cycle
		private void Start()
		{
			Board<Piece<HexagonTile>, HexagonTile> board = new Board<Piece<HexagonTile>, HexagonTile>();
			HexagonalGrid hexagonalGrid = new HexagonalGrid(_helper.GridRadius);

			PlaceTiles(hexagonalGrid);
			RegisterTiles(hexagonalGrid, _grid);

			SpawnPlayer();
			SpawnEnemies();

			_deck = new Deck<BaseCard<Piece<HexagonTile>, HexagonTile>, Piece<HexagonTile>, HexagonTile>(_board, _grid);

			for (int i = 0; i < 10; i++)
			{
				BaseCard<Piece<HexagonTile>, HexagonTile> cardPrefab = _cardPrefabs[UnityEngine.Random.Range(0, _cardPrefabs.Count)];
				BaseCard<Piece<HexagonTile>, HexagonTile> card = Instantiate(cardPrefab, _deckTransform);
				_deck.Register(card);

				card.CardBeginDrag += (sender, eventArgs) => Select(eventArgs.Card);
				card.CardEndDrag += (sender, eventArgs) => DeselectAll();
			}

			_deck.FillHand();
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
			if(_grid.TryGetTileAt(q, r, s, out HexagonTile tile))
			{
				Piece<HexagonTile> piece = Instantiate(piecePrefab, tile.transform.position, Quaternion.identity);
				_board.Place(piece, tile);

				_board.PieceMoved += (sender, eventArgs) => eventArgs.Piece.MoveTo(eventArgs.ToTile);
				_board.PiecePlaced += (sender, eventArgs) => eventArgs.Piece.PlaceAt(eventArgs.AtTile);
				_board.PieceTaken += (sender, eventArgs) => eventArgs.Piece.TakeFrom(eventArgs.FromTile);

				return piece;
			}
			else
			{
				throw new Exception("Given coordinates don't exist!");
			}
		}

		public void Highlight(HexagonTile hexagonTile)
		{
			if (hexagonTile == null) return;
			if (SelectedCard == null) return;

			List<HexagonTile> tiles = SelectedCard.Positions(PlayerPiece, hexagonTile);

			foreach (HexagonTile tile in tiles)
				if(tile!= null) tile.Highlight = true;
		}

		public void Execute(HexagonTile hexagonTile)
		{
			_deck.PlayCard(SelectedCard, PlayerPiece, hexagonTile);
		}

		public void UnhighlightAll()
		{
			List<HexagonTile> tiles = _grid.GetTiles();

			foreach (HexagonTile tile in tiles)
				tile.Highlight = false;
		}

		private void Select(BaseCard<Piece<HexagonTile>, HexagonTile> card)
		{
			_selectedCard = card;
		}

		public void DeselectAll()
		{
			_selectedCard = null;
		}
		#endregion
	}
}
