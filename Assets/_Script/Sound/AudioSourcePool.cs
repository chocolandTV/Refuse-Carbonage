using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    

    private readonly Queue<AudioSource> audioSources = new Queue<AudioSource>();
    private readonly LinkedList<AudioSource> inuse = new LinkedList<AudioSource>();
    private readonly Queue<LinkedListNode<AudioSource>> nodePool = new Queue<LinkedListNode<AudioSource>>();

    private int lastCheckFrame = -1;

    

    private void CheckInUse()
    {
        var node = inuse.First;
        while (node != null)
        {
            var current = node;
            node = node.Next;

            if (!current.Value.isPlaying)
            {
                audioSources.Enqueue(current.Value);
                inuse.Remove(current);
                nodePool.Enqueue(current);
            }
        }
    }

    public void PlayAtPoint(AudioClip clip, Vector3 point, AudioSource prefab, float volume)
    {
        AudioSource source;

        if (lastCheckFrame != Time.frameCount)
        {
            lastCheckFrame = Time.frameCount;
            CheckInUse();
        }

        if (audioSources.Count == 0)
            source = GameObject.Instantiate(prefab);
        else
            source = audioSources.Dequeue();

        if (nodePool.Count == 0)
            inuse.AddLast(source);
        else
        {
            var node = nodePool.Dequeue();
            node.Value = source;
            inuse.AddLast(node);
        }

        source.transform.position = point;
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }
}
