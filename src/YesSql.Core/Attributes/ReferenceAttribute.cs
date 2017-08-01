using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YesSql.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class ReferenceAttribute: System.Attribute
    {
        public Type TargetType { get; }
        public string TargetPath;

        public ReferenceAttribute(Type targetType) {
            TargetType = targetType;
            TargetPath = null;
        }
    }
}
