using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    private Transform _sfxParent;
    [SerializeField]
    private AudioClip _uiButton;
    [SerializeField]
    private float _uiButtonVolume = 1;
    [SerializeField]
    private AudioClip _milestoneRached;
    [SerializeField]
    private float _milestoneRachedVolume= 1;
    [SerializeField]
    private AudioClip _buildingSelect;
    [SerializeField]
    private float _buildingSelectVolume = 1;
    [SerializeField]
    private AudioClip _spawnBuilding;
    [SerializeField]
    private float _spawnBuildingVolume= 1;
    [SerializeField]
    private List<AudioClip> _buildingImpact;
    [SerializeField]
    private float _buildingImpactVolume= 1;
    [SerializeField]
    private List<AudioClip> _buildingDestroy;
    [SerializeField]
    private float _buildingDestroyVolume= 1;
    public static SFXManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void PlayUIButton()
    {
        CreateSound(_uiButton, _uiButtonVolume);
    }

    public void PlayMilestone()
    {
        CreateSound(_milestoneRached, _milestoneRachedVolume);
    }
    public void PlayBuildingSelect()
    {
        CreateSound(_buildingSelect, _buildingSelectVolume);
    }
    public void PlaySpawnBuilding()
    {
        CreateSound(_spawnBuilding, _spawnBuildingVolume);
    }
    public void PlayBuildingImpact()
    {
        CreateRandomSound(_buildingImpact, _buildingImpactVolume);
    }
    public void PlayBuildingDestroy()
    {
        CreateRandomSound(_buildingDestroy, _buildingDestroyVolume);
    }

    private void CreateRandomSound(List<AudioClip> clips, float volume)
    {
        int randomIndex = Random.Range(1, clips.Count-1);
        CreateSound(clips[randomIndex], volume);
    }

    private void CreateSound(AudioClip clip, float volume)
    {
        GameObject sfx = new GameObject("SFX");
        sfx.transform.parent = _sfxParent;
        AudioSource source = sfx.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        float destructionTime = clip.length +0.1f;
        StartCoroutine(DestroyAudioSource(sfx, destructionTime));
    }

    private IEnumerator DestroyAudioSource(GameObject audioSouce, float destructionTime)
    {
        yield return new WaitForSeconds(destructionTime);
        Destroy(audioSouce);
    }
}
