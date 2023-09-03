using UnityEngine;
using UnityEngine.UI;

namespace PLAYERTWO.PlatformerProject
{
	[AddComponentMenu("PLAYER TWO/Platformer Project/UI/HUD")]
	public class HUD : MonoBehaviour
	{
		public string retriesFormat = "00";
		public string coinsFormat = "000";
		public string healthFormat = "0";
        public string applesFormat = "0";
		public string totalApplesFormat = "/3";
		public string lemonsFormat = "0";
		public string totalLemonsFormat = "/2";
		public string watermelonsFormat = "0";
		public string totalWatermelonsFormat = "/1";

		[Header("UI Elements")]
		public Text retries;
		public Text coins;
		public Text health;
		public Text timer;
		public Text apples;
		public Text totalApples;
		public Text lemons;
		public Text totalLemons;
		public Text watermelons;
		public Text totalWatermelons;
		public Image[] starsImages;

		protected Game m_game;
		protected LevelScore m_score;
		protected Player m_player;

		protected float timerStep;
		protected static float timerRefreshRate = .1f;

	

		/// <summary>
		/// Set the coin counter to a given value.
		/// </summary>
		protected virtual void UpdateCoins(int value)
		{
			coins.text = value.ToString(coinsFormat);
		}

		//Sets apple counter and displays text green if all apples are collected
		protected virtual void UpdateApples(int value)
		{
			apples.text = value.ToString(applesFormat);

			Debug.Log("Test");
			if(m_score.apples == m_score.totalApplesInLevel)
            {
				Debug.Log("HUD TEST");
                apples.color = Color.green;
				totalApples.color = Color.green;
            }
        }

		protected virtual void UpdateLemons(int value)
		{
			lemons.text = value.ToString(lemonsFormat);

			Debug.Log("Test");
			if (m_score.lemons == m_score.totalLemonsInLevel)
			{
				Debug.Log("HUD TEST");
				lemons.color = Color.green;
				totalLemons.color = Color.green;
			}
		}

		protected virtual void UpdateWatermelons(int value)
		{
			watermelons.text = value.ToString(watermelonsFormat);

			Debug.Log("Test");
			if (m_score.watermelons == m_score.totalWatermelonsInLevel)
			{
				Debug.Log("HUD TEST");
				watermelons.color = Color.green;
				totalWatermelons.color = Color.green;
			}
		}

		/// <summary>
		/// Set the retries counter to a given value.
		/// </summary>
		protected virtual void UpdateRetries(int value)
		{
			retries.text = value.ToString(retriesFormat);
		}

		/// <summary>
		/// Called when the Player Health changed.
		/// </summary>
		protected virtual void UpdateHealth()
		{
			health.text = m_player.health.current.ToString(healthFormat);
		}

		/// <summary>
		/// Set the stars images enabled state to match a boolean array.
		/// </summary>
		protected virtual void UpdateStars(bool[] value)
		{
			for (int i = 0; i < starsImages.Length; i++)
			{
				starsImages[i].enabled = value[i];
			}
		}

		/// <summary>
		/// Set the timer text to the Level Score time.
		/// </summary>
		protected virtual void UpdateTimer()
		{
			timerStep += Time.deltaTime;

			if (timerStep >= timerRefreshRate)
			{
				var time = m_score.time;
				timer.text = GameLevel.FormattedTime(m_score.time);
				timerStep = 0;
			}
		}

		/// <summary>
		/// Called to force an updated on the HUD.
		/// </summary>
		public virtual void Refresh()
		{
			UpdateCoins(m_score.coins);
			UpdateRetries(m_game.retries);
			UpdateHealth();
            UpdateStars(m_score.stars);
			UpdateApples(m_score.apples);
			UpdateLemons(m_score.lemons);
			UpdateWatermelons(m_score.watermelons);
		}

        protected virtual void Awake()
		{
			m_game = Game.instance;
			m_score = LevelScore.instance;
			m_player = FindObjectOfType<Player>();

			m_score.OnScoreLoaded.AddListener(() =>
			{
				m_score.OnCoinsSet.AddListener(UpdateCoins);
				m_score.OnStarsSet.AddListener(UpdateStars);
				m_game.OnRetriesSet.AddListener(UpdateRetries);
				m_player.health.onChange.AddListener(UpdateHealth);
				m_score.OnApplesSet.AddListener(UpdateApples);
				m_score.OnLemonsSet.AddListener(UpdateLemons);
				m_score.OnWatermelonsSet.AddListener(UpdateWatermelons);
				Refresh();
			});
		}

		protected virtual void Update() => UpdateTimer();
	}
}
