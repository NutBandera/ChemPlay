﻿//======================================================================
//        Copyright (C) 2015-2020 Winddy He. All rights reserved
//        Email: hgplan@126.com
//======================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace WindJson
{
    public class JsonNode
    {
        public virtual string Value { get; set; }
        public virtual string Key   { get; set; }
        public virtual int    Count { get; private set; }

        public virtual JsonNode this[int nIndex] { get { return null; } set { } }
        public virtual JsonNode this[string rKey]{ get { return null; } set { } }
        public virtual JsonNode Node             { get; set; }
        
        public virtual void Add(string rKey, JsonNode rItem) {}
        public virtual void Add(JsonNode rItem)  {}

        public virtual JsonNode Remove(string rKey)    { return null;  }
        public virtual JsonNode Remove(int nIndex)     { return null;  }
        public virtual JsonNode Remove(JsonNode rNode) { return rNode; }

        public override string    ToString()           { return base.ToString();    }
        public virtual object     ToObject(Type rType) { return null;               }
        public T                  ToObject<T>()        { return (T)ToObject(typeof(T));              }
        public List<T>            ToList<T>()          { return (List<T>)ToObject(typeof(List<T>));  }
        public T[]                ToArray<T>()         { return (T[])ToObject(typeof(T[]));          }

        public Dict<TKey, TValue> ToDict<TKey, TValue>() 
        { 
            return (Dict<TKey, TValue>)ToObject(typeof(Dict<TKey, TValue>));  
        }
        public Dictionary<TKey, TValue> ToDictionary<TKey, TValue>() 
        {
            return (Dictionary<TKey, TValue>)ToObject(typeof(Dictionary<TKey, TValue>));
        }

        public virtual int AsInt
        {
            get { return CastInt(Value);    }
            set { Value = value.ToString(); }
        }

        public virtual long AsLong
        {
            get { return CastLong(Value);   }
            set { Value = value.ToString(); }
        }

        public virtual float AsFloat
        {
            get { return CastFloat(Value);  }
            set { Value = value.ToString(); }
        }

        public virtual double AsDouble
        {
            get { return CastDouble(Value); }
            set { Value = value.ToString(); }
        }

        public virtual bool AsBool
        {
            get { return CastBool(Value);   }
            set { Value = value.ToString(); }
        }

        public virtual string AsString
        {
            get { return Value;  }
            set { Value = value; }
        }

        public int CastInt(string value)
        {
            int re = 0;
            if (int.TryParse(value, out re)) return re;
            Debug.LogError(string.Format("Value: {0} is not int type.", value));
            return re;
        }

        public long CastLong(string value)
        {
            long re = 0;
            if (long.TryParse(value, out re)) return re;
            Debug.LogError(string.Format("Value: {0} is not int type.", value));
            return re;
        }

        public float CastFloat(string value)
        {
            float re = 0;
            if (float.TryParse(value, out re)) return re;
            Debug.LogError(string.Format("Value: {0} is not int type.", value));
            return re;
        }

        public double CastDouble(string value)
        {
            double re = 0;
            if (double.TryParse(value, out re)) return re;
            Debug.LogError(string.Format("Value: {0} is not int type.", value));
            return re;
        }

        public bool CastBool(string value)
        {
            if (value == "false" || value == "true")
                return value == "false" ? false : true;
            else
                return CastInt(value) == 0 ? false : true;
        }

        public object CastEnum(Type type, string value)
        {
            //if enum is number，and return number
            int re = 0;
            if (int.TryParse(value, out re)) return re;
            
            // if enum is not number, convert it to enum
            return Enum.Parse(type, value, true);
        }
    }

    public class JsonArray : JsonNode, IEnumerable
    {
        private List<JsonNode> list = new List<JsonNode>();

        public override JsonNode this[int nIndex]
        {
            get
            {
                if (nIndex >= 0 && nIndex < Count)  return list[nIndex];
                Debug.LogError(string.Format("Index out of size limit, Index = {0}, Count = {1}", nIndex, Count));
                return null;
            }
            set
            {
                if (nIndex >= Count)
                    list.Add(value);
                else if (nIndex >= 0 && nIndex < Count)
                    list[nIndex] = value;
            }
        }

        public override int Count { get { return list.Count; } }

        public override void Add(JsonNode rItem)
        {
            if (!list.Contains(rItem))
                list.Add(rItem);
        }

        public override JsonNode Remove(int nIndex)
        {
            if (nIndex < 0 || nIndex >= Count)
                return null;
            JsonNode tmp = list[nIndex];
            list.RemoveAt(nIndex);
            return tmp;
        }

        public override JsonNode Remove(JsonNode rNode)
        {
            list.Remove(rNode);
            return rNode;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var rNode in list)
            {
                yield return rNode;
            }
        }

        public override string ToString()
        {
            string jsonStr = "[";
            for (int i = 0; i < list.Count - 1; i++)
            {
                jsonStr += list[i].ToString();
                jsonStr += ",";
            }
			jsonStr += list.Count == 0 ? "" : list[list.Count - 1].ToString();
            jsonStr += "]";
            return jsonStr;
        }

        public override object ToObject(Type rType)
        {
            if (rType.IsArray)
            {
                Array rObject = Array.CreateInstance(rType.GetElementType(), this.Count);
                Type rArrayElemType = rType.GetElementType();
                for (int i = 0; i < this.Count; i++)
                {
                    object rValue = this.list[i].ToObject(rArrayElemType);
                    rObject.SetValue(rValue, i);
                }
                return rObject;
            }
            else if (rType.IsGenericType && typeof(IList).IsAssignableFrom(rType.GetGenericTypeDefinition()))  //是否为泛型
            {
                IList rObject = (IList)ReflectionAssist.CreateInstance(rType, BindingFlags.Default);
                Type[] rArgsTypes = rType.GetGenericArguments();
                for (int i = 0; i < this.Count; i++)
                {
                    object rValue = this.list[i].ToObject(rArgsTypes[0]);
                    rObject.Add(rValue);
                }
                return rObject;
            }
            return null;
        }
    }

    public class JsonClass : JsonNode, IEnumerable
    {
        private Dict<string, JsonNode> dict = new Dict<string, JsonNode>();

        public override JsonNode this[string rKey]
        {
            get
            {
                JsonNode rNode = null;
                dict.TryGetValue(rKey, out rNode);
                return rNode;
            }
            set
            {
                if (dict.ContainsKey(rKey))
                    dict[rKey] = value;
                else
                    dict.Add(rKey, value);
            }
        }

        public override int Count { get { return dict.Count; } }

        public override void Add(string rKey, JsonNode rItem)
        {
            if (!string.IsNullOrEmpty(rKey))
            {
                if (dict.ContainsKey(rKey))
                    dict[rKey] = rItem;
                else
                    dict.Add(rKey, rItem);
            }
            else
            {
                Debug.LogError("JsonClass dict cannot Add empty string key.");
            }
        }

        public override JsonNode Remove(string rKey)
        {
            if (!dict.ContainsKey(rKey)) return null;
            JsonNode rNode = dict[rKey];
            dict.Remove(rKey);
            return rNode;
        }

        public override JsonNode Remove(JsonNode rNode)
        {
            return base.Remove(rNode);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var rItem in dict)
                yield return rItem;
        }

        public override string ToString()
        {
            string jsonStr = "{";
            int i = 0;
            foreach (var rItem in dict)
            {
                jsonStr += "\"" + rItem.Key + "\":" + rItem.Value.ToString();
                if (i < Count - 1) jsonStr += ",";
                i++;
            }
            jsonStr += "}";
            return jsonStr;
        }

        public override object ToObject(Type rType)
        {
            if (rType.IsGenericType && typeof(IDictionary).IsAssignableFrom(rType.GetGenericTypeDefinition()))
            {
                // Special process type of IDictionary<,>
                IDictionary rObject = (IDictionary)ReflectionAssist.CreateInstance(rType, BindingFlags.Default);
                Type[] rArgsTypes = rType.GetGenericArguments();
                foreach (var rItem in this.dict)
                {
                    object rKey = GetKey_ByString(rArgsTypes[0], rItem.Key);
                    object rValue = rItem.Value.ToObject(rArgsTypes[1]);
                    rObject.Add(rKey, rValue);
                }
                return rObject;
            }
            else if (rType.IsGenericType && typeof(IDict).IsAssignableFrom(rType.GetGenericTypeDefinition()))
            {
                // Special process type of IDict<,>
                IDict rObject = (IDict)ReflectionAssist.CreateInstance(rType, BindingFlags.Default);
                Type[] rArgsTypes = rType.GetGenericArguments();
                foreach (var rItem in this.dict)
                {
                    object rKey = GetKey_ByString(rArgsTypes[0], rItem.Key);
                    object rValue = rItem.Value.ToObject(rArgsTypes[1]);
                    rObject.AddObject(rKey, rValue);
                }
                return rObject;
            }
            else if (rType.IsClass)
            {
                BindingFlags rBindFlags = ReflectionAssist.flags_all;
                object rObject = ReflectionAssist.CreateInstance(rType, rBindFlags);
                foreach (var rItem in this.dict)
                {
                    Type rMemberType = null;
                    FieldInfo rFieldInfo = rType.GetField(rItem.Key, rBindFlags);
                    if (rFieldInfo != null)
                    {
                        rMemberType = rFieldInfo.FieldType;
                        object rValueObj = rItem.Value.ToObject(rMemberType);
                        rFieldInfo.SetValue(rObject, rValueObj);
                        continue;
                    }
                    PropertyInfo rPropInfo = rType.GetProperty(rItem.Key, rBindFlags);
                    if (rPropInfo != null)
                    {
                        rMemberType = rPropInfo.PropertyType;
                        object rValueObj = rItem.Value.ToObject(rMemberType);
                        rPropInfo.SetValue(rObject, rValueObj, null);
                        continue;
                    }
                }
                return rObject;
            }
            return null;
        }

        /// <summary>
        /// Convert Key
        /// </summary>
        private object GetKey_ByString(Type rKeyType, string rKeyStr)
        {
            object rKey = rKeyStr;
            if (rKeyType == typeof(int))
            {
                int rIntKey = 0;
                int.TryParse(rKeyStr, out rIntKey);
                rKey = rIntKey;
            }
            else if (rKeyType == typeof(long))
            {
                long rLongKey = 0;
                long.TryParse(rKeyStr, out rLongKey);
                rKey = rLongKey;
            }
            return rKey;
        }
    }

    public class JsonData : JsonNode
    {
        private string  value;

        public JsonData(string v)
        {
            value = v;
        }

        public JsonData(float v)
        {
            AsFloat = v;
        }

        public JsonData(double v)
        {
            AsDouble = v;
        }

        public JsonData(int v)
        {
            AsInt = v;
        }

        public JsonData(long v)
        {
            AsLong = v;
        }

        public JsonData(bool v)
        {
            AsBool = v;
        }

        public override string Value
        {
            get { return this.value;  }
            set { this.value = value; }
        }

        public override string ToString()
        {
            return "\"" + value.ToString() + "\"";
        }

        public override object ToObject(Type rType)
        {
            if (rType.IsPrimitive)
            {
                if (rType == typeof(int))
                {
                    return CastInt(this.value);
                }
                else if (rType == typeof(long))
                {
                    return CastLong(this.value);
                }
                else if (rType == typeof(float))
                {
                    return CastFloat(this.value);
                }
                else if (rType == typeof(double))
                {
                    return CastDouble(this.value);
                }
                else if (rType == typeof(bool))
                {
                    return CastBool(this.value);
                }
            }
            else if (rType.IsEnum)
            {
                return CastEnum(rType, this.value);
            }
            else if (rType == typeof(string))
            {
                return this.value;
            }
            Debug.LogError("Is not Primitive type, can't convert to JsonData !");
            return null;
        }
    }
}


