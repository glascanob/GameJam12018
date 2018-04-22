using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform PlayerPosition = null;
    public Transform WorldPosition = null;
    public Transform SpawnParent = null;

    public float SpawnTime = 20.0f;
    public float SpawnVariance = 5.0f;


    public GameObject[] CreatureList;
    public float[] CreatureWeighting;


    private float m_fNextSpawn = 0.0f;



	void Start()
    {
		
	}

	
	void FixedUpdate()
    {
        if (PlayerPosition == null)
            return;

        if (m_fNextSpawn - Time.timeSinceLevelLoad < 0.0f)
        {
            SpawnCreature();
            m_fNextSpawn += (SpawnTime + (Random.Range(-SpawnVariance, SpawnVariance)));
        }
	}


    private void SpawnCreature()
    {
        Vector3 v3UpDir = (WorldPosition.position - PlayerPosition.position).normalized;
        Vector3 v3OriginPosition = WorldPosition.position + (v3UpDir * 100.0f);

        int iWorldMask = 1 << 8;
        RaycastHit hitInfo = new RaycastHit();

        if (Physics.Raycast(v3OriginPosition, -v3UpDir, out hitInfo, 10000.0f, iWorldMask))
            SpawnCreatureAtPosition(hitInfo.point + (v3UpDir * 1.0f));
    }


    private void SpawnCreatureAtPosition(Vector3 v3Pos)
    {
        GameObject enemyObj = GameObject.Instantiate(CreatureList[Random.Range(0,CreatureList.Length)], v3Pos, Quaternion.identity, SpawnParent);

        if (enemyObj != null)
        {
            EnemyController cont = enemyObj.GetComponent<EnemyController>();

            if (cont != null)
            {
                cont.Target = PlayerPosition;
                cont.World = WorldPosition;
            }
        }
    }
}
