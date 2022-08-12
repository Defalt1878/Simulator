using System;
using System.Collections.Generic;
using System.Linq;
using Notifications;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Windows.Miner
{
	public class GameField : MonoBehaviour
	{
		[SerializeField] private PopUpNotification notification;
		public ConnectionScreen ConnectionScreen { get; set; }
		private const int FieldSize = 7;
		private int _targetsAmount;
		private GameCell[,] _field;
		private Vector2Int _start;
		private Vector2Int _exit;
		private HashSet<Vector2Int> _canBeSelected;
		private int _bestPathLength;
		private bool _isDragging;
		private int _currentLength;
		private List<Vector2Int> _currentPath;

		public void StartGame(int targetsAmount)
		{
			_targetsAmount = targetsAmount;
			_canBeSelected = new HashSet<Vector2Int>();
			while (true)
			{
				GenerateGameField();
				ResetGame();
				if (PathFinder.FindBestPathLength(_field, _start, _exit, out _bestPathLength))
					break;
			}
		}

		private void GenerateGameField()
		{
			_field = new GameCell[FieldSize, FieldSize];
			for (var i = 0; i < transform.childCount; i++)
			{
				var cell = transform.GetChild(i).GetComponent<GameCell>();
				var position = new Vector2Int(i % FieldSize, i / FieldSize);
				cell.Type = default;
				cell.CurrentField = this;
				cell.Position = position;
				_field[position.x, position.y] = cell;
			}

			_start = GetRandomDefaultPos(100);
			_field[_start.x, _start.y].Type = CellType.Start;
			_exit = GetRandomDefaultPos(100);
			_field[_exit.x, _exit.y].Type = CellType.Exit;
			for (var i = 0; i < _targetsAmount; i++)
			{
				var targetPos = GetRandomDefaultPos(100);
				_field[targetPos.x, targetPos.y].Type = CellType.Target;
			}
		}

		private void ResetGame()
		{
			_currentPath = new List<Vector2Int>();
			_currentLength = 0;
			foreach (var cell in _field)
				cell.Color = cell.Type is CellType.Start
					? CellColor.CanBeSelected
					: CellColor.DefaultColor;
		}

		private Vector2Int GetRandomDefaultPos(int tryCount)
		{
			for (var i = 0; i < tryCount; i++)
			{
				var pos = new Vector2Int(Random.Range(0, FieldSize), Random.Range(0, FieldSize));
				if (_field[pos.x, pos.y].Type == CellType.Default)
					return pos;
			}

			throw new Exception("Can't find empty cell!");
		}

		private void OnMouseUp() => _isDragging = false;

		public void CellMouseDown(GameCell cell)
		{
			if (cell.Type is not CellType.Start && !_canBeSelected.Contains(cell.Position))
				return;
			if (cell.Type is CellType.Start)
			{
				ResetGame();
				_currentPath.Add(cell.Position);
				_canBeSelected.Add(cell.Position);
			}

			_isDragging = true;
			CellMouseEnter(cell);
		}

		public void CellMouseEnter(GameCell cell)
		{
			if (!_isDragging || !_canBeSelected.Contains(cell.Position))
				return;
			cell.Color = CellColor.SelectedColor;
			_currentLength++;

			if (cell.Type == CellType.Exit)
			{
				_currentPath.Add(cell.Position);
				_canBeSelected.Remove(cell.Position);
				foreach (var pos in _canBeSelected)
					_field[pos.x, pos.y].Color = CellColor.DefaultColor;
				_canBeSelected.Clear();
				return;
			}

			if (cell.Type == CellType.Target)
				_currentPath.Add(cell.Position);

			UpdateCanBeSelected(cell.Position);
		}

		private void UpdateCanBeSelected(Vector2Int current)
		{
			_canBeSelected.Remove(current);
			foreach (var pos in _canBeSelected)
				_field[pos.x, pos.y].Color = CellColor.DefaultColor;
			_canBeSelected.Clear();
			foreach (var cell in
			         PathFinder.GetNeighbours4(current, FieldSize)
				         .Select(p => _field[p.x, p.y])
				         .Where(c => c.Color == CellColor.DefaultColor))
			{
				_canBeSelected.Add(cell.Position);
				cell.Color = CellColor.CanBeSelected;
			}
		}

		public void CheckSolution()
		{
			if (_currentPath.LastOrDefault() != _exit)
			{
				notification.Appear("Exit server not found!", NotificationType.Error);
				return;
			}

			if (_currentPath.Count - 2 != _targetsAmount)
			{
				notification.Appear("Not all servers connected!", NotificationType.Error);
				return;
			}

			if (_currentLength > _bestPathLength)
			{
				notification.Appear("Not optimal connection!", NotificationType.Error);
				return;
			}

			ConnectionScreen.GameFinished();
		}
	}
}