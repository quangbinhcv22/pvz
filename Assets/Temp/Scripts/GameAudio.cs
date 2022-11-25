using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class GameAudio
{
    static GameAudio()
    {
        Container = new GameObject("audio");
        Object.DontDestroyOnLoad(Container);
    }

    private static readonly GameObject Container;


    private static List<AudioSource> _sources = new();
    private static Dictionary<string, AudioClip> _clips = new();


    public static void PlayRandom(List<string> keys)
    {
        if (!keys.Any()) return;

        var key = keys[Random.Range(default, keys.Count)];
        Play(key);
    }


    public static void Play(string key)
    {
        AudioClip clip;

        if (_clips.ContainsKey(key))
        {
            clip = _clips[key];
        }
        else
        {
            clip = Addressables.LoadAssetAsync<AudioClip>(key).WaitForCompletion();
            _clips.Add(key, clip);
        }


        var source = _sources.FirstOrDefault(source => !source.isPlaying);

        if (!source)
        {
            source = Container.AddComponent<AudioSource>();
            _sources.Add(source);
        }

        source.PlayOneShot(clip);
    }
}