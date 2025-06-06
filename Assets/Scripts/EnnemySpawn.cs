using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawn : MonoBehaviour
{
    public int ennemyMaxNumber = 20;
    public int strongEnnemyMaxNumber = 1;
    public float maxRange = 40.0f;

    [SerializeField]
    private GameObject ennemyPrefab;
    [SerializeField]
    private GameObject strongEnnemyPrefab;

    [SerializeField]
    private int ennemyCount = 0;
    [SerializeField]
    private int strongEnnemyCount = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (ennemyCount < ennemyMaxNumber)
        {
            Vector3 ennemyPosition = new Vector3(Random.Range(maxRange, -maxRange), 0, Random.Range(maxRange, -maxRange));
            ennemyPrefab.gameObject.transform.position = ennemyPosition;
            Instantiate(ennemyPrefab);
            ennemyCount += 1;

        }
        if (strongEnnemyCount < strongEnnemyMaxNumber)
        {
            Vector3 strongEnnemyPosition = new Vector3(Random.Range(maxRange, -maxRange), 0, Random.Range(maxRange, -maxRange));
            strongEnnemyPrefab.gameObject.transform.position = strongEnnemyPosition;
            Instantiate(strongEnnemyPrefab);
            strongEnnemyCount += 1;

        }
    }

    public void RemoveEnnemy()
    {
        ennemyCount -= 1;
        ennemyMaxNumber += 1;
    }
    public void RemoveStrongEnnemy()
    {
        strongEnnemyCount -= 1;
        strongEnnemyMaxNumber += 1;
    }

}
