using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.SelectionSystem
{
	public class SelectionManager<TSelectableItem>
	{
		#region Properties
		public event EventHandler<SelectionEventArgs<TSelectableItem>> Selected;
		public event EventHandler<SelectionEventArgs<TSelectableItem>> Deselected;

		public TSelectableItem SelectedItem => _selectedItems.First();

		public bool HasSelection => _selectedItems.Count > 0;
		public bool IsSelected(TSelectableItem selectableItem)
			=> _selectedItems.Contains(selectableItem); 
		#endregion

		#region Fields
		private HashSet<TSelectableItem> _selectedItems = new HashSet<TSelectableItem>();
		#endregion

		#region Methods
		public bool Select(TSelectableItem selectableItem)
		{
			if (_selectedItems.Add(selectableItem))
			{
				OnSelected(new SelectionEventArgs<TSelectableItem>(selectableItem));
				return true;
			}

			return false;
		}

		public bool Deselect(TSelectableItem selectableItem)
		{
			if (_selectedItems.Remove(selectableItem))
			{
				OnDeselected(new SelectionEventArgs<TSelectableItem>(selectableItem));
				return true;
			}

			return false;
		}

		public bool Toggle(TSelectableItem selectableItem)
		{
			if (IsSelected(selectableItem))
				return !Deselect(selectableItem);
			else
				return Select(selectableItem);
		}

		public void DeselectAll()
		{
			foreach (TSelectableItem selectable in _selectedItems.ToList())
			{
				Deselect(selectable);
			}
		}

		protected virtual void OnSelected(SelectionEventArgs<TSelectableItem> eventArgs)
		{
			Debug.Log($"Selected {eventArgs.SelectedItem}");
			EventHandler<SelectionEventArgs<TSelectableItem>> handler = Selected;
			handler?.Invoke(this, eventArgs);
		}

		protected virtual void OnDeselected(SelectionEventArgs<TSelectableItem> eventArgs)
		{
			EventHandler<SelectionEventArgs<TSelectableItem>> handler = Deselected;
			handler?.Invoke(this, eventArgs);
		} 
		#endregion
	}

	#region EventArgs
	public class SelectionEventArgs<TSelectedItem> : EventArgs
	{
		public TSelectedItem SelectedItem { get; }

		public SelectionEventArgs(TSelectedItem selectedItem)
		{
			SelectedItem = selectedItem;
		}
	}
	#endregion
}

