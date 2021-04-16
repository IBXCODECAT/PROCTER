using UnityEngine;

public class Chunk : MonoBehaviour
{
	GameObject meshObject;
	Vector2 position;
	Bounds bounds;

	public Chunk(Vector2 coord, int size, Transform parent, GameObject chunk)
	{
		TerrainData meshData;

		position = coord * size;
		bounds = new Bounds(position, Vector2.one * size);
		Vector3 positionV3 = new Vector3(position.x, 0, position.y);

		meshObject = Instantiate(chunk, Vector3.zero, Quaternion.identity);

		meshData = meshObject.GetComponent<Terrain>().terrainData;

		MapGenerator.GenerateMesh(position, meshData);

		meshObject.transform.position = positionV3;
		meshObject.transform.parent = parent;
		Loader(false);
	}

	public void UpdateChunkState(float renderDistance, Vector2 center)
	{
		float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(center));
		bool visible = viewerDstFromNearestEdge <= renderDistance;
		Loader(visible);
	}

	public void Loader(bool visible) { meshObject.SetActive(visible); }
	public bool IsRendering() { return meshObject.activeSelf; }

}