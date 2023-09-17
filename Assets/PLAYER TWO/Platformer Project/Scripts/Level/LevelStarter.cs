using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PLAYERTWO.PlatformerProject
{
	[AddComponentMenu("PLAYER TWO/Platformer Project/Level/Level Starter")]
	public class LevelStarter : Singleton<LevelStarter>
	{
		/// <summary>
		/// Called when the starter routine has endend.
		/// </summary>
		public UnityEvent OnStart;

		public float enablePlayerDelay = 1f;

		public bool cutscenePlaying;
		public float cutsceneLength;
		public float timePassed;
		public GameObject cutsceneCamera;
		public GameObject player;
		public PlayerInputManager m_inputManager;

		protected Level m_level => Level.instance;
		protected LevelScore m_score => LevelScore.instance;
		protected LevelPauser m_pauser => LevelPauser.instance;


		protected Fader m_fader => Fader.instance;

		protected virtual IEnumerator Routine()
		{
			Game.LockCursor();
			m_level.player.controller.enabled = false;
			m_level.player.inputs.enabled = false;
			yield return new WaitForSeconds(enablePlayerDelay);
			m_score.stopTime = false;
			m_level.player.controller.enabled = true;
			m_level.player.inputs.enabled = true;
			m_pauser.canPause = true;
			OnStart?.Invoke();
		}

		protected virtual void Start()
		{
			StartCoroutine(Routine());
			//changes made to be able to skip cutscene are below this line
			timePassed = 0;
			cutsceneCamera.SetActive(true);
			cutscenePlaying = true;
			m_inputManager = player.GetComponent<PlayerInputManager>();
			
		}

        public void FixedUpdate()
        {
			CutsceneUpdate();
        }

		public void CutsceneUpdate()
        {
			if (timePassed < cutsceneLength && cutscenePlaying == true)
            {
				timePassed += 1;
				m_pauser.canPause = false;
				m_level.player.controller.enabled = false;
				if (m_inputManager.m_pause.IsPressed())
                {
					cutsceneCamera.SetActive(false);
					m_pauser.canPause = true;
					m_level.player.controller.enabled = true;
					cutscenePlaying = false;
				}
			}
			else
            {
				m_pauser.canPause = true;
			}
        }
    }
}
