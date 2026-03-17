using Microsoft.AspNetCore.Components;
using System.Numerics;

namespace Blazor.Components.Pages
{
	public partial class RabbitPyramid
	{
		private int levels = 4; // Количество поколений

		// Модель узла дерева кроликов
		public class RabbitNode
		{
			public int Level { get; set; }
			public RabbitNode? Left { get; set; }  // Потомки
			public RabbitNode? Right { get; set; } // Взрослая пара
		}

		private RabbitNode BuildTree(int currentLevel, int maxLevel)
		{
			var node = new RabbitNode { Level = currentLevel };
			if (currentLevel < maxLevel)
			{
				node.Left = BuildTree(currentLevel + 1, maxLevel);
				// По правилу Фибоначчи, пара кроликов живет и размножается
				if (currentLevel < maxLevel - 1)
					node.Right = BuildTree(currentLevel + 1, maxLevel);
			}
			return node;
		}
	}
}
