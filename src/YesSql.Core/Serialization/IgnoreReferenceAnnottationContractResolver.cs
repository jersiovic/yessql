using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Reflection;
using YesSql.Attributes;

namespace YesSql.Serialization
{
    public class IgnoreReferenceAnnottationContractResolver : DefaultContractResolver
    {
        public static readonly IgnoreReferenceAnnottationContractResolver Instance = new IgnoreReferenceAnnottationContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (member.CustomAttributes.Any(a=>a.AttributeType == typeof(ReferenceAttribute)))
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        return false;
                    };
            }

            return property;
        }
    }
}