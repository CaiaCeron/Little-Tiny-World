using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderMaster : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        AudioManager.instance.MasterVolumeControl(slider.value);
        slider.onValueChanged.AddListener(volume => AudioManager.instance.MasterVolumeControl(volume));
    }
}
