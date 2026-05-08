using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource stepAudio;
    [SerializeField] private AudioSource fallAudio;
    [SerializeField] private AudioSource blackHoleAudio;

    public void PlayStep_Audio(){
        if (stepAudio != null)
        {
            stepAudio.pitch = Random.Range(1.1f, 1.8f);
            stepAudio.Play();  
            stepAudio.SetScheduledEndTime(AudioSettings.dspTime + 0.7);
        }
    }
    public void Play_ReturnToStart_Audio(){
        if (blackHoleAudio != null)
        {
            blackHoleAudio.time = 0.9f;
            fallAudio.pitch = 0.9f; 
            fallAudio.Play();
        }
    }
    public void Play_BlackHole_Audio(){
        double startTime = AudioSettings.dspTime + 0.1;  
        double clipStart = 1.0f;                         
        double clipEnd   = 2.0f;                        

        stepAudio.time = (float)clipStart;            
        stepAudio.PlayScheduled(startTime);            
        stepAudio.SetScheduledEndTime(startTime + (clipEnd - clipStart));
    }
}
