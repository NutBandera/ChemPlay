using UnityEngine;
using System.Collections;
using WindJson;
using System.Collections.Generic;
using System.IO;

public class Demo : MonoBehaviour
{
    public class A
    {
        public int      B1 { get; set; }
        public long     B2 { get; set; }
        public float    B3 { get; set; }
        public double   B4 { get; set; }
        public bool     B5 { get; set; }

        public A1[]     A1 { get; set; }

        public List<int>                D;
        public Dictionary<int, A1[]>    E;
        public Dict<int, Dict<int, A1>> F;
    }

    public class A1
    {
        public double a;
    }

    public class C
    {
        public int[] c;
    }

    void Start()
    {
        JsonParser rJsonParser = null;
        using (FileStream fs = new FileStream("Assets/Plugins/WindJson/Demo/Test1.txt", FileMode.Open))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                rJsonParser = new JsonParser(sr.ReadToEnd());
            }
        }
        // Json string --> object
        JsonNode rNode = rJsonParser.Parser();
        // Debug.Log(rNode.ToString());*/
        A a = rNode.ToObject(typeof(A)) as A;
        /*Debug.Log(string.Format("{0}, {1}, {2}", a.B1, a.B2, a.A1[1].a));
        for (int i = 0; i < a.D.Count; i++)
        {
            Debug.Log(a.D[i]);
        }
        foreach (var item in a.E)
        {
            Debug.Log(string.Format("E: ({0}, {1})", item.Key, item.Value[1].a));
        }
        foreach (var item in a.F)
        {
            foreach (var secondItem in item.Value)
            {
                Debug.Log(string.Format("F: ({0}, {1})", item.Key, secondItem.Value.a));
            }
        }*/

        // object --> Json string
        JsonNode rJsonNode = JsonParser.ToJsonNode(a);
        Debug.Log(rJsonNode.ToString());

        // Build JsonNode
        /*JsonNode rRootNode = new JsonClass();
        rRootNode["name"] = new JsonData("Winddy");
        rRootNode["age"] = new JsonData(12);
        JsonNode rArray = new JsonArray();
        rArray.Add(new JsonData("book1"));
        rArray.Add(new JsonData("book2"));
        rArray.Add(new JsonData("book3"));
        rRootNode["books"] = rArray;
        Debug.Log(rRootNode.ToString());*/

        //List<A> rLists = rJsonNode.ToList<A>();
        //A[] rArrays = rJsonNode.ToArray<A>();
        //Dict<string, A> rDict1 = rJsonNode.ToDict<string, A>();
        //Dictionary<string, A> rDict2 = rJsonNode.ToDictionary<string, A>();
    }
}
