//======================================================================
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
    /// <summary>
    /// Json of Word anysis : Parse matrix and data structure.
    /// </summary>
    public class JsonLexical
    {
        /// <summary>
        /// Parse state matrix of comments
        /// a -> '/'    a -> '*'    a -> 'other'    a -> '\n'
        ///    0           1             2              3
        /// 5 last state, 6 error state
        /// </summary>
        public static int[][] CommentsStateMatrix = new int[][]
        {
            new int[] { 1, 6, 6, 6 },   //0
            new int[] { 4, 2, 6, 6 },   //1
            new int[] { 2, 3, 2, 2 },   //2
            new int[] { 5, 2, 2, 2 },   //3
            new int[] { 4, 4, 4, 5 },   //4
        };

        /// <summary>
        /// Parse state matrix of special charactors
        /// a -> '\b' '\f' '\r' '\t' '\n' ' '   a -> other
        /// 2 last state, 3 error state
        /// </summary>
        public static int[][] SpecialSymbolMatrix = new int[][]
        {
            new int[] { 1, 3 }, //0
            new int[] { 1, 2 }  //1
        };

        /// <summary>
        /// Parse state matrix of string
        /// a -> '"'   a -> other
        /// 3 last state, 4 error state
        /// </summary>
        public static int[][] StringMatrix = new int[][]
        {
            new int[] { 1, 5 }, //0
            new int[] { 3, 2 }, //1
            new int[] { 3, 2 }, //2
            new int[] { 4, 4 }  //3
        };

        /// <summary>
        /// Parse state matrix of indentify
        /// a -> letter   a -> digit  a -> _  a -> other
        ///     0             1          2       3
        /// 2 last state, 3 error state
        /// </summary>
        public static int[][] IdentiferMatrix = new int[][]
        {
            new int[] { 1, 3, 1, 3 },
            new int[] { 1, 1, 1, 2 }
        };

        /// <summary>
        /// keywords in json: false true null
        /// </summary>
        public static List<string> Keywords = new List<string>()
        {
            "false",
            "true",
            "null"
        };

        /// <summary>
        /// Parse state matrix of number
        /// a -> digit(0)   a -> digit(1-9)  a -> E/e   a -> .   a -> +     a -> -    a -> other
        ///    0                    1           2         3         4          5         6
        /// 9 last state, 10 error state
        /// </summary>
        public static int[][] DigitMatrix = new int[][]
        {
            new int[] { 2, 1, 10, 4,  10, 3,  10 },   //0
            new int[] { 1, 1, 6,  4,  9,  9,  9  },   //1
            new int[] { 1, 1, 6,  4,  9,  9,  9  },   //2
            new int[] { 2, 1, 10, 4,  10, 10, 10 },   //3
            new int[] { 5, 5, 6,  9,  9,  9,  9  },   //4
            new int[] { 5, 5, 6,  9,  9,  9,  9  },   //5
            new int[] { 8, 8, 10, 10, 7,  10, 10 },   //6
            new int[] { 8, 8, 10, 10, 10, 10, 10 },   //7
            new int[] { 8, 8, 9,  9,  9,  9,  9  },   //8
        };

        public enum SymbolType
        {
            Unknown  = 0,   // unknown
            ObjStart,       // '{'
            ObjEnd,         // '}'
            ArrayStart,     // '['
            ArrayEnd,       // ']'
            ObjSplit,       // ','
            ElementSplit,   // ':'
            Key,            // Name: string
            Value,          // Value：number、string、true、false、null
        }

        public class SymbolItem
        {
            public string       value;
            public SymbolType   type;
            public JsonNode     node;
        }
    }

    /// <summary>
    /// JsonParser
    /// </summary>
    public class JsonParser
    {
        /// <summary>
        /// json original data
        /// </summary>
        public string originData;

        public JsonParser(string rOriginData)
        {
            this.originData = rOriginData.Trim();
        }

        /// <summary>
        /// Parse to JsonNode
        /// </summary>
        /// <param name="jsonStr">json string</param>
        public static JsonNode Parse(string jsonStr)
        {
            JsonParser rJsonParser = new JsonParser(jsonStr);
            return rJsonParser.Parser();
        }

        /// <summary>
        /// Process Comments and Special Charactor
        /// </summary>
        public string PretreatmentProc()
        {
            string temp = "";
            string tempWord = "";

            int i = 0;
            int end = 0;
            while(i < originData.Length)
            {
                //clear ' ', '\t', '\r' '\n'
                if (!string.IsNullOrEmpty(isSpecialSymbol(i, ref end)))
                {
                    i = end;
                    continue;
                }

                //clear comments
                if (!string.IsNullOrEmpty(tempWord = isComment(i, ref end)))
                {
                    Debug.Log(tempWord);
                    i = end;
                    continue;
                }
                temp += originData[i];
                i++;
            }

            return temp;
        }

        /// <summary>
        /// Parse Json
        /// </summary>
        public JsonNode Parser()
        {
            int end = 0;
            int i = 0;

            JsonLexical.SymbolItem rCurSymbol = null;
            JsonLexical.SymbolItem rLastSymbol = null;
            JsonNode rCurNode = null;

            Stack<JsonLexical.SymbolItem> rNodeStack = new Stack<JsonLexical.SymbolItem>();
            while(i < this.originData.Length)
            {
                //clear ' ', '\t', '\r' '\n' comments
				if (!string.IsNullOrEmpty(isSpecialSymbol(i, ref end)) || !string.IsNullOrEmpty(isComment(i, ref end)))
                {
                    i = end;
                    continue;
                }

                //Start build SymbolItem
                rCurSymbol = buildSymbolItem(rLastSymbol, rCurNode, i, ref end);
                if (rCurSymbol != null)
                {
                    JsonLexical.SymbolItem rTopValueItem = null;
                    JsonLexical.SymbolItem rTopKeyItem = null;
                    switch (rCurSymbol.type)
                    {
                        case JsonLexical.SymbolType.ObjStart:
                            rCurSymbol.node = new JsonClass();
                            rNodeStack.Push(rCurSymbol);
                            rCurNode = rCurSymbol.node;
                            break;
                        case JsonLexical.SymbolType.ObjEnd:
                        case JsonLexical.SymbolType.ArrayEnd:
                        case JsonLexical.SymbolType.ObjSplit:
                            if (rNodeStack.Count == 0) break;

                            rTopValueItem = rNodeStack.Pop();
                            rTopKeyItem = null;
                            if (rNodeStack.Count == 0)
                            {
                                if (rCurSymbol.type == JsonLexical.SymbolType.ObjSplit)
                                    rNodeStack.Push(rTopValueItem);
                                break;
                            }
                            if (rNodeStack.Peek().type == JsonLexical.SymbolType.Key)
                                rTopKeyItem = rNodeStack.Pop();
                            
                            if (rTopKeyItem != null)    //is Object
                            {
                                rCurNode = rNodeStack.Count != 0 ? rNodeStack.Peek().node : new JsonClass();
                                rCurNode.Add(rTopKeyItem.value, rTopValueItem.node);
                            }
                            else  //is Array
                            {
                                rCurNode = rNodeStack.Count != 0 ? rNodeStack.Peek().node : new JsonArray();
                                rCurNode.Add(rTopValueItem.node);
                            }
                            break;
                        case JsonLexical.SymbolType.ArrayStart:
                            rCurSymbol.node = new JsonArray();
                            rNodeStack.Push(rCurSymbol);
                            rCurNode = rCurSymbol.node;
                            break;
                        case JsonLexical.SymbolType.ElementSplit:
                            break;
                        case JsonLexical.SymbolType.Key:
                            rNodeStack.Push(rCurSymbol);
                            break;
                        case JsonLexical.SymbolType.Value:
                            rCurSymbol.node = new JsonData(rCurSymbol.value);
                            rNodeStack.Push(rCurSymbol);
                            rCurNode = rCurSymbol.node;
                            break;
                        default:
                            break;
                    }
                    i = end;
                    rLastSymbol = rCurSymbol;
                    //Debug.Log(string.Format("CurSymbol value = {0}, type = {1}", rCurSymbol.value, rCurSymbol.type));
                    continue;
                }
                i++;
            }
            return rCurNode;
        }

        private JsonLexical.SymbolItem buildSymbolItem(JsonLexical.SymbolItem rLastSymbol, 
                                                       JsonNode rCurNode, int begin, ref int end)
        {
            if (originData[begin] == '{')
            {
                end = begin + 1;
                return new JsonLexical.SymbolItem() { value = "{", type = JsonLexical.SymbolType.ObjStart };
            }
            else if (originData[begin] == '}')
            {
                end = begin + 1;
                return new JsonLexical.SymbolItem() { value = "}", type = JsonLexical.SymbolType.ObjEnd };
            }
            else if (originData[begin] == '[')
            {
                end = begin + 1;
                return new JsonLexical.SymbolItem() { value = "[", type = JsonLexical.SymbolType.ArrayStart };
            }
            else if (originData[begin] == ']')
            {
                end = begin + 1;
                return new JsonLexical.SymbolItem() { value = "]", type = JsonLexical.SymbolType.ArrayEnd };
            }

            if (rLastSymbol.type == JsonLexical.SymbolType.ElementSplit || rLastSymbol.type == JsonLexical.SymbolType.ArrayStart ||
                (rCurNode is JsonArray && rLastSymbol.type == JsonLexical.SymbolType.ObjSplit))
            {
                string tempWord = "";
                //Is Keyword, number and string
                if (!string.IsNullOrEmpty(tempWord = isKeyword(begin, ref end)))
                {
                    return new JsonLexical.SymbolItem() { value = tempWord, type = JsonLexical.SymbolType.Value };
                }
                if (!string.IsNullOrEmpty(tempWord = isDigit(begin, ref end)))
                {
                    return new JsonLexical.SymbolItem() { value = tempWord, type = JsonLexical.SymbolType.Value };
                }
                if (!string.IsNullOrEmpty(tempWord = isString(begin, ref end)))
                {
                    tempWord = tempWord.Substring(1, tempWord.Length - 2);
                    return new JsonLexical.SymbolItem() { value = tempWord, type = JsonLexical.SymbolType.Value };
                }
                Debug.Log(string.Format("Json parse symbol item error! LastSymbol = {0}", 
                               rLastSymbol != null ? rLastSymbol.value : "null"));
                return null;
            }
            else
            {
                if (originData[begin] == ',')
                {
                    end = begin + 1;
                    return new JsonLexical.SymbolItem() { value = ",", type = JsonLexical.SymbolType.ObjSplit };
                }
                else if (originData[begin] == ':')
                {
                    end = begin + 1;
                    return new JsonLexical.SymbolItem() { value = ":", type = JsonLexical.SymbolType.ElementSplit };
                }
                else if (originData[begin] == '\"')
                {
                    string tempWord = "";
                    if (!string.IsNullOrEmpty(tempWord = isString(begin, ref end)))
                    {
                        tempWord = tempWord.Substring(1, tempWord.Length - 2);
                        return new JsonLexical.SymbolItem() { value = tempWord, type = JsonLexical.SymbolType.Key };
                    }
                    Debug.Log(string.Format("Json parse symbol item error! LastSymbol = {0}",
                                   rLastSymbol != null ? rLastSymbol.value : "null"));
                    return null;
                }
                else
                {
                    Debug.Log(string.Format("Json parse symbol item error! LastSymbol = {0}",
                                   rLastSymbol != null ? rLastSymbol.value : "null"));
                    return null;
                }
            }
        }

        private int isCommitInputChar(char c)
        {
            int j = 0;
            if      (c == '/')  j = 0;
            else if (c == '*')  j = 1;
            else if (c == '\n') j = 3;
            else                j = 2;        //other
            return  j;
        }

        /// <summary>
        /// is Comment /**/ 和 //
        /// </summary>
        private string isComment(int begin, ref int end)
        {
            string result = checkLexical(isCommitInputChar, JsonLexical.CommentsStateMatrix, 5, 6, begin, ref end);
            if (!string.IsNullOrEmpty(result)) result += originData[end++];
            return result;
        }

        private int isSpecialSymbolInputChar(char c)
        {
            int j = 0;
            if (c == '\b' || c == '\f' || c == '\r' || c == '\n' || c == ' ' || c == '\0' || c == '\t')
                j = 0;
            else
                j = 1;
            return j;
        }

        /// <summary>
        /// Is Special charactor:  \b \f \n \r \t space
        /// </summary>
        private string isSpecialSymbol(int begin, ref int end)
        {
            return checkLexical(isSpecialSymbolInputChar, JsonLexical.SpecialSymbolMatrix, 2, 3, begin, ref end);
        }

        private int isStringInputChar(char c)
        {
            int j = 0;
            if (c == '"') j = 0;
            else          j = 1;
            return j;
        }

        /// <summary>
        /// Is "string"
        /// </summary>
        private string isString(int begin, ref int end)
        {
            return checkLexical(isStringInputChar, JsonLexical.StringMatrix, 4, 5, begin, ref end);
        }

        private int isIdentifierInputChar(char c)
        {
            int j = 0;
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                j = 0;
            else if (c >= '0' && c <= '9')
                j = 1;
            else if (c == '_')
                j = 2;
            else
                j = 3;
            return j;
        }

        /// <summary>
        /// Is Identifier
        /// </summary>
        private string isIdentifier(int begin, ref int end)
        {
            return checkLexical(isIdentifierInputChar, JsonLexical.IdentiferMatrix, 2, 3, begin, ref end);
        }

        /// <summary>
        /// Is Keyword false true null
        /// </summary>
        private string isKeyword(int begin, ref int end)
        {
            string tempWord = isIdentifier(begin, ref end);
            if (JsonLexical.Keywords.Contains(tempWord))
                return tempWord;
            else
                return "";
        }

        private int isDigitInputChar(char c)
        {
            int j = 0;
            if (c == '0')
                j = 0;
            else if (c >= '1' && c <= '9')
                j = 1;
            else if (c == 'E' || c == 'e')
                j = 2;
            else if (c == '.')
                j = 3;
            else if (c == '+')
                j = 4;
            else if (c == '-')
                j = 5;
            else
                j = 6;
            return j;
        }

        private string isDigit(int begin, ref int end)
        {
            return checkLexical(isDigitInputChar, JsonLexical.DigitMatrix, 9, 10, begin, ref end);
        }

        private string checkLexical(System.Func<char, int> isInputCharFunc, int[][] stateMatrix,
                                    int lastState, int errorState, int begin, ref int end)
        {
            string temp = "";
            int pCurrent = begin;
            int nState = 0;
            int nInputChar = 0;

            while (pCurrent < originData.Length)
            {
                nInputChar = isInputCharFunc(originData[pCurrent]);
                if (nState != lastState && nState != errorState)
                {
                    nState = stateMatrix[nState][nInputChar];
                }

                if (nState != lastState && nState != errorState)
                    temp += originData[pCurrent];

                if (nState == lastState)
                {
                    end = pCurrent;
                    break;
                }
                if (nState == errorState)
                {
                    end = pCurrent;
                    temp = "";
                    break;
                }
                pCurrent++;
            }

            if (nState != lastState) temp = "";

            return temp;
        }

        /// <summary>
        /// Object to JsonNode
        /// </summary>
        public static JsonNode ToJsonNode(object rObject)
        {
            Type rType = rObject.GetType();

            JsonNode rRootNode = null;

            //List
            if (rType.IsGenericType && typeof(IList).IsAssignableFrom(rType.GetGenericTypeDefinition()))
            {
                rRootNode = new JsonArray();
                IList rListObj = (IList)rObject;
                foreach (var rItem in rListObj)
                {
                    JsonNode rNode = ToJsonNode(rItem);
                    rRootNode.Add(rNode);
                }
            }
            else if (rType.IsArray) //Array
            {
                rRootNode = new JsonArray();
                Array rArrayObj = (Array)rObject;
                foreach (var rItem in rArrayObj)
                {
                    JsonNode rNode = ToJsonNode(rItem);
                    rRootNode.Add(rNode);
                }
            }
            else if (rType.IsGenericType && typeof(IDictionary).IsAssignableFrom(rType.GetGenericTypeDefinition()))
            {
                //Dictionary
                rRootNode = new JsonClass();
                IDictionary rDictObj = (IDictionary)rObject;
                foreach (var rKey in rDictObj.Keys)
                {
                    JsonNode rValueNode = ToJsonNode(rDictObj[rKey]);
                    rRootNode.Add(rKey.ToString(), rValueNode);
                }
            }
            else if (rType.IsGenericType && typeof(IDict).IsAssignableFrom(rType.GetGenericTypeDefinition()))
            {
                rRootNode = new JsonClass();
                IDict rDictObj = (IDict)rObject;
                foreach (var rItem in rDictObj.OriginCollection)
                {
                    JsonNode rValueNode = ToJsonNode(rItem.Value);
                    rRootNode.Add(rItem.Key.ToString(), rValueNode);
                }
            }
            else if (rType.IsClass) //Is Class，Get all public attributes
            {
                rRootNode = new JsonClass();
                // all public attributes
                PropertyInfo[] rPropInfos = rType.GetProperties(ReflectionAssist.flags_public);
                for (int i = 0; i < rPropInfos.Length; i++)
                {
                    object rValueObj = rPropInfos[i].GetValue(rObject, null);
                    JsonNode rValueNode = ToJsonNode(rValueObj);
                    rRootNode.Add(rPropInfos[i].Name, rValueNode);
                }
                // all public attributes
                FieldInfo[] rFieldInfos = rType.GetFields(ReflectionAssist.flags_public);
                for (int i = 0; i < rFieldInfos.Length; i++)
                {
                    object rValueObj = rFieldInfos[i].GetValue(rObject);
                    JsonNode rValueNode = ToJsonNode(rValueObj);
                    rRootNode.Add(rFieldInfos[i].Name, rValueNode);
                }
                // TODO: Arrtibute of private

            }
            else if (rType.IsPrimitive)
            {
                if (rType == typeof(int))
                    rRootNode = new JsonData((int)rObject);
                else if (rType == typeof(long))
                    rRootNode = new JsonData((long)rObject);
                else if (rType == typeof(float))
                    rRootNode = new JsonData((float)rObject);
                else if (rType == typeof(double))
                    rRootNode = new JsonData((double)rObject);
                else if (rType == typeof(bool))
                    rRootNode = new JsonData((bool)rObject);
                else if (rType == typeof(string))
                    rRootNode = new JsonData((string)rObject);
                else
                    Debug.LogError(string.Format("Type = {0}, not supported object type!", rObject.GetType()));
            }

            return rRootNode;
        }
    }
}