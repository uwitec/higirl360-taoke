using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HiGirl360.Infrastructure.Extensions;

namespace HiGirl360.Infrastructure
{
    public class Range<T>
    {
        private string format;

        public Range() : this(default(T), default(T), string.Empty) { }

        public Range(string format) : this(default(T), default(T), format) { }

        public Range(T min, T max) : this(min, max, string.Empty) { }

        public Range(T min, T max, string format)
        {
            this.Min = min;
            this.Max = max;
            this.format = format;
        }

        public T Min { get; set; }

        public T Max { get; set; }

        public static explicit operator Range<T>(string range)
        {
            var min = default(T);
            var max = default(T);

            if (!string.IsNullOrEmpty(range))
            {
                var items = range.Split(',');
                if (items.Length > 0)
                {
                    min = items[0].As<T>();
                }
                if (items.Length > 1)
                {
                    max = items[1].As<T>();
                }
            }

            return new Range<T>(min, max);
        }

        public static implicit operator string(Range<T> range)  // implicit digit to byte conversion operator
        {

            return range == null
                ? string.Empty
                : range.ToString();
        }

        public override string ToString()
        {
            return this.ToString(this.format);
        }
        public string ToString(string format)
        {
            var min = this.Min.Equals(default(T))
                ? string.Empty
                : this.Min.Format(format);

            var max = this.Max.Equals(default(T))
                ? string.Empty
                : this.Max.Format(format);

            if (string.IsNullOrEmpty(min) && string.IsNullOrEmpty(max))
            {
                return string.Empty;
            }

            return string.Format("{0},{1}", min, max);
        }
    }
}
