using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using NAudio.Lame;
using NAudio.Wave;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class AudioRecorder : MonoBehaviour
{

    [SerializeField] private GigaChatTest gigaChatTest;
    [SerializeField] private UnityEvent stopRecordEvent;
    private AudioClip recordedClip;
    private AudioSource audioSource;
    private string outputPath;
    private int sampleRate;
    private bool isRecording;
    
    void Start()
    {
        outputPath = Path.Combine(Application.dataPath, "RecordedAudio", "recordedAudio.wav");
        sampleRate = AudioSettings.outputSampleRate;
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeRecordState()
    {
        if (!isRecording)
            StartRecording();
        else
            StopRecording();
    }

    private void StartRecording()
    {
        audioSource.clip = Microphone.Start(Microphone.devices[0], false, 30, sampleRate);
        recordedClip = audioSource.clip;
        audioSource.Play();
        isRecording = true;
        StartCoroutine(StopRecordingAfterTimeout());
    }

    private void StopRecording()
    {
        Microphone.End(Microphone.devices[0]);

        isRecording = false;
        
        if (recordedClip == null)
        {
            Debug.LogError("No audio recorded!");
            return;
        }
        
        StopAllCoroutines();

        Directory.CreateDirectory(Application.dataPath + "/RecordedAudio");
        SaveToWAV(recordedClip, outputPath);
        
        stopRecordEvent.Invoke();
    }

    public void SaveToWAV(AudioClip audioClip, string filePath)
    {
        // Проверяем, существует ли аудиоклип
        if (audioClip == null)
        {
            Debug.LogError("AudioClip is null.");
            return;
        }

        // Получаем данные аудиоклипа
        float[] samples = new float[audioClip.samples * audioClip.channels];
        audioClip.GetData(samples, 0);

        // Количество каналов
        int numChannels = audioClip.channels;
        // Частота дискретизации
        int sampleRate = audioClip.frequency;

        // Создаем WAV заголовок
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            using (var writer = new BinaryWriter(fileStream))
            {
                writer.Write(new char[4] { 'R', 'I', 'F', 'F' });
                writer.Write(36 + samples.Length * 2);
                writer.Write(new char[4] { 'W', 'A', 'V', 'E' });
                writer.Write(new char[4] { 'f', 'm', 't', ' ' });
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)numChannels);
                writer.Write(sampleRate);
                writer.Write(sampleRate * 2 * numChannels);
                writer.Write((ushort)(2 * numChannels));
                writer.Write((ushort)16);

                writer.Write(new char[4] { 'd', 'a', 't', 'a' });
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * 32767f));
                }
            }
        }

        Debug.Log("AudioClip saved to WAV: " + filePath);
    }
    
    private IEnumerator StopRecordingAfterTimeout()
    {
        yield return new WaitForSeconds(15);
        StopRecording();
    }
}
