using UnityEngine;
using UnityEngine.Playables;

public class MaterialControlMixer : PlayableBehaviour
{
	private Material material;
	private bool firstFrameHappened;

	private Color defaultColor;

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		material = playerData as Material;

		if (material == null)
			return;

		if (!firstFrameHappened)
		{
			defaultColor = material.color;
		}

		int inputCount = playable.GetInputCount();

		Color blendedColor = Color.clear;
		float totalWeight = 0f;

		for (int i = 0; i < inputCount; ++i)
		{
			float inputWeight = playable.GetInputWeight(i);
			ScriptPlayable<MaterialControlBehaviour> inputPlayable = (ScriptPlayable<MaterialControlBehaviour>)playable.GetInput(i);
			MaterialControlBehaviour behaviour = inputPlayable.GetBehaviour();

			blendedColor += behaviour.color * inputWeight;

			totalWeight += inputWeight;
		}

		float remainingWeight = 1 - totalWeight;

		material.color = blendedColor + defaultColor * remainingWeight;
	}

	public override void OnPlayableDestroy(Playable playable)
	{
		firstFrameHappened = false;

		if (material == null)
			return;

		material.color = defaultColor;
	}
}
