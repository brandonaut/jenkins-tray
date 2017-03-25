using System;
using System.Collections;
using System.Collections.Generic;

namespace JenkinsTray.Utils.Collections
{
    [Obsolete]
    public class DualPropertiesContainer : IPropertiesContainer
    {
        private readonly PropertiesContainer allProperties;
        private readonly PropertiesContainer defaultProperties;
        private string id;
        private readonly PropertiesContainer userProperties;
        private readonly PropertiesContainer[] writeEnabledContainers;

        public DualPropertiesContainer(string id)
        {
            this.id = id;
            userProperties = new PropertiesContainer("User:" + id, true);
            defaultProperties = new PropertiesContainer("Default:" + id, true);
            allProperties = new PropertiesContainer("All:" + id, true);
            writeEnabledContainers = new[] {userProperties, allProperties};
        }

        // user properties: they can be changed
        internal IPropertiesContainer UserProperties
        {
            get { return userProperties; }
        }

        // default properties: they are read-only
        internal IPropertiesContainer DefaultProperties
        {
            get { return defaultProperties; }
        }

        // user+default properties: they are read-only
        internal IPropertiesContainer Properties
        {
            get { return allProperties; }
        }

        public string this[string key]
        {
            get { return allProperties[key]; }
            set { Apply(delegate(IPropertiesContainer properties) { properties[key] = value; }); }
        }

        public string GetStringValue(string key)
        {
            return allProperties.GetStringValue(key);
        }

        public string GetStringValue(string key, string defaultValue)
        {
            return allProperties.GetStringValue(key, defaultValue);
        }

        public string GetRequiredStringValue(string key)
        {
            return allProperties.GetRequiredStringValue(key);
        }

        public int? GetIntValue(string key)
        {
            return allProperties.GetIntValue(key);
        }

        public int GetIntValue(string key, int defaultValue)
        {
            return allProperties.GetIntValue(key, defaultValue);
        }

        public void SetIntValue(string key, int value)
        {
            Apply(delegate(IPropertiesContainer properties) { properties.SetIntValue(key, value); });
        }

        public int GetRequiredIntValue(string key)
        {
            return allProperties.GetRequiredIntValue(key);
        }

        public float? GetFloatValue(string key)
        {
            return allProperties.GetFloatValue(key);
        }

        public float? GetFloatValue(string key, float defaultValue)
        {
            return allProperties.GetFloatValue(key, defaultValue);
        }

        public float GetRequiredFloatValue(string key)
        {
            return allProperties.GetRequiredFloatValue(key);
        }

        public bool? GetBoolValue(string key)
        {
            return allProperties.GetBoolValue(key);
        }

        public bool GetBoolValue(string key, bool defaultValue)
        {
            return allProperties.GetBoolValue(key, defaultValue);
        }

        public void SetBoolValue(string key, bool value)
        {
            Apply(delegate(IPropertiesContainer properties) { properties.SetBoolValue(key, value); });
        }

        public void RemoveValue(string key)
        {
            Apply(delegate(IPropertiesContainer properties) { properties.RemoveValue(key); });
        }

        public int GetGroupCount(string group)
        {
            return allProperties.GetGroupCount(group);
        }

        public string GetGroupStringValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupStringValue(group, groupId, key);
        }

        public string GetGroupStringValue(string group, int groupId, string key, string defaultValue)
        {
            return allProperties.GetGroupStringValue(group, groupId, key, defaultValue);
        }

        public string GetGroupRequiredStringValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupRequiredStringValue(group, groupId, key);
        }

        public int? GetGroupIntValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupIntValue(group, groupId, key);
        }

        public int? GetGroupIntValue(string group, int groupId, string key, int defaultValue)
        {
            return allProperties.GetGroupIntValue(group, groupId, key, defaultValue);
        }

        public int GetGroupRequiredIntValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupRequiredIntValue(group, groupId, key);
        }

        public float? GetGroupFloatValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupFloatValue(group, groupId, key);
        }

        public float? GetGroupFloatValue(string group, int groupId, string key, float defaultValue)
        {
            return allProperties.GetGroupFloatValue(group, groupId, key, defaultValue);
        }

        public float GetGroupRequiredFloatValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupRequiredFloatValue(group, groupId, key);
        }

        public bool? GetGroupBoolValue(string group, int groupId, string key)
        {
            return allProperties.GetGroupBoolValue(group, groupId, key);
        }

        public bool GetGroupBoolValue(string group, int groupId, string key, bool defaultValue)
        {
            return allProperties.GetGroupBoolValue(group, groupId, key, defaultValue);
        }

        public void SetGroupCount(string group, int count)
        {
            Apply(delegate(IPropertiesContainer properties) { properties.SetGroupCount(group, count); });
        }

        public void SetGroupStringValue(string group, int groupId, string key, string value)
        {
            Apply(
                delegate(IPropertiesContainer properties)
                {
                    properties.SetGroupStringValue(group, groupId, key, value);
                });
        }

        public void SetGroupIntValue(string group, int groupId, string key, int value)
        {
            Apply(delegate(IPropertiesContainer properties) { properties.SetGroupIntValue(group, groupId, key, value); });
        }

        public void SetGroupBoolValue(string group, int groupId, string key, bool value)
        {
            Apply(
                delegate(IPropertiesContainer properties) { properties.SetGroupBoolValue(group, groupId, key, value); });
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return allProperties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return allProperties.GetEnumerator();
        }

        public void CopyPropertiesFrom(IPropertiesContainer properties)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool ReadOnly
        {
            get { throw new Exception("The method or operation is not implemented."); }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public void Clear()
        {
            Apply(delegate(IPropertiesContainer properties) { properties.Clear(); });
        }

        private void Apply(PropertiesWriteActionDelegate action)
        {
            foreach (var container in writeEnabledContainers)
            {
                container.ReadOnly = false;
                action(container);
                container.ReadOnly = true;
            }
        }

        private delegate void PropertiesWriteActionDelegate(IPropertiesContainer properties);
    }
}