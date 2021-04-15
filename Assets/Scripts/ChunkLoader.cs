using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
	public static ChunkLoader instance;

	[SerializeField] private GameObject center;

	public void UpdateChunkState(float renderDistance, GameObject chunkObject)
	{
		float viewerDstFromNearestEdge = Mathf.Sqrt(ChunkManagement.Chunk.bounds.SqrDistance(center.transform.position));
		bool visible = viewerDstFromNearestEdge <= renderDistance;
		Loader(visible, chunkObject);
	}

	public void Loader(bool visible, GameObject chunkObject) { chunkObject.SetActive(visible); }
	public bool IsRendering(GameObject chunkObject) { return chunkObject.activeSelf; }
}
