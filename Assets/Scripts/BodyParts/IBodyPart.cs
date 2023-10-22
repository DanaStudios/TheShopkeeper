using Items;

namespace BodyParts
{
	public interface IBodyPart
	{
		BodyPartType BodyPartType { get; }
		IItem EquippedItem { get; }
		void EquipItem(IItem item);
	}
}