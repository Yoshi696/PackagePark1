using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]private Object[] stages;
    private string stageNow,stageNext;

    public Object[] GetStages
    {
        set { stages = value; }
        get { return stages; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        SceneManager.LoadScene(stages[0].name);
        stageNow = stages[0].name;
        stageNext = stageNow;
        //Debug.Log(stages[0].name);
    }

    // Update is called once per frame
    void Update()
    {
        StageSelect();
    }

    void StageSelect()
    {
        string stage = stageNow;

        if(stage != stageNext)
        {
            SceneManager.LoadScene(stageNext);
            stageNow = stageNext;
        }
    }

    public void SummonNextStage(string next) {
        stageNext = next;
    }
}
