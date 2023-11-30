using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModuleManager : TemporalSingleton<ModuleManager> 
{
    public GameObject         m_module0;
    private GameObject[]      m_modules;
    private Queue<GameObject> m_modulesOnMap;
    private float             m_minZDistance;
    private float             m_spawnZDistance;
    private int               m_maxModulesOnMap;
    private Vector3           m_spawnVector;
    //private float index = 0;

    [SerializeField]
    private GameObject     m_shieldEnemy;
    [SerializeField]
    private GameObject m_slashEnemy;
    [SerializeField]
    private GameObject m_groundWaveEnemy;

    private void Start()
    {
        GameObject StartPlane1 = Instantiate(m_module0, transform);
        StartPlane1.transform.position = new Vector3(0, 0, 41);
        m_modules = GameObject.FindGameObjectsWithTag("Module");
        foreach (GameObject module in m_modules){
            module.GetComponent<ModuleBehaviour>().InitializeModule();
            module.SetActive(false);
        }

        m_modulesOnMap = new Queue<GameObject>();
        m_modulesOnMap.Enqueue(StartPlane1);
        m_maxModulesOnMap = 2;
        m_minZDistance = -60.0f;
        m_spawnZDistance = 138.0f;
        m_spawnVector = new Vector3(0, 0, m_spawnZDistance);

        //StartPlane1.SetActive(true);
        EnqueueModule();

        //m_shieldEnemy.GetComponent<EnemyAbstract>().SetIsSpawn(true);
        //m_slashEnemy.GetComponent<EnemyAbstract>().SetIsSpawn(true);
        //m_groundWaveEnemy.GetComponent<EnemyAbstract>().SetIsSpawn(true);
    }
    // Render.OnBecameOInvisible()
    private void Update() 
    {
        foreach(GameObject module in m_modulesOnMap)
        {
            module.transform.position += new Vector3(0, 0, -SpeedManager.Instance.GetRunSpeed() * Time.deltaTime);
        }

        if (m_modulesOnMap.Peek().transform.position.z <= m_minZDistance)
        {
            DequeuModule();
            EnqueueModule();
        }
    }

    private void EnqueueModule()
    {
        bool moduleIsValid = false;
        int randomModule;

        while(!moduleIsValid)
        {
            randomModule = Random.Range(0, m_modules.Length);

            if (!m_modules[randomModule].activeSelf)
            {
                m_modulesOnMap.Enqueue(m_modules[randomModule]);
                m_modules[randomModule].SetActive(true);
                m_modules[randomModule].GetComponent<ModuleBehaviour>().ResetModule();
                m_modules[randomModule].transform.position = new Vector3(0.0f, 0.0f, m_modulesOnMap.Peek().transform.position.z + 100.0f);
                moduleIsValid = true;
            }
        }
    }

    private void DequeuModule()
    {
        m_modulesOnMap.Dequeue().gameObject.SetActive(false);
    }

    public GameObject GetShieldEnemy()     { return m_shieldEnemy; }
    public GameObject GetSlashEnemy()      { return m_slashEnemy; }
    public GameObject GetGroundWaveEnemy() { return m_groundWaveEnemy; }
}