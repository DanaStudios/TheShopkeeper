using UnityEngine;

namespace Items
{
	public interface IItem
	{
		ItemType ItemType { get; }
		string Name { get; }
		string Description { get; }
		Texture2D Icon { get; }
		int Cost { get; }
		int MaxStackCount { get; }
	}
}