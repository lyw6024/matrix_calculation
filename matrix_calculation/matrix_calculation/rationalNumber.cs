using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix_calculation
{
    class rationalNumber
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

        public rationalNumber()
        {
            _num = 0;
            _den = 1;
        }
        public rationalNumber(int x)
        {
            _num = x;
            _den = 1;
        }
        public rationalNumber(int nn,int dd)
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
        public static rationalNumber operator+(rationalNumber self,rationalNumber o)
        {
            return new rationalNumber(self._num * o._den + self._den * o._num, self._den * o._den);
        }
        public static rationalNumber operator -(rationalNumber self, rationalNumber o)
        {
            return new rationalNumber(self._num * o._den - self._den * o._num, self._den * o._den);
        }
        public static rationalNumber operator *(rationalNumber self, rationalNumber o)
        {
            return new rationalNumber(self._num * o._num, self._den * o._den);
        }
        public static rationalNumber operator /(rationalNumber self, rationalNumber o)
        {
            return new rationalNumber(self._num * o._den, self._den * o._num);
        }
    }
}
