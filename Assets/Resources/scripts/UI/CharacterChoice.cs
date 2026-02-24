
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterChoise : MonoBehaviour
{
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GameObject charactersButtonsPanel;
    [SerializeField] private GameObject characterPreviewPanel;
    [SerializeField] private GameObject characterName;
    [SerializeField] private GameObject characterImage;
    [SerializeField] private GameObject characterDescrition;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSoundEffect;
    [SerializeField] private AudioClip pressedSoundEffect;

    private GameObject[] charactersList;

    private void Start()
    {
        charactersList = Resources.LoadAll("prefab/player", typeof(GameObject)).Cast<GameObject>().ToArray();

        charactersCboiceButtonsFill(charactersList);
    }

    public void CharSet(CharacterInfo Character)
    {
        Character.initBasicCharacteristics();
        characterPreviewPanel.SetActive(true);
        
        PlayerPrefs.DeleteKey("SelectedCharacter");
        PlayerPrefs.SetInt("SelectedCharacter", (int)Character.character);

        characterName.GetComponent<TextMeshProUGUI>().text = Character.name;
        characterImage.GetComponent<Image>().sprite = Character.preview;
        characterImage.SetActive(true);
        characterDescrition.GetComponent<TextMeshProUGUI>().text = Character.desc + "\n" + Character.characteristics;
    }

    private void charactersCboiceButtonsFill(GameObject[] charactersList)
    {
        foreach (GameObject character in charactersList)
        {
            CharacterInfo characterInfo = character.GetComponent<CharacterInfo>();

            GameObject characterPanel = Instantiate(buttonTemplate, charactersButtonsPanel.transform);

            GameObject characterPanelIcon = characterPanel.transform.GetChild(1).gameObject;
            
            characterPanelIcon.GetComponent<Image>().sprite = characterInfo.icon;

            ButtonSoundEffect panelSound = characterPanel.AddComponent<ButtonSoundEffect>();
            panelSound._audioSource = audioSource;
            panelSound.buttonSoundEffect = hoverSoundEffect;
            panelSound.buttonPressedSoundEffect = pressedSoundEffect;

            Button charButton = characterPanel.GetComponent<Button>();
            
            charButton.onClick.AddListener(() =>CharSet(characterInfo));
            charButton.onClick.AddListener(() =>panelSound.pressed());
        }
    }
}
