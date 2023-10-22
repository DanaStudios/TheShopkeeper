using AppData.Player;
using Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Screens.Player
{
	public class PlayerHUD : IPlayerHUD
	{
		public bool Visible => RootVisualElement.style.display == DisplayStyle.Flex;
		public void Show() => RootVisualElement.style.display = DisplayStyle.Flex;
		public void Hide() => RootVisualElement.style.display = DisplayStyle.None;
		private VisualElement RootVisualElement => UIDocument.rootVisualElement;
		private UIDocument UIDocument { get; }
		
		public PlayerHUD(UIDocument uiDocument, IPlayerData playerData)
		{
			UIDocument = uiDocument;
			InitializeAsync(playerData);
		}
		
		public async void UpdateGoldAmount(int amount)
		{
			await new WaitUntil(() => RootVisualElement != null);
			RootVisualElement.Find<Label>("GoldLabel").text = $"{amount}g";
		}
		
		private async void InitializeAsync(IPlayerData playerData)
		{
			await new WaitUntil(() => RootVisualElement != null);
			RootVisualElement.Find<Label>("PlayerName").text = playerData.Name;
			RootVisualElement.Find<Image>("ProfilePicture").image = playerData.ProfilePicture;
			RootVisualElement.SetPickingModeRecursively(PickingMode.Ignore);
			UpdateGoldAmount(playerData.StartingGold);
			Show();
		}
	}
}