using Naninovel;
using System;
using UnityEngine;

public class QuestLogController : MonoBehaviour {
    [SerializeField] private string customVariableName;
    [SerializeField] private GameObject questPanel;
    public string CustomVariableName { get => customVariableName; set => customVariableName = value; }
    private ICustomVariableManager variableManager;
    private Transform questPanelTransform;
    private void Awake() {
        variableManager = Engine.GetService<ICustomVariableManager>();
        questPanelTransform = questPanel.transform;
    }

    private void Start() {
        variableManager.OnVariableUpdated += HandleVariableUpdated;
    }

    private void HandleVariableUpdated(CustomVariableUpdatedArgs args) {
        if (!args.Name.EqualsFastIgnoreCase(CustomVariableName)) return;
        if (!string.IsNullOrEmpty(args.Value) && ParseUtils.TryInvariantInt(args.Value, out var intValue)) {
            if (intValue < 0) return;
            if (intValue == questPanelTransform.childCount) {
                questPanelTransform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            if (intValue == 1) {
                questPanelTransform.GetChild(0).gameObject.SetActive(true);
            }
            if (intValue > 1) {
                questPanelTransform.GetChild(intValue - 1).GetChild(0).gameObject.SetActive(true);
            }
            questPanelTransform.GetChild(intValue).gameObject.SetActive(true);
        }
    }
}