using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DictionaryManager : MonoBehaviour
{
    public GameObject dictionaryItemPrefab;

    private List<Word> words;
    private DefinitionHandler definitionPanel;

    private void Start()
    {
        words = LoadData();

        DictionaryItem currDictItem;

        foreach (Word word in words)
        {
            currDictItem = GameManager.Instance.Blackboard.ObjectPooler.GetPooledObject("dictItem").GetComponent<DictionaryItem>();
            currDictItem.Init(word, definitionPanel);
            currDictItem.gameObject.transform.SetParent(transform);
        }
    }

    public void UnlockWord(string engWord)
    {
        foreach (Word word in words)
        {
            if (word.eng == engWord)
            {
                word.isUnlocked = true;
            }
        }
    }

    public void Filter(string filter)
    {
        if (!string.IsNullOrWhiteSpace(filter))
        {
            foreach (Transform child in transform)
            {
                if (!child.GetComponent<DictionaryItem>().Word.eng.Contains(filter))
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private List<Word> LoadData()
    {
        List<Word> dict = null;

        string path = Application.persistentDataPath + "/dict.json";


        if (!File.Exists(path))
        {
            var dictFile = Resources.Load<TextAsset>("dict");

            RawWords rawWords = JsonUtility.FromJson<RawWords>(dictFile.text);

            File.WriteAllText(path, dictFile.text);

            dict = new List<Word>(rawWords.words);
        }
        else
        {
            var dictFile = File.ReadAllText(path);

            RawWords rawWords = JsonUtility.FromJson<RawWords>(dictFile);

            dict = new List<Word>(rawWords.words);
        }

        return dict;
    }

    [System.Serializable]
    private class RawWords
    {
        public Word[] words;
    }
}