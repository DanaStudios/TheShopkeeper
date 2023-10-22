using UnityEngine;

namespace Extensions
{
	public static class PhysicsExtensions
	{
		public static T FindClosest<T>(this Transform t, float radius, Collider2D[] colliders)
		{
			var count = Physics2D.OverlapCircleNonAlloc(t.position, radius, colliders);

			var closest = default(T);
			var closestDistance = float.MaxValue;
			
			for (var i = 0; i < count; i++)
			{
				if (!colliders[i].TryGetComponent<T>(out var component)) continue;
				var distance = Vector2.Distance(t.position, colliders[i].transform.position);
				if (!(distance < closestDistance)) continue;
				closestDistance = distance;
				closest = component;
			}
    
			return closest;
		}
	}
}