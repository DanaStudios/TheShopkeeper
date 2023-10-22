using System;
using UnityEngine;

namespace AppData.Player
{
	[Serializable]
	public class PlayerData : IPlayerData
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public float MoveSpeed { get; private set; }
		[field: SerializeField] public Texture2D ProfilePicture { get; private set; }
		[field: SerializeField] public string DialogueText { get; private set; }
		[field: SerializeField] public int StartingGold { get; private set; }
	}
}