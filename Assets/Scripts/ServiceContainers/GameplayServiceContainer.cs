using Animators;
using AppData.Player;
using AppData.Shopkeeper;
using Interactors;
using Physics.Bodies;
using Player;
using Screens;
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
		[SerializeField] private PlayerBehaviour playerPrefab;

		[Header("Player Interaction")] 
		[SerializeField] private float playerInteractionRadius;
		
		[Header("Shopkeeper")]
		[SerializeField] private ShopkeeperData shopkeeperData;
		[SerializeField] private ShopkeeperBehaviour shopkeeperPrefab;
		[SerializeField] private UIDocument shopScreenPrefab;
		[SerializeField] private int shopkeeperCapacity;
		[SerializeField] private Item.Item[] shopkeeperItems;
		
		private PlayerBehaviour player;

		private void Awake()
		{
			InitializePlayer();
			InitializeShopkeeper();
		}

		private void InitializePlayer()
		{
			player = Instantiate(playerPrefab);
			var rb = player.transform.GetComponentInChildren<Rigidbody2D>();
			var animator = player.transform.GetComponentInChildren<Animator>();
			var playerBody = new PlayerBody(rb);
			var playerAnimator = new CharacterAnimator(animator);
			var interactor = InitializePlayerInteractor();
			player.Inject(playerData, playerBody, playerAnimator, interactor);
		}
		
		private IInteractor InitializePlayerInteractor()
		{
			var interactObject = new GameObject("Interactor");
			var col = interactObject.AddComponent<CircleCollider2D>();
			var playerInteractBehaviour = interactObject.transform.AddComponent<PlayerInteractBehaviour>();
			col.radius = playerInteractionRadius;
			col.isTrigger = true;
			playerInteractBehaviour.transform.SetParent(player.transform);
			playerInteractBehaviour.Inject(player, col);
			return playerInteractBehaviour;
		}
		
		private void InitializeShopkeeper()
		{
			var shopkeeper = Instantiate(shopkeeperPrefab);
			var uiDocument = Instantiate(shopScreenPrefab);
			var shopScreen = new ShopScreen(uiDocument);
			var inventory = new Inventory.Inventory(shopkeeperCapacity, shopkeeperItems);
			shopkeeper.Inject(inventory, shopkeeperData, shopScreen);
		}
	}
}