using System;
using System.Collections; // For IEnumerable
using System.Collections.Generic;
using UnityEngine;

// This generic class can be reused for any key-value types you need.
// Place this in a general 'Utilities' or 'Core' folder.
[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver, IDictionary<TKey, TValue>
{
    [SerializeField]
    private List<TKey> _keys = new List<TKey>();
    [SerializeField]
    private List<TValue> _values = new List<TValue>();

    private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

    // --- ISerializationCallbackReceiver Methods ---
    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();
        foreach (var pair in _dictionary)
        {
            _keys.Add(pair.Key);
            _values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        _dictionary.Clear();
        for (int i = 0; i < _keys.Count; i++)
        {
            // Handle potential duplicate keys (e.g., from manual Inspector edits or external data)
            if (i < _values.Count) // Ensure we have a corresponding value
            {
                if (!_dictionary.ContainsKey(_keys[i]))
                {
                    _dictionary.Add(_keys[i], _values[i]);
                }
                else
                {
                    // If a duplicate key is found, you might choose to overwrite or log a warning.
                    // For Q&A, you likely want unique questions.
                    Debug.LogWarning($"Duplicate key '{_keys[i]}' found during deserialization. Skipping this Q&A entry.");
                }
            }
        }
    }

    // --- IDictionary Implementation (Pass-through to the internal dictionary) ---
    // These methods allow you to use this class just like a normal Dictionary.
    public TValue this[TKey key]
    {
        get { return _dictionary[key]; }
        set { _dictionary[key] = value; }
    }

    public ICollection<TKey> Keys => _dictionary.Keys;
    public ICollection<TValue> Values => _dictionary.Values;
    public int Count => _dictionary.Count;
    public bool IsReadOnly => ((IDictionary<TKey, TValue>)_dictionary).IsReadOnly;

    public void Add(TKey key, TValue value) { _dictionary.Add(key, value); }
    public bool ContainsKey(TKey key) { return _dictionary.ContainsKey(key); }
    public bool Remove(TKey key) { return _dictionary.Remove(key); }
    public bool TryGetValue(TKey key, out TValue value) { return _dictionary.TryGetValue(key, out value); }

    public void Add(KeyValuePair<TKey, TValue> item) { ((IDictionary<TKey, TValue>)_dictionary).Add(item); }
    public void Clear() { _dictionary.Clear(); }
    public bool Contains(KeyValuePair<TKey, TValue> item) { return ((IDictionary<TKey, TValue>)_dictionary).Contains(item); }
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { ((IDictionary<TKey, TValue>)_dictionary).CopyTo(array, arrayIndex); }
    public bool Remove(KeyValuePair<TKey, TValue> item) { return ((IDictionary<TKey, TValue>)_dictionary).Remove(item); }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() { return _dictionary.GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return _dictionary.GetEnumerator(); }
}