[System.Serializable]
public class Word
{

    public string eng;
    public string ind;
    public string pos;
    public string pronunciationAudioClipID;
    public bool isUnlocked;
    public Word(string eng, string ind, PartOfSpeech pos, string pronunciationAudioClipID, bool isUnlocked)
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

//Does this need to be public?
public enum PartOfSpeech
{
    Benda,
    Penentu,
    Ganti,
    Kerja,
    Sifat,
    Keterangan,
    Depan,
    Konjungsi
};
