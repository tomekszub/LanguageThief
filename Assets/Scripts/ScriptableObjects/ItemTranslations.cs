using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemTranslations", menuName = "Create ItemTranslations", order = 0)]
public class ItemTranslations : SerializedScriptableObject
{
    [SerializeField] Dictionary<string, Dictionary<string, string>> _Translations;

    [Button]
    void FillDictionaryFromCSVFile(TextAsset csv)
    {
        _Translations = new();
        var content = csv.text;

        string[] lines = content.Split('\n');
        string[] columnNames = lines[0].Split(new char[] { ',' });
        string[] cells;
        int cellLimit;

        for (int i = 1; i < lines.Length; i++) 
        {
            cells = lines[i].Split(new char[] { ',' });

            if (string.IsNullOrEmpty(cells[0]))
            {
                Debug.LogError($"Name of item number {i} is empty!");
                continue;
            }

            _Translations.Add(cells[0], new());
            cellLimit = Mathf.Min(columnNames.Length, cells.Length);

            for (int j = 1; j < cellLimit; j++) 
            {
                if(!string.IsNullOrEmpty(cells[j]))
                {
                    _Translations[cells[0]].Add(columnNames[j], cells[j]);
                }
            }
        }
    }
}
