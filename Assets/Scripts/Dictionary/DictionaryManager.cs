using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DictionaryManager : MonoBehaviour
{
    [System.Serializable]
    private class RawWord
    {
        public string eng;
        public string ind;
        public int posVal;
        public string pronunciationAudioClipID;
        public bool isUnlocked;
    }

    [System.Serializable]
    private class RawWords
    {
        public RawWord[] dict;
    }

    public DefinitionHandler definitionPanel;
    //public GameObject dictionaryPanel;

    private List<Word> words;
    private Dictionary<string, GameObject> dictItems = new Dictionary<string, GameObject>();


    private void Start()
    {
        words = LoadData();

        DictionaryItem currDictItem;

        foreach (Word word in words)
        {
            currDictItem = GameManager.Instance.Blackboard.ObjectPooler.GetPooledObject("dictItem").GetComponent<DictionaryItem>();
            currDictItem.Init(word, definitionPanel);
            currDictItem.gameObject.transform.SetParent(transform);
            currDictItem.gameObject.SetActive(word.isUnlocked);
            dictItems.Add(word.eng, currDictItem.gameObject);
        }
    }

    /// <summary>
    /// Unlocks a word and enables the dictionary entry.
    /// </summary>
    /// <param name="engWord">Unlocked word.</param>
    public void UnlockWord(string engWord)
    {
        foreach (Word word in words)
        {
            if (word.eng == engWord)
            {
                word.isUnlocked = true;

                dictItems[word.eng].gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Filters the dictionary entries.
    /// </summary>
    /// <param name="filter">Search term.</param>
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

    /// <summary>
    /// Loads data from the saved dictionary. If there is no saved dictionary, creates a base dictionary.
    /// </summary>
    /// <returns>List of Words.</returns>
    private List<Word> LoadData()
    {
        string path = Application.persistentDataPath + "/dict.json";

        List<RawWord> rawDict = null;

        if (!File.Exists(path))
        {
            var dictFile = Resources.Load<TextAsset>("dict");

            RawWords rawWords = JsonUtility.FromJson<RawWords>(dictFile.text);

            File.WriteAllText(path, dictFile.text);

            rawDict = new List<RawWord>(rawWords.dict);
        }
        else
        {
            var dictFile = File.ReadAllText(path);

            RawWords rawWords = JsonUtility.FromJson<RawWords>(dictFile);

            rawDict = new List<RawWord>(rawWords.dict);
        }

        List<Word> dict = new List<Word>();

        foreach (RawWord rawWord in rawDict)
        {
            dict.Add(new Word(rawWord.eng, rawWord.ind, rawWord.posVal, rawWord.pronunciationAudioClipID, rawWord.isUnlocked));
        }

        return dict;
    }

    /// <summary>
    /// Saves the dictionary to persistent data path.
    /// </summary>
    public void SaveDictionary()
    {
        List<RawWord> rawDict = new List<RawWord>();

        foreach (Word word in words)
        {
            RawWord newRawWord = new RawWord();
            newRawWord.eng = word.eng;
            newRawWord.ind = word.ind;
            newRawWord.posVal = (int) word.pos;
            newRawWord.pronunciationAudioClipID = word.pronunciationAudioClipID;
            newRawWord.isUnlocked = word.isUnlocked;
            rawDict.Add(newRawWord);
        }

        RawWords rawWords = new RawWords();
        rawWords.dict = rawDict.ToArray();

        string jsonString = JsonUtility.ToJson(rawWords);
        File.WriteAllText(Application.persistentDataPath + "/dict.json", jsonString);
    }

    /// <summary>
    /// Deletes the local dictionary in the persistent data path.
    /// </summary>
    public void DeleteDictionary()
    {
        string path = Application.persistentDataPath + "/dict.json";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}