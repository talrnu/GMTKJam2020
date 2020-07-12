using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	private static T m_instance;
	public static T Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = FindObjectOfType<T>();
				if (m_instance == null)
				{
					GameObject newGameObject = new GameObject();
					m_instance = newGameObject.AddComponent<T>();
				}
			}

			return m_instance;
		}
	}

	protected virtual void Awake()
	{
		m_instance = this as T;
	}
}
