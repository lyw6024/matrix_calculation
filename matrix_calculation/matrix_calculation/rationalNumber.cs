using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix_calculation
{
    class RationalNumber
    {
        private int _num, _den;
        private int gcd(int x,int y)
        {
            if(x%y==0)
            {
                return y;
            }
            else
            {
                return gcd(y, x % y);
            }
        }

        public RationalNumber()
        {
            _num = 0;
            _den = 1;
        }
        public RationalNumber(int x)
        {
            _num = x;
            _den = 1;
        }
        public RationalNumber(int nn,int dd)
        {
            int sign = 1;
            int g = gcd(Math.Abs(nn), Math.Abs(dd));
            if(nn*dd<0)
            {
                sign = -1;
            }
            _num = sign*Math.Abs(nn)/g;
            _den = Math.Abs(dd)/g;
        }
        public string ToLaTeX()
        {
            if (_den == 1)
            {
                return _num.ToString();
            }
            else
            {
                if (_num > 0)
                {
                    return String.Format("\\frac{{{0}}}{{{1}}}", _num, _den);
                }
                else
                {
                    return String.Format("-\\frac{{{0}}}{{{1}}}", -_num, _den);
                }
            }
        }
        public override string ToString()
        {
            return String.Format("{0}/{1}", _num, _den);
        }
        public static RationalNumber operator+(RationalNumber self,RationalNumber o)
        {
            return new RationalNumber(self._num * o._den + self._den * o._num, self._den * o._den);
        }
        public static RationalNumber operator -(RationalNumber self, RationalNumber o)
        {
            return new RationalNumber(self._num * o._den - self._den * o._num, self._den * o._den);
        }
        public static RationalNumber operator *(RationalNumber self, RationalNumber o)
        {
            return new RationalNumber(self._num * o._num, self._den * o._den);
        }
        public static RationalNumber operator /(RationalNumber self, RationalNumber o)
        {
            return new RationalNumber(self._num * o._den, self._den * o._num);
        }
        public static RationalNumber operator /( int o, RationalNumber self)
        {
            return new RationalNumber( self._den * o, self._num);
        }
        public static RationalNumber operator /(RationalNumber self, int o)
        {
            return new RationalNumber(self._num , self._den *o );
        }
        public static bool operator ==(RationalNumber self, RationalNumber o)
        {
            return self._num == o._num && self._den == o._den;
        }
        public static bool operator !=(RationalNumber self, RationalNumber o)
        {
            return !(self._num == o._num && self._den == o._den);
        }

        public static bool operator ==(RationalNumber self, int o)
        {
            return self._den == 1 && self._num == o;
        }
        public static bool operator !=(RationalNumber self, int o)
        {
            return (self._den != 1  || (self._den ==1 && self._num != o));
        }

        public override bool Equals(Object o)
        {
            return this is RationalNumber &&  this == (RationalNumber)o;
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(String.Format("{0}{1}",_num , _den));
        }
    }
}
