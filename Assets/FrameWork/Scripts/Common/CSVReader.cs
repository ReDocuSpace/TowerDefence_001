using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class CSVReader
{
    private readonly static string streamingPATH = string.Format(Application.streamingAssetsPath);

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }

    public static List<Dictionary<string, object>> AssetRead(string file)
    {
        var list = new List<Dictionary<string, object>>();
        string path = string.Format(streamingPATH + "/CSVData/" + file + ".csv");

        string[] header;

        Debug.Log(path);

        using (StreamReader sr = new StreamReader(path))
        {
            string headerLine = sr.ReadLine();

            if (string.IsNullOrEmpty(headerLine)) return null;

            header = headerLine.Split(',');

            while(!sr.EndOfStream)
            {
                var dataString = sr.ReadLine();
                var dataValues = dataString.Split(',');

                Dictionary<string, object> data = new Dictionary<string, object>();

                for(int i = 0;i < dataValues.Length ;i++)
                {
                    data.Add(header[i], dataValues[i]);
                }
                list.Add(data);
            }

            sr.Close();
        }

        Debug.Log("CSV Read File : " + file);

        return list;
    }
}
