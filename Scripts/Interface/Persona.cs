using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
internal class Persona
{
    [DataMember]
    internal string name;

    [DataMember]
    internal int age;
    public string getName() {
        return name;
    }
    public int getAge() {
        return age;
    }
}
