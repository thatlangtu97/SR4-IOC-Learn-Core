using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
public abstract class AbsShowPopupCmd : Command
{
    [Inject] public PopupManager popupManager { get; set; }
    public override void Execute()
    {

    }
	public T GetInstance<T>() where T : Component
	{
		bool isInit = injectionBinder.GetBinding<T>(GetInjectName()) == null ||
					  injectionBinder.GetInstance<T>(GetInjectName()) == null;

		if (isInit)
		{
			GameObject q = Instantiate();
			//q.transform.localScale = new Vector3(1, 1, 1);
			if (injectionBinder.GetBinding<T>(GetInjectName()) != null)
			{
				injectionBinder.Unbind<T>(GetInjectName());
			}

			injectionBinder.Bind<T>()
				.ToValue(q.GetComponent<T>())
				.ToName(GetInjectName());
		}

		return injectionBinder.GetInstance<T>(GetInjectName());
	}

	public virtual string GetInjectName()
	{
		return "";
	}
	private GameObject Instantiate()
	{
		GameObject o = PrefabUtils.LoadPrefab(GetResourcePath());
		GameObject spawned = null;
		AbsPopupView typePopup = o.GetComponent<AbsPopupView>();
		if (!popupManager.CheckContainPopup(typePopup))
		{
			spawned = GameObject.Instantiate(o) as GameObject;
			popupManager.AddPopup(spawned.GetComponent<AbsPopupView>());
		}
		else
		{
			if (popupManager.GetPopupByPopupKey(typePopup) == null)
			{
				spawned = GameObject.Instantiate(o) as GameObject;
				popupManager.AddPopup(spawned.GetComponent<AbsPopupView>());
			}
			else
			{
				spawned = popupManager.GetPopupByPopupKey(typePopup).gameObject;

			}
		}

		return spawned;
	}

	public abstract string GetResourcePath();
}
