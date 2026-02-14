using System;
using System.Diagnostics.CodeAnalysis;

namespace AvaloniaApplication5.Models
{
    public readonly struct EdgeKey
    {
        public string A{get;}
        public string B{get;}

        public EdgeKey(string a, string b)
        {
            if(string.Compare(a,b) < 0){ A=a; B=b;}
            else { A=b; B=a;}
        }

        public override int GetHashCode() => HashCode.Combine(A,B);
        public override bool Equals(object obj)
            => obj is EdgeKey other && A==other.A && B==other.B;
    }
}