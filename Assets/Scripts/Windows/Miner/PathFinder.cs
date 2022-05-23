using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Windows.Miner
{
	public static class PathFinder
	{
		public static int FindBestPathLength(GameCell[,] field, Vector2Int start, Vector2Int exit)
		{
			var points = field
				.Cast<GameCell>()
				.Where(cell => cell.IsTarget)
				.Select(cell => cell.Position)
				.Concat(new[] {start, exit})
				.ToHashSet();
			var lengthDictionary = GetLengthDictionary(points, field.GetLength(0));
			var queue = new Queue<(Vector2Int pos, HashSet<Vector2Int> visited, int length)>();
			queue.Enqueue((start, new HashSet<Vector2Int> {start}, 1));
			var bestLength = int.MaxValue;
			while (queue.Any())
			{
				var (pos, visited, length) = queue.Dequeue();
				if (pos == exit)
				{
					if (visited.Count != points.Count)
						continue;
					if (length < bestLength)
						bestLength = length;
				}

				foreach (var newPos in points.Where(p => !visited.Contains(p)))
				{
					var newLength = lengthDictionary[(pos, newPos)];
					queue.Enqueue((newPos, new HashSet<Vector2Int>(visited) {newPos}, length + newLength));
				}
			}

			return bestLength;
		}

		private static Dictionary<(Vector2Int start, Vector2Int end), int> GetLengthDictionary(
			IEnumerable<Vector2Int> targets,
			int fieldSize
		)
		{
			var dictionary = new Dictionary<(Vector2Int start, Vector2Int end), int>();
			var points = targets.ToArray();
			for (var i = 1; i < points.Length; i++)
			{
				var target1 = points[i - 1];
				foreach (var (target2, length) in GetPathLengths(target1, points[i..], fieldSize))
				{
					dictionary[(target1, target2)] = length;
					dictionary[(target2, target1)] = length;
				}
			}

			return dictionary;
		}

		private static IEnumerable<(Vector2Int end, int length)> GetPathLengths(
			Vector2Int start,
			IEnumerable<Vector2Int> targets,
			int fieldSize
		)
		{
			var queue = new Queue<(Vector2Int pos, int length)>();
			queue.Enqueue((start, 0));
			var visited = new HashSet<Vector2Int> {start};
			var targetsSet = targets.ToHashSet();
			while (queue.Any())
			{
				var (pos, length) = queue.Dequeue();
				visited.Add(pos);
				if (targetsSet.Contains(pos))
				{
					yield return (pos, length);
					targetsSet.Remove(pos);
					if (targetsSet.Count == 0)
						yield break;
				}

				foreach (var neighbour in GetNeighbours4(pos, fieldSize).Where(v => !visited.Contains(v)))
					queue.Enqueue((neighbour, length + 1));
			}
		}

		private static IEnumerable<Vector2Int> GetNeighbours4(Vector2Int current, int fieldSize)
		{
			for (var dx = -1; dx <= 1; dx++)
			for (var dy = -1; dy <= 1; dy++)
				if (dx == 0 ^ dy == 0)
				{
					var newX = current.x + dx;
					var newY = current.y + dy;
					if (newX >= 0 && newX < fieldSize && newY >= 0 && newY < fieldSize)
						yield return new Vector2Int(newX, newY);
				}
		}
	}
}