using DAE.BoardSystem;
using DAE.GameSystem.Cards;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.GameSystem
{
	public class Deck<TCard, TPiece, TTile> where TCard : MonoBehaviour, ICard<TPiece, TTile>
	{
		#region Properties
		public event EventHandler<CardEventArgs<TCard>> CardPlayed;
		#endregion

		#region Fields
		private Board<TPiece, TTile> _board;
		private Grid<TTile> _grid;

		private List<TCard> _cards = new List<TCard>();
		#endregion

		#region Constructors
		public Deck(Board<TPiece, TTile> board, Grid<TTile> grid)
		{
			_board = board;
			_grid = grid;
		}
		#endregion

		#region Methods
		public void Register(TCard card)
		{
			_cards.Add(card);
			card.Initialize(_board, _grid);
		}

		public void FillHand()
		{
			int activeCards = 0;

			foreach (TCard card in _cards)
			{
				if (activeCards == 5)
					break;

				if (!card.gameObject.activeInHierarchy)
					card.gameObject.SetActive(true);

				activeCards++;
			}
		}

		public void PlayCard(TCard card, TPiece piece, TTile tile)
		{
			if (card.Execute(piece, tile))
			{
				_cards.Remove(card);
				FillHand();
			}
				
		}

		public void OnCardPlayed(CardEventArgs<TCard> eventArgs)
		{
			EventHandler<CardEventArgs<TCard>> handler = CardPlayed;
			handler?.Invoke(this, eventArgs);
		}
		#endregion
	}

	#region EventArgs
	public class CardEventArgs<TCard> : EventArgs
	{
		public TCard Card { get; }

		public CardEventArgs(TCard card)
		{
			Card = card;
		}
	}
	#endregion
}
