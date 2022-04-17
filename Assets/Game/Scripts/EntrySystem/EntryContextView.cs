using System;
using UnityEngine;
using strange.extensions.context.impl;
using UnityEngine.SceneManagement;

namespace EntrySystem {
	public class EntryContextView : ContextView
	{
		private static EntryContextView instance;
		public bool loadFlashScene;
		public static EntryContextView Instance
		{
			get
			{
				if (instance == null)
				{
					GameObject dataManager = new GameObject();
					dataManager.name = "EntryContext";
					instance = dataManager.AddComponent<EntryContextView>();
					DontDestroyOnLoad(instance);
					
				}
				return instance;
			}
		}

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			context = new EntryContext(this, true);
			context.Start();
			Debug.Log("Awake entry");
#if UNITY_EDITOR
			Application.targetFrameRate = -1;
#else
			Application.targetFrameRate = 70;
#endif
		}

		void Start()
		{
			if(loadFlashScene)
				SceneManager.LoadScene("FlashScene");
			Debug.Log("start entry");
		}
	}
}
