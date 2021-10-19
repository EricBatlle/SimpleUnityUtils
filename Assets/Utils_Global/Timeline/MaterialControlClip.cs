using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class MaterialControlClip : PlayableAsset, ITimelineClipAsset
{
	[SerializeField] private MaterialControlBehaviour template = new MaterialControlBehaviour();

	public ClipCaps clipCaps => ClipCaps.Blending;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<MaterialControlBehaviour>.Create(graph, template);
	}
}
