using DTT.MinigameMemory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private MemoryGameManager gameManager;
    [SerializeField] private MemoryGameSettings gameSettings;
    [SerializeField] private string novelScriptName;
    private void OnEnable() {
        gameManager.Finish += OnGameFinished;
    }
    private void Start()
    {
       gameManager.StartGame(gameSettings);
    }

    private void OnGameFinished(MemoryGameResults results) {
        var switchCommand = new SwitchToNovelMode { ScriptName = novelScriptName};
        ICustomVariableManager variableManager = Engine.GetService<ICustomVariableManager>();
        variableManager.SetVariableValue("miniGame", "true");
        switchCommand.ExecuteAsync().Forget();
    }
}
