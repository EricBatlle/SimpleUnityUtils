using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
[TrackColor(241f / 255f, 249f / 255f, 99f / 255f)]
[TrackBindingType(typeof(Material))]
[TrackClipType(typeof(MaterialControlClip))]
public class MaterialControlTrack : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		return ScriptPlayable<MaterialControlMixer>.Create(graph, inputCount);
	}
}
