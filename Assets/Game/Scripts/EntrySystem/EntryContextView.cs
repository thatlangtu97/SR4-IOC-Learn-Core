using UnityEngine;
using strange.extensions.context.impl;
using UnityEngine.SceneManagement;

namespace EntrySystem {
	public class EntryContextView : ContextView {
		void Start()
		{
			DontDestroyOnLoad(gameObject);
			context = new EntryContext(this, true);
			context.Start();
			SceneManager.LoadScene("FlashScene");
		}
	}
}
