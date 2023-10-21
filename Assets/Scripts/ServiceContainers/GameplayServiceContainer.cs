using Animators;
using AppData.Player;
using AppData.Shopkeeper;
using Interactions;
using Physics.Bodies;
using Screens.Player;
using Screens.Shop;
using Shopkeeper;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace ServiceContainers
{
	[DefaultExecutionOrder(-9999)]
	public class GameplayServiceContainer : MonoBehaviour
	{
		[Header("Player")]
		[SerializeField] private PlayerData playerData;
		[SerializeField] private Player.PlayerBehaviour playerPrefab;
		[SerializeField] private UIDocument playerHUDPrefab;
		[SerializeField] private float playerInteractionRadius;
		[SerializeField] private int playerItemCapacity;
		
		[Header("Shopkeeper")]
		[SerializeField] private ShopkeeperData shopkeeperData;
		[SerializeField] private ShopkeeperBehaviour shopkeeperPrefab;
		[SerializeField] private UIDocument shopScreenPrefab;
		[SerializeField] private int shopkeeperCapacity;
		[SerializeField] private Item.Item[] shopkeeperItems;
		
		private Player.PlayerBehaviour playerBehaviour;
		private ShopkeeperBehaviour shopkeeper;

		private void Awake()
		{
			InitializePlayer();
			InitializeShopkeeper();
		}

		private void InitializePlayer()
		{
			playerBehaviour = Instantiate(playerPrefab);
			var rb = playerBehaviour.transform.GetComponentInChildren<Rigidbody2D>();
			var animator = playerBehaviour.transform.GetComponentInChildren<Animator>();
			var playerBody = new PlayerBody(rb);
			var playerAnimator = new CharacterAnimator(animator);
			var playerWallet = new Wallet.Wallet(playerData.StartingGold);
			var playerInteractBehaviour = InitializePlayerInteractor();
			var playerInventory = new Inventory.Inventory(playerItemCapacity, null);
			var uiDocument = Instantiate(playerHUDPrefab);
			var playerHUD = new PlayerHUD(uiDocument, playerData);
			playerBehaviour.Inject(playerData, playerBody, playerAnimator, playerWallet, playerInventory, 
				playerInteractBehaviour, playerHUD);
		}
		
		private PlayerInteractionBehaviour InitializePlayerInteractor()
		{
			var interactObject = new GameObject("Interactor");
			var playerInteractBehaviour = interactObject.transform.AddComponent<PlayerInteractionBehaviour>();
			playerInteractBehaviour.transform.SetParent(playerBehaviour.transform);
			playerInteractBehaviour.Inject(playerBehaviour, playerInteractionRadius);
			return playerInteractBehaviour;
		}
		
		private void InitializeShopkeeper()
		{
			shopkeeper = Instantiate(shopkeeperPrefab);
			var uiDocument = Instantiate(shopScreenPrefab);
			var shopScreen = new ShopScreen(uiDocument);
			var inventory = new Inventory.Inventory(shopkeeperCapacity, shopkeeperItems);
			var wallet = new Wallet.Wallet(shopkeeperData.StartingGold);
			shopkeeper.Inject(shopkeeperData, inventory, wallet, shopScreen);
		}
	}
}