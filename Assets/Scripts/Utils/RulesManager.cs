using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RulesManager {

    private Dictionary<string, IRuleset> Rulesets = new Dictionary<string, IRuleset>();

    private static RulesManager _instance = new RulesManager();
    public static RulesManager Instance
    {
        get { return _instance; }
    }

    private RulesManager() { /*DENIED*/ }

    public bool register(string key, IRuleset value)
    {
        if (!Rulesets.ContainsKey(key) )
        {
            Rulesets.Add(key, value);
            return true;
        }

        return false;
    }

    public bool getRules(string key, ref IRuleset rules)
    {
        return Rulesets.TryGetValue(key, out rules);
    }

    public int Count()
    {
        return Rulesets.Count;
    }

    void Awake()
    {
        _instance = this;
    }

    void OnDestroy()
    {
        _instance = null;
    }
}
