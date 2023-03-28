// dnSpy decompiler from Assembly-CSharp.dll class: SimplePool
using System;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
	private static void Init(GameObject prefab = null, int qty = 3)
	{
		if (SimplePool.pools == null)
		{
			SimplePool.pools = new Dictionary<GameObject, SimplePool.Pool>();
		}
		if (prefab != null && !SimplePool.pools.ContainsKey(prefab))
		{
			SimplePool.pools[prefab] = new SimplePool.Pool(prefab, qty);
		}
	}

	public static void Preload(GameObject prefab, int qty = 1)
	{
		SimplePool.Init(prefab, qty);
		GameObject[] array = new GameObject[qty];
		for (int i = 0; i < qty; i++)
		{
			array[i] = SimplePool.Spawn(prefab, Vector3.zero, Quaternion.identity);
		}
		for (int j = 0; j < qty; j++)
		{
			SimplePool.Despawn(array[j]);
		}
	}

	public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		SimplePool.Init(prefab, 3);
		return SimplePool.pools[prefab].Spawn(pos, rot);
	}

	public static void Despawn(GameObject obj)
	{
		SimplePool.PoolMember component = obj.GetComponent<SimplePool.PoolMember>();
		if (component == null)
		{
			UnityEngine.Debug.Log("Object '" + obj.name + "' wasn't spawned from a pool. Destroying it instead.");
			UnityEngine.Object.Destroy(obj);
		}
		else
		{
			component.myPool.Despawn(obj);
		}
	}

	private const int DEFAULT_POOL_SIZE = 3;

	private static Dictionary<GameObject, SimplePool.Pool> pools;

	private class Pool
	{
		public Pool(GameObject prefab, int initialQty)
		{
			this.prefab = prefab;
			this.inactive = new Stack<GameObject>(initialQty);
		}

		public GameObject Spawn(Vector3 pos, Quaternion rot)
		{
			GameObject gameObject;
			if (this.inactive.Count == 0)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, pos, rot);
				gameObject.name = string.Concat(new object[]
				{
					this.prefab.name,
					" (",
					this.nextId++,
					")"
				});
				gameObject.AddComponent<SimplePool.PoolMember>().myPool = this;
			}
			else
			{
				gameObject = this.inactive.Pop();
				if (gameObject == null)
				{
					return this.Spawn(pos, rot);
				}
			}
			gameObject.transform.position = pos;
			gameObject.transform.rotation = rot;
			gameObject.SetActive(true);
			return gameObject;
		}

		public void Despawn(GameObject obj)
		{
			obj.SetActive(false);
			this.inactive.Push(obj);
		}

		private int nextId = 1;

		private Stack<GameObject> inactive;

		private GameObject prefab;
	}

	private class PoolMember : MonoBehaviour
	{
		public SimplePool.Pool myPool;
	}
}
