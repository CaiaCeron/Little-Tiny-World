using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SaveSlots : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;

    [SerializeField] private GameObject hasDataContent;

    [SerializeField] private TextMeshProUGUI playerPositionText;

    private Button saveSlotButon;

    private void Awake()
    {
        saveSlotButon = GetComponent<Button>();
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButon.interactable = interactable;
    }


    public void SetData(GameData data)
    {
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            playerPositionText.text = "Player Position: " + data.playerPosition;

        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }

}
