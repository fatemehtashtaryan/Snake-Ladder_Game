using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource stepAudio;
    [SerializeField] private AudioSource fallAudio;
    [SerializeField] private AudioSource blackHoleAudio;
    [SerializeField] private AudioSource SnakeAudio;

    private void Start()
    {
        Instance = this;
    }

    public void PlayStep_Audio(){
        if (stepAudio != null)
        {
            stepAudio.pitch = Random.Range(1.1f, 1.8f);
            stepAudio.Play();  
            stepAudio.SetScheduledEndTime(AudioSettings.dspTime + 0.7);
        }
    }

    public void Play_ReturnToStart_Audio(){
        if (fallAudio != null)
        {   
            // fallAudio.time = 0.9f;
            // fallAudio.pitch = 0.9f; 
            // fallAudio.Play();
            stepAudio.pitch = 0.9f;
            stepAudio.Play();  
            stepAudio.SetScheduledEndTime(AudioSettings.dspTime + 0.7);
        }
    }

    public void Play_BlackHole_Audio(){
        double startTime = AudioSettings.dspTime + 0.1;  
        double clipStart = 1.0f;                         
        double clipEnd   = 2.0f;                        
        blackHoleAudio.pitch = 2f; 
        blackHoleAudio.time = (float)clipStart;            
        blackHoleAudio.PlayScheduled(startTime);            
        blackHoleAudio.SetScheduledEndTime(startTime + (clipEnd - clipStart));
    }

    public void PlaySnake_Audio(){
        if (SnakeAudio != null)
        {
            stepAudio.pitch = Random.Range(1.1f, 1.8f);
            stepAudio.Play();  
            stepAudio.SetScheduledEndTime(AudioSettings.dspTime + 0.7);
        }
    }
}
