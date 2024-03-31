using Naninovel;
using Naninovel.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CommandAlias("miniGame")]
public class SwitchToMiniGame : Command
{
    [ParameterAlias("reset")]
    public BooleanParameter ResetState = true;
    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default) {

        var inputManager = Engine.GetService<IInputManager>();
        inputManager.ProcessInput = false;

        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        scriptPlayer.Stop();

        var hidePrinter = new HidePrinter();
        hidePrinter.ExecuteAsync(asyncToken).Forget();

        // 4. Reset state (if required).
        if (ResetState) {
            var stateManager = Engine.GetService<IStateManager>();
            await stateManager.ResetStateAsync();
        }

        var naniCamera = Engine.GetService<ICameraManager>().Camera;
        naniCamera.enabled = false;

        var miniGameContent = GameObject.Find("MiniGame").transform.GetChild(0).gameObject;
        miniGameContent.SetActive(true);
        //SceneManager.LoadScene(sceneName: "MiniGameScene");

    }
}
