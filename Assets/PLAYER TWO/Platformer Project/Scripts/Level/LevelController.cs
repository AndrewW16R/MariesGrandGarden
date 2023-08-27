using UnityEngine;

namespace PLAYERTWO.PlatformerProject
{
	[AddComponentMenu("PLAYER TWO/Platformer Project/Level/Level Controller")]
	public class LevelController : MonoBehaviour
	{
		protected LevelFinisher m_finisher => LevelFinisher.instance;
		protected LevelRespawner m_respawner => LevelRespawner.instance;
		protected LevelScore m_score => LevelScore.instance;
		protected LevelPauser m_pauser => LevelPauser.instance;

		public virtual void Finish() => m_finisher.Finish();
		public virtual void Exit() => m_finisher.Exit();

		public virtual void Respawn(bool consumeRetries) => m_respawner.Respawn(consumeRetries);
		public virtual void Restart() => m_respawner.Restart();

		public virtual void AddCoins(int amount) => m_score.coins += amount;
		public virtual void CollectStar(int index) => m_score.CollectStar(index);

		public virtual void AddApples(int amount) => m_score.apples += amount;
		public virtual void ConsolidateScore() => m_score.Consolidate();

		public virtual void Pause(bool value) => m_pauser.Pause(value);

		//checks each type of fruit if all of them have been collected
		public void FruitCheck()
		{

			if (m_score.m_apples == m_score.totalApplesInLevel)
			{
				m_score.applesCollected = true;
				Debug.Log(m_score.applesCollected);
			}
		}
	}
}
