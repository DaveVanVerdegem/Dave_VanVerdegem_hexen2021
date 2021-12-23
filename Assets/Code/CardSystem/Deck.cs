using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.CardSystem
{
	public class Deck<TCard, TTile> where TCard : MonoBehaviour
	{
		#region Properties
		public event EventHandler<CardEventArgs<TCard>> CardPlayed;
		#endregion

		#region Fields
		private List<TCard> _cards = new List<TCard>();
		#endregion

		#region Methods
		public void Register(TCard card)
		{
			_cards.Add(card);
			card.gameObject.SetActive(false);
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

		public void PlayCard(TCard card, TTile tile)
		{

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
