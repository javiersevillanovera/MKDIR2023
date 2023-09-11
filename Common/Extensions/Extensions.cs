using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{ 
    public static class Extensions
    {
        public static string ToHex(this byte[] bytes)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString("X2"));
            return result.ToString();
        }

        public static string ToSHA256(this string value)
        {
            try
            {
                var sha256 = System.Security.Cryptography.SHA256.Create();
                var hash = sha256.ComputeHash(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(value));

                return hash.ToHex();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string ToStringIfNull(this object o)
        {
            if (o == null)
                return string.Empty;
            return o.ToString();
        }

        public static bool isCPF(this string cpf)
        {
            char[] c;
            int[] v = new int[2];

            if ((c = Regex.Replace(cpf, "[^0-9]", string.Empty).ToCharArray()).Length != 11) return false;
            if (new Regex("^" + c[0] + "{11}$").IsMatch(string.Join("", c))) return false;

            int[] d = c.Select(c => int.Parse(c.ToString())).ToArray();

            for (var i = 0; i <= 1; i++)
            {
                var sum = 0;
                for (var j = 0; j <= 8 + i; j++)
                    sum += d[j] * (10 + i - j);

                v[i] = (sum * 10) % 11;
                if (v[i] == 10) v[i] = 0;
            }
            return (v[0] == d[9] & v[1] == d[10]);

        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int page, int take)
        {
            return page > 0 ? source.Skip((page - 1) * take).Take(take).AsQueryable() : source.AsQueryable();
        }
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, int page, int take)
        {
            return page > 0 ? source.Skip((page - 1) * take).Take(take).AsQueryable() : source.AsQueryable();
        }

        public static (bool IsSuccess, object Value) TryChangeType(this object value, Type conversionType)
        {
            (bool IsSuccess, object Value) response = (false, null);
            var isNotConvertible =
                conversionType == null
                    || value == null
                    || !(value is IConvertible);
            if (isNotConvertible)
            {
                return response;
            }

            try
            {
                if (conversionType.Name == "Guid")
                {
                    if (Guid.TryParse(value.ToString(), out Guid valueGuid))
                    {
                        response = (true, valueGuid);
                    }
                }
                else
                    response = (true, Convert.ChangeType(value, conversionType));
            }
            catch (Exception ex)
            {
                response.Value = null;
            }

            return response;
        }
    }
}

