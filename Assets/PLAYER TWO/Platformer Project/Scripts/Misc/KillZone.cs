using UnityEngine;

namespace PLAYERTWO.PlatformerProject
{
	[RequireComponent(typeof(Collider))]
	[AddComponentMenu("PLAYER TWO/Platformer Project/Misc/Kill Zone")]
	public class KillZone : MonoBehaviour
	{
		protected Collider m_collider;

		protected virtual void Start()
		{
			m_collider = GetComponent<Collider>();
			m_collider.isTrigger = true;
		}

		protected virtual void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(GameTags.Player))
			{
				if (other.TryGetComponent(out Player player))
				{
					player.Die();
				}
			}
		}
	}
}
