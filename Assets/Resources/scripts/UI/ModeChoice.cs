using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeChoice : MonoBehaviour
{
    [SerializeField] private GameObject modesContainer;
    [SerializeField] private TMP_FontAsset menuFont;
    private List<GameObject> modesPanels = new List<GameObject>();
    private GameObject[] modesPresets;
    private float swipeDistance;
    private int selectedModeCounter = 0;

    private void Start()
    {
        swipeDistance = modesContainer.GetComponent<GridLayoutGroup>().cellSize.x + modesContainer.GetComponent<GridLayoutGroup>().spacing.x;
        modesPresets = Resources.LoadAll("prefab/modes", typeof(GameObject)).Cast<GameObject>().ToArray();
        modeChoiceFill(modesPresets);
        
        modeSet(modesPresets[0].GetComponent<modePresetData>());
    }

    public void modeSet(modePresetData mode)
    {
        PlayerPrefs.DeleteKey("SelectedMode");
        PlayerPrefs.SetFloat("SelectedMode", mode.gameDuration);
    }

    private void modeChoiceFill(GameObject[] modesList)
    {
        foreach (GameObject mode in modesList)
        {
            modePresetData modeInfo = mode.GetComponent<modePresetData>();

            GameObject modePanelBlueprint = new GameObject("panelmode_"+modeInfo.mode);
            modePanelBlueprint.transform.parent = modesContainer.transform;
            
            GridLayoutGroup modePanelGrid = modePanelBlueprint.AddComponent<GridLayoutGroup>();

            modePanelGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            modePanelGrid.constraintCount = 1;
            modePanelGrid.startAxis = GridLayoutGroup.Axis.Vertical;
            modePanelGrid.childAlignment = TextAnchor.MiddleCenter;

            modePanelGrid.cellSize = modesContainer.GetComponent<GridLayoutGroup>().cellSize;
            
            modesPanels.Add(modePanelBlueprint);
            modePanelBlueprint.AddComponent<modePresetData>().gameDuration = modeInfo.gameDuration;
            modePanelBlueprint.AddComponent<CanvasGroup>().alpha = 0;
            
            
            
            GameObject titleObject = new GameObject("titlemode_"+modeInfo.mode);
            titleObject.transform.parent = modePanelBlueprint.transform;
            TextMeshProUGUI modeTitle = titleObject.AddComponent<TextMeshProUGUI>();
            modeTitle.text = modeInfo.mode.ToString();
            modeTitle.alignment = TextAlignmentOptions.Center;
            modeTitle.verticalAlignment = VerticalAlignmentOptions.Bottom;
            modeTitle.color = new Color(0.6f, 0.6f, 0.6f, 1f);
            modeTitle.fontSize = 28;
            modeTitle.font = menuFont;

            GameObject descriptionObject = new GameObject("descmode_"+modeInfo.mode);
            descriptionObject.transform.parent = modePanelBlueprint.transform;
            TextMeshProUGUI modeDescription = descriptionObject.AddComponent<TextMeshProUGUI>();
            modeDescription.text = modeInfo.description;
            modeDescription.alignment = TextAlignmentOptions.Center;
            modeTitle.verticalAlignment = VerticalAlignmentOptions.Middle;
            modeDescription.color = new Color(0.6f, 0.6f, 0.6f, 1f);
            modeDescription.fontSize = 21;
            modeDescription.font = menuFont;
        }

        modesPanels[0].GetComponent<CanvasGroup>().LeanAlpha(1, 0f);
    }

    public void swipeRight()
    {
        if (selectedModeCounter < modesPanels.Count-1)
        {
            selectedModeCounter++;

            modesContainer.LeanMoveX(modesContainer.transform.position.x - swipeDistance, 0.25f).setEaseInOutExpo();
            
            modesPanels[selectedModeCounter - 1].GetComponent<CanvasGroup>().LeanAlpha(0, 0.25f);
            modesPanels[selectedModeCounter].GetComponent<CanvasGroup>().LeanAlpha(1, 0.25f);
            
            modeSet(modesPanels[selectedModeCounter].GetComponent<modePresetData>());
        }
    }

    public void swipeLeft()
    {
        if (selectedModeCounter > 0)
        {
            selectedModeCounter--;

            modesContainer.LeanMoveX(modesContainer.transform.position.x + swipeDistance, 0.25f).setEaseInOutExpo();

            modesPanels[selectedModeCounter].GetComponent<CanvasGroup>().LeanAlpha(1, 0.25f);
            modesPanels[selectedModeCounter + 1].GetComponent<CanvasGroup>().LeanAlpha(0, 0.25f);

            modeSet(modesPanels[selectedModeCounter].GetComponent<modePresetData>());
        }
    }
}