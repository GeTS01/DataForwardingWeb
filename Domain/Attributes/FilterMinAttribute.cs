using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FilterMinAttribute : Attribute
    {
        public double Min { get; }
        
        public FilterMinAttribute(double min) { 
            Min = min;
        }

        public FilterMinAttribute(int min) { 
            Min = min;
        }

        public override string ToString() { 
            return Min.ToString();
        }

    }
}

