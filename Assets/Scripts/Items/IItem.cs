using BodyParts;
using UnityEngine;

namespace Items
{
	public interface IItem
	{
		BodyPartType BodyPartType { get; }
		string Name { get; }
		string Description { get; }
		Sprite Sprite { get; }
		int Cost { get; }
		int MaxStackCount { get; }
		
		// TODO: Only equipments should have this property
		bool Equipped { get; set; }
	}
}