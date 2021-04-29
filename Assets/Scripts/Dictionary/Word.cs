using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{

    public string eng;
    public string ind;
    public PartOfSpeech pos;
    public AudioClip pronunciation; //Maybe better using string? Using the ID of the clip in the AudioManager.
    public bool isUnlocked;


    public Word(string eng, string ind, PartOfSpeech pos, AudioClip pronunciation, bool isUnlocked)
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
