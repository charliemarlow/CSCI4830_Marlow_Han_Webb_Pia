using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;



public class Leaderboard : MonoBehaviour
{

    List<List<string>> currentCSV = new List<List<string>>();
    string finalDest;

    int positionLoc = 0;
    int scoreLoc = 1;
    int dateLoc = 2;
    int nameLoc = 3;
    int timeLoc = 4;
    bool emptyCSV = false;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        finalDest = filePath + @"\chessLeaderboard.txt";
        if (!File.Exists(finalDest))
        {
            File.Create(finalDest);
        }
    }

    public void writeToLeaderboard(float newScore, float time, string name)
    {
        DateTime date = new DateTime();
        Debug.Log(date.Month);
        Debug.Log(date.Day);
        Debug.Log(date.Year);
        updateCurrentCSV();
        int users = currentCSV.Count;

        // find if the user is already in the leaderboard
        int loc = 0;
        bool alreadyExists = false;
        foreach(List<string> ls in currentCSV)
        {
            
            if (!emptyCSV && ls.Count > nameLoc && ls[nameLoc].Equals(name))
            {
                Debug.Log("same name");
                alreadyExists = true;
                break;
            }
            loc++;
        }

        List<string> currentEntry = new List<string>();
        if (alreadyExists)
        {
            currentEntry = currentCSV[loc];
            // remove currentEntry, insert it later
            currentCSV.RemoveAt(loc);

            // set score
            string currentScore = currentEntry[scoreLoc];
            float currfScore = float.Parse(currentScore);
            currfScore += newScore;
            currentScore = currfScore.ToString();
            currentEntry[scoreLoc] = currentScore;

            // set date
            currentEntry[dateLoc] = "0";

            string oldTime = currentEntry[timeLoc];
            float oldfTime = float.Parse(oldTime);
            if(time > oldfTime)
            {
                time = oldfTime;
            }
            string newTime = time.ToString();
            Debug.Log("t" +time + "tts" +time.ToString());
            currentEntry[timeLoc] = newTime;

        }
        else
        {
            List<string> newEntry = new List<string>();
            newEntry.Add(currentCSV.Count.ToString()); // add number
            newEntry.Add(newScore.ToString());  // add score
            newEntry.Add("0"); // add date
            newEntry.Add(name);
            currentCSV.Add(newEntry); // added new entry to global
            currentEntry = newEntry;
        }

        // basically look for the first entry that yours is better than and insert it there
        int index = 0;
        bool inserted = false;
        if (emptyCSV)
        {
            currentCSV.Add(currentEntry);
        }
        else
        {
            foreach (List<string> stringL in currentCSV)
            {
                string currentSTime = stringL[timeLoc];
                float currentFTime = float.Parse(currentSTime);
                if (time < currentFTime)
                {
                    currentCSV.Insert(index, currentEntry);
                    inserted = true;
                    break;
                }
                index++;
            }

            if (!inserted)
            {
                currentCSV.Add(currentEntry);
            }
        }

        index = 0;
        foreach (List<string> stringL in currentCSV)
        {
            if(index == 0)
            {
                index++;
                continue;
            }
            stringL[positionLoc] = index.ToString();
            index++;
        }

        // writeback
        writeBack();

    }
    void updateCurrentCSV()
    {
        string allText = System.IO.File.ReadAllText(@finalDest, System.Text.Encoding.ASCII);
        if (allText.Equals(""))
        {
            emptyCSV = true;
            currentCSV.Clear();
        }
        Debug.Log(allText.ToString());
        currentCSV.Clear();
        List<string> currCSV = allText.Split('\n').ToList<string>();

        foreach (string s in currCSV)
        {
            currentCSV.Add(s.Split(' ').ToList<string>());
        }
    }

    void writeBack()
    {
        emptyCSV = false;
        List<string> currCSV = new List<string>();
        foreach(List<string> listS in currentCSV)
        {
            string joiner = String.Join(" ", listS.ToArray());
            currCSV.Add(joiner);
        }
        string allText = String.Join(Environment.NewLine, currCSV.ToArray());
        System.IO.File.WriteAllText(@finalDest, allText);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
