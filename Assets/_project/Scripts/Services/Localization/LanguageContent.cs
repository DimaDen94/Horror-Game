using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "MultiLanguage/Language Content", order = 51)]
public class LanguageContent : ScriptableObject
{
    [TableList]
    [SerializeField] private List<TranslatableString> _translatableStrings;

    public string GetValueByKey(TranslatableKey key) {
        return  _translatableStrings.Find(trans => trans.key == key).value;
    }

    //[SerializeField] private string _data;

    //[Button]
    //private void SerializeList() {
    //    _translatableStrings = JsonConvert.DeserializeObject<List<TranslatableString>>(_data);
    //}
}
