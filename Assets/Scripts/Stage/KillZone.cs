#region

using Game.Player;
using UnityEngine;

#endregion

namespace Game.Stage
{

	/*
	 * Apply to GameObject with collider set to isTrigger
	 * Upon collision, deducts 9999 health from the player.
	 */
	public class KillZone: MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D col)
        {
	        // Grab player who collided
	        PlayerStat target = col.gameObject.GetComponent<PlayerStat>();
	        // Deduct their HP
			target.DeductHealth(new DamageInfo(target.ID, target.ID, 9999));
        }
	}
}

