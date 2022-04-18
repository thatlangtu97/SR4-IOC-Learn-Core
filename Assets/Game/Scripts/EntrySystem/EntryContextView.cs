using System;
using Sirenix.Serialization;
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

					GameObject Entry = Instantiate( Resources.Load<GameObject>("EntryContext")); 
					//Entry.name = "EntryContext";
					instance = Entry.GetComponent<EntryContextView>();
					DontDestroyOnLoad(Entry);
				}
				return instance;
			}
		}
		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{

				Destroy(gameObject);
			}
			
			context = new EntryContext(this, true);
			context.Start();
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
		}
	}
}
