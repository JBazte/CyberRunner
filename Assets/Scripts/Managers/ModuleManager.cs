using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModuleManager : TemporalSingleton<ModuleManager> 
{
    public GameObject         m_module0Prefab;
    private GameObject        m_module0Instance;
    public GameObject         m_tutorialModulePrefab;
    private GameObject        m_tutorialModuleInstance;
    private GameObject[]      m_modules;
    private GameObject        m_collisionObject;
    
    private Queue<GameObject> m_modulesOnMap;
    private int               m_maxModulesOnMap;

    private bool              m_bossactive = false;
    
    public int                m_bossPhase = 1;
    public  int               m_phaseModulesCompleted = 0;
    private bool              m_bossPhase2Starts = false;
    [SerializeField]
    private GameObject[] m_bossmodulesPhase1;

    [SerializeField]
    private GameObject m_module0phase2;
    [SerializeField]
    private GameObject[] m_bossmodulesPhase2;
    private GameObject        m_auxModule;

    [SerializeField]
    private GameObject m_shieldEnemy;
    [SerializeField]
    private GameObject m_slashEnemy;
    [SerializeField]
    private GameObject m_groundWaveEnemy;

    private void Start()
    {
        if (PlayerPrefs.GetString(AppPlayerPrefs.TutorialCompleted) == "") PlayerPrefs.SetString(AppPlayerPrefs.TutorialCompleted, "false");
        
        m_module0Instance = Instantiate(m_module0Prefab, transform);
        m_module0Instance.GetComponent<ModuleBehaviour>().InitializeModule();
        m_module0Instance.SetActive(false);
        m_tutorialModuleInstance = Instantiate(m_tutorialModulePrefab, transform);
        m_tutorialModuleInstance.GetComponent<ModuleBehaviour>().InitializeModule();
        m_tutorialModuleInstance.SetActive(false);
        m_modules = GameObject.FindGameObjectsWithTag("Module");
        foreach (GameObject module in m_modules){
            module.GetComponent<ModuleBehaviour>().InitializeModule();
            module.SetActive(false);
        }
        foreach (GameObject module in m_bossmodulesPhase1)
        {
            module.GetComponent<ModuleBehaviour>().InitializeModule();
            module.SetActive(false);
        }
        m_module0phase2.SetActive(false);
        foreach (GameObject module in m_bossmodulesPhase2)
        {
            module.GetComponent<ModuleBehaviour>().InitializeModule();
            module.SetActive(false);
        }

        m_modulesOnMap = new Queue<GameObject>();
        m_maxModulesOnMap = 2;

        SetInitialScenario();
    }
    // Render.OnBecameOInvisible()
    private void Update() 
    {
        if(m_modulesOnMap.Count > 0 && GameManager.Instance.GetRunActive())
        {
            m_auxModule = m_modulesOnMap.Peek();

            foreach (GameObject module in m_modulesOnMap)
            {
                module.transform.position += new Vector3(0, 0, -SpeedManager.Instance.GetRunSpeed() * Time.deltaTime);
            }
            if (m_auxModule.transform.position.z <= -(m_auxModule.GetComponent<ModuleBehaviour>().GetFloorsCount() * 100 - 40))
            {
                DequeuModule();
                EnqueueModule();
            }
        }

        if(m_phaseModulesCompleted == 3)
        {   
            if(m_bossPhase == 1) // WHEN COMPLETING PHASE 1
            {
                m_phaseModulesCompleted = 0;
                m_bossPhase2Starts = true;
                m_bossPhase = 2;
            }
            else if(m_bossPhase == 2) // WHEN COMPLETING PHASE 2
            {
                m_phaseModulesCompleted = 0;
                m_bossPhase = 3;
            }
            else if(m_bossPhase == 3) // END OF FINAL BOSS
            {
                m_bossactive = false;
                m_bossPhase = 0;
                m_phaseModulesCompleted = 0;
            }
        }
    }

    public void SetInitialScenario()
    {
        m_bossPhase = 1;
        m_bossactive = false;
        m_phaseModulesCompleted = 0;
        if(m_modulesOnMap.Count > 0)
        {
            for(int i = 0; i < m_maxModulesOnMap; i++)
            {
                
                DequeuModule();
            }
        }
        if(PlayerPrefs.GetString(AppPlayerPrefs.TutorialCompleted) == "true")
        {
            m_module0Instance.transform.position = new Vector3(0, 0, 41);
            m_module0Instance.GetComponent<ModuleBehaviour>().ResetModule();
            m_module0Instance.SetActive(true);
            m_modulesOnMap.Enqueue(m_module0Instance);
        }
        else if(PlayerPrefs.GetString(AppPlayerPrefs.TutorialCompleted) == "false")
        {
            m_tutorialModuleInstance.transform.position = new Vector3(0, 0, 41);
            m_tutorialModuleInstance.GetComponent<ModuleBehaviour>().ResetModule();
            m_tutorialModuleInstance.SetActive(true);
            m_modulesOnMap.Enqueue(m_tutorialModuleInstance);
        }
            
        EnqueueModule();
    }

    private void EnqueueModule()
    {
        bool moduleIsValid = false;
        int randomModule;

        if(m_bossPhase2Starts)
        {
            m_module0phase2.SetActive(true);
            m_module0phase2.transform.position = new Vector3(0.0f, 0.0f, m_modulesOnMap.Peek().transform.position.z + 100.0f);
            m_modulesOnMap.Enqueue(m_module0phase2);
            m_bossPhase2Starts = false;
        }
        else
        {
            while (!moduleIsValid)
            {
                if (!m_bossactive)
                {
                    randomModule = Random.Range(0, m_modules.Length);

                    if (!m_modules[randomModule].activeSelf)
                    {
                        m_modules[randomModule].SetActive(true);
                        m_modules[randomModule].GetComponent<ModuleBehaviour>().ResetModule();
                        m_modules[randomModule].transform.position = new Vector3(0.0f, 0.0f, 
                            m_modulesOnMap.Peek().transform.position.z + (m_modulesOnMap.Peek().GetComponent<ModuleBehaviour>().GetFloorsCount() * 100.0f));
                        m_modulesOnMap.Enqueue(m_modules[randomModule]);
                        moduleIsValid = true;
                    }
                }
                else
                {
                    if (m_bossPhase == 1 || m_bossPhase == 3)
                    {
                        randomModule = Random.Range(0, m_bossmodulesPhase1.Length);

                        if (!m_bossmodulesPhase1[randomModule].activeSelf)
                        {
                            m_bossmodulesPhase1[randomModule].SetActive(true);
                            m_bossmodulesPhase1[randomModule].transform.position = new Vector3(0.0f, 0.0f,
                                m_modulesOnMap.Peek().transform.position.z + (m_modulesOnMap.Peek().GetComponent<ModuleBehaviour>().GetFloorsCount() * 100.0f));
                            m_modulesOnMap.Enqueue(m_bossmodulesPhase1[randomModule]);
                            m_phaseModulesCompleted++;
                            moduleIsValid = true;
                        }
                    }
                    else if (m_bossPhase == 2)
                    {
                        randomModule = Random.Range(0, m_bossmodulesPhase2.Length);

                        if (!m_bossmodulesPhase2[randomModule].activeSelf)
                        {
                            m_bossmodulesPhase2[randomModule].SetActive(true);
                            m_bossmodulesPhase2[randomModule].transform.position = new Vector3(0.0f, 0.0f,
                                m_modulesOnMap.Peek().transform.position.z + (m_modulesOnMap.Peek().GetComponent<ModuleBehaviour>().GetFloorsCount() * 100.0f));
                            m_modulesOnMap.Enqueue(m_bossmodulesPhase2[randomModule]);
                            m_phaseModulesCompleted++;
                            moduleIsValid = true;
                        }
                    }
                }
            }
        }
    }

    private void DequeuModule()
    {
        m_modulesOnMap.Dequeue().gameObject.SetActive(false);
    }
    
    public bool HasCollisionObject() { if(m_collisionObject != null) return true; else return false; }
    public void SetCollisionObject(GameObject collisionObject) { m_collisionObject = collisionObject; }
    public void DeactivateCollisionObject() { m_collisionObject.GetComponent<Collider>().enabled = false; }
    public void ReactivateCollisionObject() { m_collisionObject.GetComponent<Collider>().enabled = true; }

    public void BossStarts() { m_bossactive = true; }
    public GameObject GetShieldEnemy()     { return m_shieldEnemy; }
    public GameObject GetSlashEnemy()      { return m_slashEnemy; }
    public GameObject GetGroundWaveEnemy() { return m_groundWaveEnemy; }
}