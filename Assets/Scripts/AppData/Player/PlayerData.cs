using System;
using UnityEngine;

namespace AppData.Player
{
	[Serializable]
	public class PlayerData : IPlayerData
	{
		[field: SerializeField] public float MoveSpeed { get; private set; }
	}
}