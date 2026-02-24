using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageChoice : MonoBehaviour
{
    [SerializeField] private GameObject stagesContainer;
    [SerializeField] private TMP_FontAsset menuFont;
    private List<GameObject> stagesPanels = new List<GameObject>();
    private GameObject[] stagesPresets;
    private float swipeDistance;
    private int selectedStageCounter = 0;

    private void Start()
    {
        PlayerPrefs.DeleteKey("SelectedStage");
        swipeDistance = stagesContainer.GetComponent<GridLayoutGroup>().cellSize.x + stagesContainer.GetComponent<GridLayoutGroup>().spacing.x;
        stagesPresets = Resources.LoadAll("prefab/stages", typeof(GameObject)).Cast<GameObject>().ToArray();
        charactersChoiceButtonsFill(stagesPresets);
    }

    public void StageSet(stagePresetData stage)
    {
        PlayerPrefs.DeleteKey("SelectedStage");
        PlayerPrefs.SetInt("SelectedStage", (int)stage.stage);
    }

    private void charactersChoiceButtonsFill(GameObject[] stagesList)
    {
        foreach (GameObject stage in stagesList)
        {
            stagePresetData stageInfo = stage.GetComponent<stagePresetData>();

            GameObject stagePanelBlueprint = new GameObject("panelStage_"+stageInfo.title);
            stagePanelBlueprint.transform.parent = stagesContainer.transform;
            
            GridLayoutGroup stagePanelGrid = stagePanelBlueprint.AddComponent<GridLayoutGroup>();

            stagePanelGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            stagePanelGrid.constraintCount = 1;
            stagePanelGrid.startAxis = GridLayoutGroup.Axis.Vertical;
            stagePanelGrid.childAlignment = TextAnchor.MiddleCenter;

            stagePanelGrid.cellSize = stagesContainer.GetComponent<GridLayoutGroup>().cellSize;
            
            stagesPanels.Add(stagePanelBlueprint);
            stagePanelBlueprint.AddComponent<stagePresetData>().stage = stageInfo.stage;
            stagePanelBlueprint.AddComponent<CanvasGroup>().alpha = 0;
            
            
            
            GameObject titleObject = new GameObject("titleStage_"+stageInfo.title);
            titleObject.transform.parent = stagePanelBlueprint.transform;
            TextMeshProUGUI stageTitle = titleObject.AddComponent<TextMeshProUGUI>();
            stageTitle.text = stageInfo.title;
            stageTitle.alignment = TextAlignmentOptions.Center;
            stageTitle.verticalAlignment = VerticalAlignmentOptions.Bottom;
            stageTitle.color = new Color(0.6f, 0.6f, 0.6f, 1f);
            stageTitle.fontSize = 28;
            stageTitle.font = menuFont;

            
            
            GameObject imageObject = new GameObject("imageStage_"+stageInfo.title);
            imageObject.transform.parent = stagePanelBlueprint.transform;
            Image stageImage = imageObject.AddComponent<Image>();
            stageImage.sprite = stageInfo.icon;

            
            
            GameObject descriptionObject = new GameObject("descStage_"+stageInfo.title);
            descriptionObject.transform.parent = stagePanelBlueprint.transform;
            TextMeshProUGUI stageDescription = descriptionObject.AddComponent<TextMeshProUGUI>();
            stageDescription.text = stageInfo.desc;
            stageDescription.alignment = TextAlignmentOptions.Center;
            stageTitle.verticalAlignment = VerticalAlignmentOptions.Middle;
            stageDescription.color = new Color(0.6f, 0.6f, 0.6f, 1f);
            stageDescription.fontSize = 21;
            stageDescription.font = menuFont;
        }

        stagesPanels[0].GetComponent<CanvasGroup>().LeanAlpha(1, 0f);
    }

    public void swipeRight()
    {
        if (selectedStageCounter < stagesPanels.Count-1)
        {
            selectedStageCounter++;

            stagesContainer.LeanMoveX(stagesContainer.transform.position.x - swipeDistance, 0.25f).setEaseInOutExpo();
            
            stagesPanels[selectedStageCounter - 1].GetComponent<CanvasGroup>().LeanAlpha(0, 0.25f);
            stagesPanels[selectedStageCounter].GetComponent<CanvasGroup>().LeanAlpha(1, 0.25f);
            
            StageSet(stagesPanels[selectedStageCounter].GetComponent<stagePresetData>());
        }
    }

    public void swipeLeft()
    {
        if (selectedStageCounter > 0)
        {
            selectedStageCounter--;

            stagesContainer.LeanMoveX(stagesContainer.transform.position.x + swipeDistance, 0.25f).setEaseInOutExpo();

            stagesPanels[selectedStageCounter].GetComponent<CanvasGroup>().LeanAlpha(1, 0.25f);
            stagesPanels[selectedStageCounter + 1].GetComponent<CanvasGroup>().LeanAlpha(0, 0.25f);

            StageSet(stagesPanels[selectedStageCounter].GetComponent<stagePresetData>());
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}