using UnityEngine;

public class QualitySetter : MonoBehaviour
{
    public string low_preset_name;
    public string med_preset_name;
    public string high_preset_name;

    private int find_quality_lvl_idx(string name)
    {
        for (int i = 0; i < name.Length; i++)
            if (name == QualitySettings.names[i])
                return i;

        return -1;
    }

    private bool set_quality(string name)
    {
        int idx = find_quality_lvl_idx(name);
        if (idx < 0)
            return false;

        QualitySettings.SetQualityLevel(idx, true);
        return true;
    }

    public void set_quality_low()
    {
        set_quality(low_preset_name);
    }

    public void set_quality_med()
    {
        set_quality(med_preset_name);
    }

    public void set_quality_high()
    {
        set_quality(high_preset_name);
    }

    private void Start()
    {
        set_quality_low();
    }
}
