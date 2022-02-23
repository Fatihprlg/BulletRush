using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] float range = 20.0f;
	[SerializeField] float spawnDelay = 0.3f;
	[SerializeField] int bigEnemyTreshold = 10;
	[SerializeField] GameObject player;
	[SerializeField] GameObject spawnPoint;

	bool activated = false;

	void Update()
	{
		if (activated) return;
		if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
			Debug.DrawLine(transform.position, player.transform.position);
			activated = true;
			StartCoroutine(SpawnEnemies());
        }
	}

	IEnumerator SpawnEnemies()
    {
		int bigEnemyCounter = 0;
		int simpleEnemyPoolCounter = 0, bigEnemyPoolCounter = 0;
		PoolInfo simpleEnemies = ObjectPool.Instance.GetPoolInfo(PoolType.SimpleEnemy);
		PoolInfo bigEnemies = ObjectPool.Instance.GetPoolInfo(PoolType.BigEnemy);
		NavMeshHit hit;
		if (NavMesh.SamplePosition(spawnPoint.transform.position, out hit, 1.0f, NavMesh.AllAreas))
		{
			while (true)
			{
				if (!simpleEnemies.poolObjects[simpleEnemyPoolCounter].activeInHierarchy)
				{
					simpleEnemies.poolObjects[simpleEnemyPoolCounter].transform.position = hit.position;
					simpleEnemies.poolObjects[simpleEnemyPoolCounter++].SetActive(true);
					bigEnemyCounter++;
					if (simpleEnemyPoolCounter == simpleEnemies.poolSize) simpleEnemyPoolCounter = 0;
				}
				yield return new WaitForSeconds(spawnDelay);
				if (bigEnemyCounter >= bigEnemyTreshold && !bigEnemies.poolObjects[bigEnemyPoolCounter].activeInHierarchy)
				{
					bigEnemyCounter = 0;
					bigEnemies.poolObjects[bigEnemyPoolCounter].transform.position = hit.position;
					bigEnemies.poolObjects[bigEnemyPoolCounter++].SetActive(true);
					if (bigEnemyPoolCounter == bigEnemies.poolSize) bigEnemyPoolCounter = 0;

					yield return new WaitForSeconds(spawnDelay);
				}
			}
		}
    }

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, range);
	}
#endif

	/*
	 * Spawn enemies random point on navmesh
	 * 
	 * void SpawnEnemy()
    {
		Vector3 point;
		if (RandomPoint(transform.position, range, out point))
		{
			Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
		}
	}

	bool RandomPoint(Vector3 center, float range, out Vector3 result)
	{
		for (int i = 0; i < 30; i++)
		{
			Vector3 randomPoint = center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
			{
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}*/


}
