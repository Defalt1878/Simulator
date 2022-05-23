using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Windows.Miner
{
	public class GameField : MonoBehaviour
	{
		private const int FieldSize = 7;
		private const int TargetsAmount = 5;
		private static readonly Vector2Int Start = new(0, 0);
		private static readonly Vector2Int Exit = new(FieldSize - 1, FieldSize - 1);
		private int _bestPathLength;
		private GameCell[,] _field;
		private bool _isDragging;
		private int _length;
		private List<Vector2Int> _path;

		public void DragStop() => _isDragging = false;

		private void OnMouseUp()
		{
			_isDragging = false;
		}

		public void CellMouseDown(GameCell cell)
		{
			if (cell.Position != Start)
				return;
			ResetGame();
			_isDragging = true;
			cell.Color = CellColors.SelectedColor;
			_length++;
			_path.Add(cell.Position);
		}

		public void CellMouseEnter(GameCell cell)
		{
			if (!_isDragging)
				return;
			cell.Color = CellColors.SelectedColor;
			_length++;
			if (cell.IsTarget || cell.Position == Exit)
				_path.Add(cell.Position);
		}

		private void ResetGame()
		{
			_path = new List<Vector2Int>();
			_length = 0;
			foreach (var cell in _field)
				cell.Color = CellColors.DefaultColor;
			_field[Start.x, Start.y].Color = CellColors.Start;
			_field[Exit.x, Exit.y].Color = CellColors.Exit;
		}

		public void CheckSolution()
		{
			if (_path.LastOrDefault() != Exit)
			{
				Debug.Log("Exit not found!");
				return;
			}
			var targetsCollected = _path.Count - 2;

			if (targetsCollected != TargetsAmount)
			{
				Debug.Log("Not all targets collected!");
				return;
			}

			if (_length > _bestPathLength)
			{
				Debug.Log("Not optimal path!");
				return;
			}

			Debug.Log("Connection successful!");
		}

		private void Awake()
		{
			GenerateGameField();
			ResetGame();
			_bestPathLength = PathFinder.FindBestPathLength(_field, Start, Exit);
		}

		private void GenerateGameField()
		{
			_field = new GameCell[FieldSize, FieldSize];
			for (var i = 0; i < transform.childCount; i++)
			{
				var cell = transform.GetChild(i).GetComponent<GameCell>();
				var position = new Vector2Int(i % FieldSize, i / FieldSize);
				cell.IsTarget = false;
				cell.CurrentField = this;
				cell.Position = position;
				_field[position.x, position.y] = cell;
			}

			var targetsLeft = TargetsAmount;
			while (targetsLeft > 0)
			{
				var targetPos = new Vector2Int(Random.Range(0, FieldSize), Random.Range(0, FieldSize));
				if (targetPos == Start || targetPos == Exit)
					continue;
				if (_field[targetPos.x, targetPos.y].IsTarget)
					continue;
				_field[targetPos.x, targetPos.y].IsTarget = true;
				targetsLeft--;
			}
		}
	}
}