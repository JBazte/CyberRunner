using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : TemporalSingleton<ModuleManager> 
{
    public GameObject   m_startTile;
    public GameObject[] m_modules;
    private float index = 0;

    private void Start()
    {
        GameObject StartPlane1 = Instantiate(m_startTile, transform);
        StartPlane1.transform.position = new Vector3(0, 0, 41);
        m_modules = GameObject.FindGameObjectsWithTag("Module");
        foreach (GameObject tile in m_modules){
            tile.GetComponent<ModuleBehaviour>().InitializeModule();
            tile.SetActive(false);
        }
    }
    
    private void Update() 
    {
        for (int i = 0; i < m_modules.Length; i++)
        {
            m_modules[i].transform.position += new Vector3(0, 0, -SpeedManager.Instance.GetRunSpeed() * Time.deltaTime);
        }
        gameObject.transform.position += new Vector3(0, 0, -SpeedManager.Instance.GetRunSpeed() * Time.deltaTime);

        for (int i = 0; i < m_modules.Length; i++)
        {
            if (m_modules[i].activeSelf && m_modules[i].transform.position.z <= -80)
            {
                m_modules[i].SetActive(false);
            }
        }

        if (transform.position.z <= index) 
        {
            int RandomInt1 = Random.Range(0, m_modules.Length-1);

            if (m_modules[RandomInt1].activeSelf && m_modules[RandomInt1].transform.position.z <= -60)
            {
                m_modules[RandomInt1].transform.position = new Vector3(0, 0, 139);

                m_modules[RandomInt1].GetComponent<ModuleBehaviour>().RandomizeModule();
            }
            else if (!m_modules[RandomInt1].activeSelf)
            {
                m_modules[RandomInt1].SetActive(true);
                m_modules[RandomInt1].transform.position = new Vector3(0, 0, 139);

                m_modules[RandomInt1].GetComponent<ModuleBehaviour>().RandomizeModule();
            }
            else
            {
                RandomInt1 += 1;

                if (m_modules[RandomInt1].activeSelf && m_modules[RandomInt1].transform.position.z <= -60)
                {
                    m_modules[RandomInt1].transform.position = new Vector3(0, 0, 138);
                    m_modules[RandomInt1].GetComponent<ModuleBehaviour>().RandomizeModule();
                }
                else if (!m_modules[RandomInt1].activeSelf)
                {
                    m_modules[RandomInt1].SetActive(true);
                    m_modules[RandomInt1].transform.position = new Vector3(0, 0, 138);
                    m_modules[RandomInt1].GetComponent<ModuleBehaviour>().RandomizeModule();
                }
                
            }      
            
            index = index - 100.0f;
        }

    }
}