using System;
using Animators;
using AppData.Player;
using Interactions;
using Inventory;
using Item;
using Physics.Bodies;
using Screens.Player;
using UnityEngine;
using Wallet;

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
		private Vector2 movementDirection;
		
		public bool PressedInteract => Input.GetKeyDown(KeyCode.E);

		public void Inject(IPlayerData data, IBody body, IAnimator animator, IWallet wallet, IInventory inventory,
			IPlayerInteraction interaction, IPlayerHUD hud)
		{
			playerData = data;
			playerBody = body;
			playerAnimator = animator;
			playerWallet = wallet;
			playerWallet.AmountUpdated += OnWalletUpdated;
			playerInventory = inventory;
			playerInteraction = interaction;
			playerHUD = hud;
		}

		public bool CanAfford(int totalCost) => playerWallet?.CurrentGold >= totalCost;
		public void Receive(IItem[] items) => playerInventory.AddItems(items);
		public void Spend(int gold) => playerWallet.Subtract(gold);

		public void SellTo(IItem item, int count, IInventory inventory)
		{
			throw new NotImplementedException();
		}

		private void Update() => ProcessInput();
		private void FixedUpdate() => Move();
		private void LateUpdate() => UpdateAnimation();

		private void ProcessInput()
		{
			movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		}

		private void Move()
		{
			if (playerInteraction.IsInteracting) return;
			playerBody.SetVelocity(movementDirection * playerData.MoveSpeed);
		}

		private void UpdateAnimation()
		{
			var animHash = playerBody.Velocity == Vector2.zero ? "Idle" : "Walk";
			playerAnimator.PlayAnimation(Animator.StringToHash(animHash));
		}
		
		private void OnWalletUpdated(int newAmount)
		{
			playerHUD.UpdateGoldAmount(newAmount);
		}
	}
}