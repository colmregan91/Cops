using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Loader : MonoBehaviour
{
    public loadOption[] progress;
    private void OnEnable()
    {
        var SavedFiles = Directory.GetFiles(Application.persistentDataPath + "/saves"); // get player progress json saved files
        for (int i = 0; i < SavedFiles.Length; i++) // loop through json files
        {
            if (SavedFiles[i] != null)
            {
                progress[i].gameObject.SetActive(true); // if not null turn on a load option
                var r = File.ReadAllText(SavedFiles[i]); // read the saved data from the saved file 
                var d = CreateFromJSON(r); // Parse it to a a player progress instance
                progress[i].prog = d; // set the progression value in this load option to the values it reads
                progress[i].Init(); // initialise load option
            }
            else
            {
                progress[i].gameObject.SetActive(false); // no saved file turn off load option
            }
        }
    }
    public static PlayerProgress CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerProgress>(jsonString); // return json data parsed into a player progress instance
    }

    public void ClearSaves()
    {
        if (Directory.Exists(Application.persistentDataPath + "/saves")) // get saved path
        {
            var f = Directory.GetFiles(Application.persistentDataPath + "/saves");
            for (int i = 0; i < f.Length; i++)
            {
                File.Delete(f[i]); // delete files
            }
        }
        for (int i = 0; i < progress.Length; i++) // turn off load options
        {
            progress[i].gameObject.SetActive(false);
        }
    }
}
