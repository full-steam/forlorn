public class Word
{
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

    public string eng;
    public string ind;
    public PartOfSpeech pos;
    public string pronunciationAudioClipID;
    public bool isUnlocked;

    public Word(string eng, string ind, int posVal, string pronunciationAudioClipID, bool isUnlocked)
    {
        this.eng = eng;
        this.ind = ind;
        this.pos = (PartOfSpeech)posVal;
        this.pronunciationAudioClipID = pronunciationAudioClipID;
        this.isUnlocked = isUnlocked;
    }
}

