using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Windows.Miner
{
	public static class PathFinder
	{
		public static bool FindBestPathLength(GameCell[,] field, Vector2Int start, Vector2Int exit, out int length)
		{
			length = -1;
			var fieldSize = field.GetLength(0);
			var targets = field
				.Cast<GameCell>()
				.Where(cell => cell.Type == CellType.Target)
				.Select(cell => cell.Position)
				.ToHashSet();

			var queue = new Queue<(HashSet<Vector2Int> visited, Vector2Int pos, int targetsCollected)>();
			queue.Enqueue((new HashSet<Vector2Int> {start}, start, 0));
			while (queue.Any())
			{
				var (visited, pos, targetsCollected) = queue.Dequeue();
				if (pos == exit)
				{
					if (targetsCollected != targets.Count)
						continue;
					length = visited.Count;
					return true;
				}

				foreach (var newPos in GetNeighbours4(pos, fieldSize).Except(visited))
				{
					if (targets.Contains(newPos))
					{
						queue.Clear();
						queue.Enqueue((new HashSet<Vector2Int>(visited) {newPos}, newPos, targetsCollected + 1));
						break;
					}

					queue.Enqueue((new HashSet<Vector2Int>(visited) {newPos}, newPos, targetsCollected));
				}
			}

			return false;
		}

		public static IEnumerable<Vector2Int> GetNeighbours4(Vector2Int current, int fieldSize)
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