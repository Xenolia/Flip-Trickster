// dnSpy decompiler from Assembly-CSharp.dll class: LowPolyWater.LowPolyWater
using System;
using UnityEngine;

namespace LowPolyWater
{
	public class LowPolyWater : MonoBehaviour
	{
		private void Awake()
		{
			this.meshFilter = base.GetComponent<MeshFilter>();
		}

		private void Start()
		{
			this.CreateMeshLowPoly(this.meshFilter);
		}

		private MeshFilter CreateMeshLowPoly(MeshFilter mf)
		{
			this.mesh = mf.sharedMesh;
			Vector3[] array = this.mesh.vertices;
			int[] triangles = this.mesh.triangles;
			Vector3[] array2 = new Vector3[triangles.Length];
			for (int i = 0; i < triangles.Length; i++)
			{
				array2[i] = array[triangles[i]];
				triangles[i] = i;
			}
			this.mesh.vertices = array2;
			this.mesh.SetTriangles(triangles, 0);
			this.mesh.RecalculateBounds();
			this.mesh.RecalculateNormals();
			this.vertices = this.mesh.vertices;
			return mf;
		}

		private void Update()
		{
			this.GenerateWaves();
		}

		private void GenerateWaves()
		{
			for (int i = 0; i < this.vertices.Length; i++)
			{
				Vector3 vector = this.vertices[i];
				vector.y = 0f;
				float num = Vector3.Distance(vector, this.waveOriginPosition);
				num = num % this.waveLength / this.waveLength;
				vector.y = this.waveHeight * Mathf.Sin(Time.time * 3.14159274f * 2f * this.waveFrequency + 6.28318548f * num);
				this.vertices[i] = vector;
			}
			this.mesh.vertices = this.vertices;
			this.mesh.RecalculateNormals();
			this.mesh.MarkDynamic();
			this.meshFilter.mesh = this.mesh;
		}

		public float waveHeight = 0.5f;

		public float waveFrequency = 0.5f;

		public float waveLength = 0.75f;

		public Vector3 waveOriginPosition = new Vector3(0f, 0f, 0f);

		private MeshFilter meshFilter;

		private Mesh mesh;

		private Vector3[] vertices;
	}
}
