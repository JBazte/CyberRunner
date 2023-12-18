using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// This enum will control the score necessary to advance to the next lvl: when player achives LVL_1 score he will be on level 2
// 1 module = 100 meters, depending on score 1 module = 100 score <= 100 meters, regading to time
public enum SCORE_PER_LEVEL { LVL_1 = 200, LVL_2 = 400 , LVL_3 = 600, LVL_4 = 800, LVL_5 = 1000,
    LVL_6 = 1200, LVL_7 = 1400, LVL_8 = 1600, LVL_9 = 1800 };

public class LevelManager : TemporalSingleton<LevelManager>
{
    public int   m_actualLevel;
    private float actualScore;

    private void Start()
    {
        m_actualLevel = 1;
    }

    private void Update()
    {
        if(m_actualLevel != 10)
        {
            actualScore = GameManager.Instance.Score;

            switch(m_actualLevel)
            {
                case 1:
                    if(actualScore >= (float)SCORE_PER_LEVEL.LVL_1)
                        m_actualLevel = 2;
                    break;
                case 2:
                    if (actualScore >= (float)SCORE_PER_LEVEL.LVL_2)
                        m_actualLevel = 3;
                    break;
                case 3:
                    if (actualScore >= (float)SCORE_PER_LEVEL.LVL_3)
                        m_actualLevel = 4;
                    break;
                case 4:
                    if (actualScore >= (float)SCORE_PER_LEVEL.LVL_4)
                        m_actualLevel = 5;
                    break;
                case 5:
                    if (actualScore >= (float)SCORE_PER_LEVEL.LVL_5)
                        m_actualLevel = 6;
                    break;
                case 6:
                    if (actualScore >= (float)SCORE_PER_LEVEL.LVL_6)
                        m_actualLevel = 7;
                    break;
                case 7:
                    if (actualScore >= (float)SCORE_PER_LEVEL.LVL_7)
                        m_actualLevel = 8;
                    break;
                case 8:
                    if (actualScore >= (float)SCORE_PER_LEVEL.LVL_8)
                        m_actualLevel = 9;
                    break;
                case 9:
                    if (actualScore >= (float)SCORE_PER_LEVEL.LVL_9)
                    {
                        // MODULE MANAGER STARTS GENERATING BOSS MODULE
                        // BOSS APPEARS
                        ModuleManager.Instance.BossStarts();
                        // MUSIC CHANGES?
                        m_actualLevel = 10;
                    }
                    break;
                default:
                    Debug.Log("Not a valid game level.");
                    break;
            }
        }
    }

    public void ResetActualLevel() { m_actualLevel = 1;}
}