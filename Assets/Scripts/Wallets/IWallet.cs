using System;

namespace Wallets
{
	public interface IWallet
	{
		event Action<int> AmountUpdated;
		int CurrentGold { get; }
		void Add(int amount);
		int Subtract(int amount);
	}
}