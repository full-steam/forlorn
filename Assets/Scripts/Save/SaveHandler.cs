using UnityEngine;

public class SaveHandler : MonoBehaviour
{

    public SaveObject so = new SaveObject();
    //public PlayerController player;
    //public FlagManager flag;

    public void AssignSaveData()
    {
        //player.playerStatus.AssignPlayerStatus(so);

        //Flags should have a method to apply data to FlagManager
    }

    public SaveObject GetLatestSaveData()
    {
        //player.playerStatus.GetPlayerStatus(out so);

        //Flags should have a method to give the data of dictionary that is already split into 2 attributes by sending out SaveHandler's so.

        return so;
    }
}
