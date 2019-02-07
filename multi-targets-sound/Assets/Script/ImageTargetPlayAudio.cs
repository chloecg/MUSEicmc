using UnityEngine;
using Vuforia;

public class ImageTargetPlayAudio : MonoBehaviour,
ITrackableEventHandler 
{

	public bool isPlaying;
	private TrackableBehaviour mTrackableBehaviour;
	private AudioSource mCurrentAudio;


	void Start()
	{
		mCurrentAudio = null;
		isPlaying = false;
		GetComponent<AudioSource>().Stop();

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{

		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			if (mCurrentAudio != null) {
				mCurrentAudio.Stop ();
				isPlaying = false;
			}	
			else
			{
				mCurrentAudio = GetComponent<AudioSource> ();
			}

		}
		else if(newStatus == TrackableBehaviour.Status.NOT_FOUND)
		{

			if (mCurrentAudio != null && !isPlaying) { // if an audio is saved and nothing is playing
				mCurrentAudio.Play ();
				isPlaying = true;
			}
		}
	}   
}