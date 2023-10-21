using System;
using UnityEngine;

namespace Wallet
{
	public class Wallet : IWallet
	{
		public event Action<int> AmountUpdated;

		public int CurrentGold
		{
			get => currentGold;
			private set
			{
				currentGold = value;
				AmountUpdated?.Invoke(currentGold);
			}
		}
		
		private int currentGold;
		
		public Wallet(int startingAmount) => CurrentGold = startingAmount;
		public void Add(int amount) => CurrentGold += amount;
		public int Subtract(int amount) => CurrentGold = Mathf.Max(CurrentGold - amount, 0);
	}
}