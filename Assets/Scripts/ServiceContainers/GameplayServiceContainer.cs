using System.Linq;
using Animators;
using AppData.Player;
using AppData.Shopkeeper;
using BodyParts;
using Factories.Items;
using Interactions;
using Items;
using Physics.Bodies;
using Screens.Inventory;
using Screens.Player;
using Shopkeeper;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Wallets;

namespace ServiceContainers
{
	[DefaultExecutionOrder(-9999)]
	public class GameplayServiceContainer : MonoBehaviour
	{
		[Header("Player")]
		[SerializeField] private PlayerData playerData;
		[SerializeField] private Player.PlayerBehaviour playerPrefab;
		[SerializeField] private UIDocument playerHUDPrefab;
		[SerializeField] private UIDocument playerInventoryPrefab;
		[SerializeField] private float playerInteractionRadius;
		[SerializeField] private int playerItemCapacity;
		
		[Header("Player Default Items")]
		[SerializeField] private Item defaultHeadItem;
		[SerializeField] private Item defaultTorsoItem;
		[SerializeField] private Item defaultLegsItem;
		[SerializeField] private Item defaultFeetItem;
		[SerializeField] private Vector2 bodyPartRelativePos;
		[SerializeField] private string sortingLayerName;
		
		[Header("Shopkeeper")]
		[SerializeField] private ShopkeeperData shopkeeperData;
		[SerializeField] private ShopkeeperBehaviour shopkeeperPrefab;
		[SerializeField] private UIDocument shopScreenPrefab;
		[SerializeField] private int shopkeeperCapacity;
		[SerializeField] private Item[] shopkeeperItems;
		
		private Player.PlayerBehaviour playerBehaviour;

		private void Awake()
		{
			InitializePlayer();
			
			var startPos = shopkeeperPrefab.transform.position;
			InitializeShopkeeper(startPos);
			InitializeShopkeeper(startPos * -1.5f);
		}

		private void InitializePlayer()
		{
			playerBehaviour = Instantiate(playerPrefab);
			var playerTransform = playerBehaviour.transform;
			var rb = playerTransform.GetComponentInChildren<Rigidbody2D>();
			var animator = playerTransform.GetComponentInChildren<Animator>();
			var playerBody = new PlayerBody(rb);
			var playerAnimator = new CharacterAnimator(animator);
			var playerWallet = new Wallet(playerData.StartingGold);
			var playerInteractBehaviour = InitializePlayerInteractor();
			var headItem = new ItemFactory(defaultHeadItem).Create();
			var torsoItem = new ItemFactory(defaultTorsoItem).Create();
			var legsItem = new ItemFactory(defaultLegsItem).Create();
			var feetItem = new ItemFactory(defaultFeetItem).Create();
			var playerItems = new [] { headItem, torsoItem, legsItem, feetItem };
			var playerInventory = new Inventory.Inventory(playerItemCapacity, playerItems);
			var playerHUD = new PlayerHUD(Instantiate(playerHUDPrefab), playerData);
			var playerInventoryScreen = new InventoryScreen(Instantiate(playerInventoryPrefab));
			var playerBodyParts = new IBodyPart[]
			{
				new BodyPart(BodyPartType.Head, CreateRenderer("Head", animator.transform), headItem),
				new BodyPart(BodyPartType.Torso, CreateRenderer("Torso", animator.transform), torsoItem),
				new BodyPart(BodyPartType.Legs, CreateRenderer("Legs", animator.transform), legsItem),
				new BodyPart(BodyPartType.Feet, CreateRenderer("Feet", animator.transform), feetItem)
			};
			
			playerBehaviour.Inject(playerData, playerBody, playerBodyParts, playerAnimator, playerWallet, 
				playerInventory, playerInteractBehaviour, playerInventoryScreen, playerHUD);
		}
		
		private PlayerInteractionBehaviour InitializePlayerInteractor()
		{
			var interactObject = new GameObject("Interactor");
			var playerInteractBehaviour = interactObject.transform.AddComponent<PlayerInteractionBehaviour>();
			playerInteractBehaviour.transform.SetParent(playerBehaviour.transform);
			playerInteractBehaviour.Inject(playerBehaviour, playerInteractionRadius);
			return playerInteractBehaviour;
		}

		private SpriteRenderer CreateRenderer(string objectName, Transform parent)
		{
			var newObject = new GameObject(objectName);
			var newRenderer = newObject.transform.AddComponent<SpriteRenderer>();
			newObject.transform.SetParent(parent);
			newObject.transform.localPosition = bodyPartRelativePos;
			newRenderer.sortingLayerName = sortingLayerName;
			return newRenderer;
		}
		
		private void InitializeShopkeeper(Vector3 position)
		{
			var shopkeeper = Instantiate(shopkeeperPrefab, position, default, null);
			var uiDocument = Instantiate(shopScreenPrefab);
			var shopScreen = new InventoryScreen(uiDocument);
			var items = shopkeeperItems.Select(item => new ItemFactory(item).Create()).ToArray();
			var inventory = new Inventory.Inventory(shopkeeperCapacity, items);
			var wallet = new Wallet(shopkeeperData.StartingGold);
			shopkeeper.Inject(shopkeeperData, inventory, wallet, shopScreen);
		}
	}
}