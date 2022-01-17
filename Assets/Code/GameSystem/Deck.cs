using DAE.BoardSystem;
using DAE.GameSystem.Cards;
using DAE.ReplaySystem;
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
		private ReplayManager _replayManager;

		private List<TCard> _deck = new List<TCard>();
		private List<TCard> _hand = new List<TCard>();
		private List<TCard> _discardPile = new List<TCard>();
		#endregion

		#region Constructors
		public Deck(Board<TPiece, TTile> board, Grid<TTile> grid, ReplayManager replayManager)
		{
			_board = board;
			_grid = grid;
			_replayManager = replayManager;
		}
		#endregion

		#region Methods
		public void Register(TCard card)
		{
			_deck.Add(card);
			card.Initialize(_board, _grid);
		}

		public void FillHand()
		{
			for (int i = 0; i < 5; i++)
				DrawCard();
		}

		public void PlayCard(TCard card, TPiece piece, TTile tile)
		{
			if(card.CanExecute(tile))
			{
				card.Execute(piece, tile, out Action forward, out Action backward);
				TCard nextCard = NextCardToDraw();

				bool cardDrawn = _deck.Count > 0;
				TCard drawnCard = null;
				if(cardDrawn)
					drawnCard = _deck[0];

				forward += ()	=>	
				{
					MoveCardBetweenLists(card, _hand, _discardPile);
					card.gameObject.SetActive(false);

					if (_deck.Count > 0)
					{
						TCard newCard = _deck[0];
						MoveCardBetweenLists(newCard, _deck, _hand);
						newCard.gameObject.SetActive(true);
					}
				};

				backward += () =>
				{
					if(cardDrawn)
					{
						MoveCardBetweenLists(drawnCard, _hand, _deck);
						drawnCard.gameObject.SetActive(false);
					}

					MoveCardBetweenLists(card, _discardPile, _hand);
					card.gameObject.SetActive(true);
				};

				_replayManager.Execute(new DelegateReplayCommand(forward, backward));
			}
		}

		public void OnCardPlayed(CardEventArgs<TCard> eventArgs)
		{
			EventHandler<CardEventArgs<TCard>> handler = CardPlayed;
			handler?.Invoke(this, eventArgs);
		}

		private TCard NextCardToDraw()
		{
			foreach(TCard card in _deck)
			{
				if (!card.gameObject.activeInHierarchy)
				{
					return card;
				}
			}

			return null;
		}

		private void DrawCard()
		{
			if (_deck.Count == 0) return;

			TCard card = _deck[0];
			MoveCardBetweenLists(card, _deck, _hand);

			card.gameObject.SetActive(true);
		}

		private void MoveCardBetweenLists(TCard card, List<TCard> startList, List<TCard> endList)
		{
			if (startList.Remove(card))
				endList.Insert(0,card);
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
