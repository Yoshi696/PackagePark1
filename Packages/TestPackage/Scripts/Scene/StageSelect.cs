using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : EditorWindow
{
    [SerializeField] Object[] scenes;
    [SerializeField] bool triggers;
    GameObject mng;

    SerializedObject so;
    SerializedProperty stringsProperty;
    SerializedProperty triggersProperty;

    // ���j���[�̖��O
    // Unity�G�f�B�^�̃��j���[�ɒǉ������
    // �������֐�
    [MenuItem("Custom/StageSelect")]
    static void Init()
    {
        //StageSelect window = (StageSelect)EditorWindow.GetWindow(typeof(StageSelect));
        //window.Show();

        GetWindow<StageSelect>();
    }

    void OnEnable()
    {
        so = new SerializedObject(this);
        stringsProperty = so.FindProperty("scenes");
        triggersProperty = so.FindProperty("triggers");
    }

    // �G�f�B�^�[��GUI������
    void OnGUI()
    {

        so.Update();

        EditorGUILayout.PropertyField(stringsProperty, true);
        EditorGUILayout.PropertyField(triggersProperty, true);

        so.ApplyModifiedProperties();

        string[] guids = AssetDatabase.FindAssets("SceneManager");
        string paths = AssetDatabase.GUIDToAssetPath(guids[0]);

        //unity�G�f�B�^�ł��������Ȃ����炿�イ��
        mng = AssetDatabase.LoadAssetAtPath<GameObject>(paths);

        // �G�f�B�^�[��Generate�Ƃ������O�̃{�^����z�u
        // �����ꂽ��I�u�W�F�N�g���V�[���ɔz�u�����
        if (GUILayout.Button("SceneOk"))
        {
            GameObject now = GameObject.Find(mng.name);

            if (now == null)
            {
                GameObject obj = Instantiate(mng, new Vector3(0, 0, 0), Quaternion.identity);
                obj.name = mng.name;
                obj.GetComponent<StageManager>().GetStages = scenes;
            }
            else
            {
                DestroyImmediate(now);

                GameObject obj = Instantiate(mng, new Vector3(0, 0, 0), Quaternion.identity);
                obj.name = mng.name;
                obj.GetComponent<StageManager>().GetStages = scenes;
            }
        }
    }

    // �V�[���ɃI�u�W�F�N�g��z�u����
    // 16��4�~4�Ŕz�u����
    void InitStage()
    {
        Instantiate(mng, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public static string FindPath(string name)
    {
        var paths = Directory.GetFiles(Application.dataPath, name, SearchOption.AllDirectories);

        if (paths != null && paths.Length > 0)
        {
            return paths[0].Replace("\\", "/").Replace(Application.dataPath, "Assets");
        }
        return null;
    }

    //public string GetPath(Transform obj)
    //{
    //    string path = obj.gameObject.name;
    //    Transform parent = obj.parent;
    //    while (parent != null)
    //    {
    //        path = parent.name + "/" + path;
    //        parent = parent.parent;
    //    }
    //    return path;
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    int check;

    //    if(stage.Length <= 0)
    //    {
    //        Debug.Log("test");
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //SceneManager.LoadScene("MainScene");
    //}
}
