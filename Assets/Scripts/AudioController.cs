using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering.Universal;
public enum SoundType
{
    knock,
    open,
    close,
    applause,
    hit,
    hitBig,
    score
}

[System.Serializable]
public class SoundEntry
{
    public SoundType type;
    public AudioSource source;
}

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [Header("SFXs")]
    [SerializeField] private SoundEntry[] sounds; //multiples audiosources per si volem escoltar 2 sons a la vegada.


    [Header("Music (piano live concert)")]
    [SerializeField] private AudioSource musicSource; //1 audiosoucre per les cançons
    [SerializeField] private AudioClip[] musicClips;  //Totes les cançons
    [SerializeField] private float applauseDelay = 0.5f;
    [SerializeField] private float delayBeforeNextSong = 1f;

    //Un cop ha acabat torni a repetir les cançons amb el mateix ordre, així estic segur que la cançó que escoltarà fa un bon rato que no l'escolta.
    private List<AudioClip> playOrder = new List<AudioClip>(); 
    private int currentIndex = 0;

    //Sigleton: us d'un manager per tenir tots els audiosource centralitzats.
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //SFXs
    public void Play(SoundType soundType, bool randomizePitch = false, Transform playPos = null)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].type == soundType)
            {
                if (sounds[i].source != null)
                {
                    //Sonido 3D
                    Vector3 pos = sounds[i].source.transform.position;
                    if (playPos) sounds[i].source.transform.position = playPos.position;

                    //Randomiza el pitch
                    if (randomizePitch)
                        sounds[i].source.pitch = Random.Range(0.8f, 1.2f);
                    else
                        sounds[i].source.pitch = 1.0f;

                    sounds[i].source.Play();
                }
                return;
            }
        }
    }

    //Música
    private void Start()
    {
        if (musicClips.Length > 0 && musicSource != null)
        {
            GenerateShuffledPlaylist();
            StartCoroutine(PlayMusicLoop());
        }
    }

    private void GenerateShuffledPlaylist()
    {
        playOrder = new List<AudioClip>(musicClips);

        for (int i = 0; i < playOrder.Count; i++)
        {
            int j = Random.Range(i, playOrder.Count);
            (playOrder[i], playOrder[j]) = (playOrder[j], playOrder[i]);
        }
    }

    private IEnumerator PlayMusicLoop()
    {
        while (true)
        {
            if (playOrder.Count == 0) yield break;

            //Play a la següent cançó
            AudioClip clip = playOrder[currentIndex];

            //Fes play
            musicSource.clip = clip;
            musicSource.Play();

            //Següent cançó
            currentIndex = (currentIndex + 1) % playOrder.Count;

            //Espera a que acabi
            yield return new WaitWhile(() => musicSource.isPlaying);

            //Delay abans de l'aplaudiment
            yield return new WaitForSeconds(applauseDelay);
            PlayRandomApplause();

            //Delay per la seguent cançó
            yield return new WaitForSeconds(delayBeforeNextSong);
        }
    }

    private void PlayRandomApplause()
    {
        List<SoundEntry> applauseSources = new List<SoundEntry>();

        foreach (var s in sounds)
        {
            if (s.type == SoundType.applause && s.source != null)
            {
                applauseSources.Add(s);
            }
        }

        if (applauseSources.Count == 0) return;

        int index = Random.Range(0, applauseSources.Count);
        applauseSources[index].source.Play();
    }
}

