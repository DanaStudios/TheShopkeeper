using UnityEngine;

namespace Physics.Bodies
{
	public interface IBody
	{
		Vector2 Velocity { get; }
		void SetVelocity(Vector2 velocity);
	}
}