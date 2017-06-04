using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionUtils.ComplexTypes
{
    public class ValueRange<ValueT> where ValueT : IComparable<ValueT>
    {
        private ValueT _min;
        public ValueT Min {
            get
            {
                return _max;
            }
            set
            {
                if (value.CompareTo(_max) > 0)
                    throw new ArgumentOutOfRangeException("Min number is greater than max.");
                _min = value;
            }
        }

        private ValueT _max;
        public ValueT Max
        {
            get
            {
                return _max;
            }
            set
            {
                if (value.CompareTo(_min) < 0)
                    throw new ArgumentOutOfRangeException("Max number is smaller than min.");
                _max = value;
            }
        }

        public ValueRange(ValueT min, ValueT max)
        {
            //Set directly
            _min = min;
            //Set with comparing
            Max = max;
        }
    }
}
