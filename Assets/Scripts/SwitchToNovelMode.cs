using Naninovel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CommandAlias("novel")]
public class SwitchToNovelMode : Command {
    public StringParameter ScriptName;
    public StringParameter Label;

    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default) {

        // 2. Switch cameras.
        var miniGameContent = GameObject.Find("MiniGame").transform.GetChild(0).gameObject;
        miniGameContent.SetActive(false);
        var naniCamera = Engine.GetService<ICameraManager>().Camera;
        naniCamera.enabled = true;

        // 3. Load and play specified script (if assigned).
        if (Assigned(ScriptName)) {
            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            await scriptPlayer.PreloadAndPlayAsync(ScriptName, label: Label);
        }

        // 4. Enable Naninovel input.
        var inputManager = Engine.GetService<IInputManager>();
        inputManager.ProcessInput = true;
    }
}
