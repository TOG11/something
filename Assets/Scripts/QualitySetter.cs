using UnityEngine;

public class QualitySetter : MonoBehaviour
{
    public string quality_str;

    void Start()
    {
        string[] quality_names = QualitySettings.names;

        bool quality_found = false;
        for (int i = 0; i < quality_names.Length; i++)
        {
            if (quality_str == quality_names[i])
            {
                QualitySettings.SetQualityLevel(i, true);
                quality_found = true;
            }
        }

        if (!quality_found)
        {
            string avail_names = "";
            foreach (string name in quality_names)
                avail_names += "'" + name + "', ";
            avail_names = avail_names.TrimEnd(',', ' ');

            Debug.LogWarning("Failed to set quality to '" + quality_str +
                "'.\nAvailable qualities: " + avail_names + ".");
        }
    }
}
