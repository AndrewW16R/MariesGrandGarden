using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PLAYERTWO.PlatformerProject
{
	[AddComponentMenu("PLAYER TWO/Platformer Project/Level/Level Finisher")]
	public class LevelFinisher : Singleton<LevelFinisher>
	{
		/// <summary>
		/// Called when the Level has been finished.
		/// </summary>
		public UnityEvent OnFinish;

		/// <summary>
		/// Called when the Level has exited.
		/// </summary>
		public UnityEvent OnExit;

		public bool unlockNextLevel;
		public string nextScene;
		public string exitScene;
		public float loadingDelay = 1f;
		public GameObject clearedTexted;

		protected Game m_game => Game.instance;
		protected Level m_level => Level.instance;
		protected LevelScore m_score => LevelScore.instance;
		protected LevelPauser m_pauser => LevelPauser.instance;
		protected GameLoader m_loader => GameLoader.instance;
		protected Fader m_fader => Fader.instance;

        public void Start()
        {
            if (clearedTexted != null)
            {
				clearedTexted.SetActive(false);
            }
        }


        protected virtual IEnumerator FinishRoutine()
		{
			OnFinish?.Invoke();
			m_pauser.Pause(false);
			m_pauser.canPause = false;
			m_score.stopTime = true;
			m_level.player.inputs.enabled = false;

			if (clearedTexted != null)
			{
				clearedTexted.SetActive(true);
			}

			yield return new WaitForSeconds(loadingDelay);

			if (unlockNextLevel)
			{
				m_game.UnlockNextLevel();
			}

			Game.LockCursor(false);
			m_score.Consolidate();
			m_loader.Load(nextScene);
			
		}

		protected virtual IEnumerator ExitRoutine()
		{
			m_pauser.Pause(false);
			m_pauser.canPause = false;
			m_level.player.inputs.enabled = false;
			yield return new WaitForSeconds(loadingDelay);
			Game.LockCursor(false);
			m_loader.Load(exitScene);
			OnExit?.Invoke();
		}

		

		/// <summary>
		/// Invokes the Level finishing routine to consolidate the score and load the next scene.
		/// </summary>
		public virtual void Finish()
		{
			StopAllCoroutines();
			StartCoroutine(FinishRoutine());
		}

		/// <summary>
		/// Invokes the Level exit routine, Level Score is not saved.
		/// </summary>
		public virtual void Exit()
		{
			StopAllCoroutines();
			StartCoroutine(ExitRoutine());
		}
	}
}
