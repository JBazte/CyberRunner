using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModuleManager : TemporalSingleton<ModuleManager> 
{
    public GameObject         m_module0Prefab;
    private GameObject        m_module0Instance;
    private GameObject[]      m_modules;
    private GameObject[]      m_bossmodules;
    private Queue<GameObject> m_modulesOnMap;
    private float             m_minZDistance;
    private float             m_spawnZDistance;
    private int               m_maxModulesOnMap;
    private Vector3           m_spawnVector;
    private bool              m_bossactive = false;
    //private float index = 0;

    [SerializeField]
    private GameObject m_shieldEnemy;
    [SerializeField]
    private GameObject m_slashEnemy;
    [SerializeField]
    private GameObject m_groundWaveEnemy;

    private void Start()
    {
        m_module0Instance = Instantiate(m_module0Prefab, transform);
        m_module0Instance.GetComponent<ModuleBehaviour>().InitializeModule();
        m_modules = GameObject.FindGameObjectsWithTag("Module");
        foreach (GameObject module in m_modules){
            module.GetComponent<ModuleBehaviour>().InitializeModule();
            module.SetActive(false);
        }
        m_modulesOnMap = new Queue<GameObject>();
        m_maxModulesOnMap = 2;
        m_minZDistance = -60.0f;
        m_spawnZDistance = 138.0f;
        m_spawnVector = new Vector3(0, 0, m_spawnZDistance);

        SetInitialScenario();
    }
    // Render.OnBecameOInvisible()
    private void Update() 
    {
        if(m_modulesOnMap.Count > 0 && GameManager.Instance.GetRunActive())
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
    }

    public void SetInitialScenario()
    {
        if(m_modulesOnMap.Count > 0)
        {
            for(int i = 0; i < m_maxModulesOnMap; i++)
            {
                DequeuModule();
            }
        }
        m_module0Instance.transform.position = new Vector3(0, 0, 41);
        m_module0Instance.GetComponent<ModuleBehaviour>().ResetModule();
        m_module0Instance.SetActive(true);
        m_modulesOnMap.Enqueue(m_module0Instance);
        EnqueueModule();
    }

    private void EnqueueModule()
    {
        bool moduleIsValid = false;
        int randomModule;

        while(!moduleIsValid)
        {
            if (!m_bossactive)
            {
                randomModule = Random.Range(0, m_modules.Length);
                
                if (!m_modules[randomModule].activeSelf)
                {
                    m_modules[randomModule].SetActive(true);
                    m_modules[randomModule].GetComponent<ModuleBehaviour>().ResetModule();
                    m_modules[randomModule].transform.position = new Vector3(0.0f, 0.0f, m_modulesOnMap.Peek().transform.position.z + 100.0f);
                    m_modulesOnMap.Enqueue(m_modules[randomModule]);
                    moduleIsValid = true;
                }
            }
            else
            {
                randomModule = Random.Range(0, m_bossmodules.Length);

                if (!m_modules[randomModule].activeSelf)
                {
                    m_bossmodules[randomModule].SetActive(true);
                    m_bossmodules[randomModule].GetComponent<ModuleBehaviour>().ResetModule();
                    m_bossmodules[randomModule].transform.position = new Vector3(0.0f, 0.0f, m_modulesOnMap.Peek().transform.position.z + 100.0f);
                    m_modulesOnMap.Enqueue(m_bossmodules[randomModule]);
                    moduleIsValid = true;
                }

            }
            
        }
    }

    private void DequeuModule()
    {
        Debug.Log("DEQUEEEE" + m_modulesOnMap.Peek());
        m_modulesOnMap.Dequeue().gameObject.SetActive(false);
        
    }

    public GameObject GetShieldEnemy()     { return m_shieldEnemy; }
    public GameObject GetSlashEnemy()      { return m_slashEnemy; }
    public GameObject GetGroundWaveEnemy() { return m_groundWaveEnemy; }
}