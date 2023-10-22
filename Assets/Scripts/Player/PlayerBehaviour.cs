using System;
using Animators;
using AppData.Player;
using Commands;
using Interactions;
using Inventory;
using Items;
using Physics.Bodies;
using Screens.Inventory;
using Screens.Player;
using UnityEngine;
using Wallets;

namespace Player
{
	public class PlayerBehaviour : MonoBehaviour, IPlayer
	{
		private IPlayerData playerData;
		private IBody playerBody;
		private IAnimator playerAnimator;
		private IWallet playerWallet;
		private IInventory playerInventory;
		private IPlayerInteraction playerInteraction;
		private IPlayerHUD playerHUD;
		private IInventoryScreen playerInventoryScreen;
		private Vector2 movementDirection;

		public bool PressedInteract => Input.GetKeyDown(KeyCode.E) && !playerInventoryScreen.Visible;
		public bool PressedInventory => Input.GetKeyDown(KeyCode.I);

		public void Inject(IPlayerData data, IBody body, IAnimator animator, IWallet wallet, IInventory inventory,
			IPlayerInteraction interaction, IInventoryScreen inventoryScreen, IPlayerHUD hud)
		{
			playerData = data;
			playerBody = body;
			playerAnimator = animator;
			playerWallet = wallet;
			playerInventory = inventory;
			playerInteraction = interaction;
			playerInventoryScreen = inventoryScreen;
			playerHUD = hud;
			
			playerWallet.AmountUpdated += OnWalletUpdated;
			playerInventory.Updated += OnInventoryUpdated;
		}

		public bool CanAfford(int totalCost) => playerWallet?.CurrentGold >= totalCost;
		public void ReceiveItems(IItem[] items) => playerInventory.AddItems(items);
		public IItem[] GetItems(IItem item, int count) => playerInventory.GetItems(item, count);
		public void Spend(int gold) => playerWallet.Subtract(gold);
		public void Earn(int gold) => playerWallet.Add(gold);

		private void Update() => ProcessInput();
		private void FixedUpdate() => Move();
		private void LateUpdate() => UpdateAnimation();

		private void ProcessInput()
		{
			var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");
			var isInteracting = playerInteraction.IsInteracting;
			
			if (PressedInventory) ToggleInventoryScreen();

			if (isInteracting || playerInventoryScreen.Visible)
			{
				movementDirection = Vector2.zero;
				return;
			}
			
			movementDirection = new Vector2(horizontal, vertical);
		}

		private void Move() => playerBody.SetVelocity(movementDirection * playerData.MoveSpeed);

		private void UpdateAnimation()
		{
			var animHash = playerBody.Velocity == Vector2.zero ? "Idle" : "Walk";
			playerAnimator.PlayAnimation(Animator.StringToHash(animHash));
		}
		
		private void ToggleInventoryScreen()
		{
			if (playerInventoryScreen.Visible)
			{
				playerInventoryScreen.Hide();
				return;
			}
			
			OnInventoryUpdated();
			playerInventoryScreen.Show();
		}
		
		private void OnInventoryUpdated()
		{
			if (playerInteraction.IsInteracting)
			{
				playerInventoryScreen.UpdateItemList(playerData, playerInventory.Items, sellClicked: OnSellClicked);
			}
			else
			{
				playerInventoryScreen.UpdateItemList(playerData, playerInventory.Items, useClicked: OnUseClicked);
			}
		}
		
		private void OnWalletUpdated(int newAmount)
		{
			playerHUD.UpdateGoldAmount(newAmount);
		}
		
		private void OnSellClicked(IItem item, int count)
		{
			var shopkeeper = playerInteraction.LastInteracted;
			if (shopkeeper == null) throw new Exception("[PlayerBehaviour] Shopkeeper not found!");
			
			ICommand sellCommand = new SellCommand(this, shopkeeper, item, count);
			sellCommand.Execute();
		}

		private void OnUseClicked(IItem item, int count)
		{
			throw new NotImplementedException();
		}
	}
}