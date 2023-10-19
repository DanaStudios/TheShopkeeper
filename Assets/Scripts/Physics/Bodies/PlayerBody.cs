using UnityEngine;

namespace Physics.Bodies
{
	public class PlayerBody : IBody
	{
		public Vector2 Velocity => rb.velocity;
		private readonly Rigidbody2D rb;
		public PlayerBody(Rigidbody2D rb) => this.rb = rb;
		public void SetVelocity(Vector2 velocity) => rb.velocity = velocity;
	}
}