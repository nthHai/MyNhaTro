using Dapper;
using System.Data;

namespace MyNhaTro.Helper
{
    public static class Extensions
    {
        public static string ToPascalCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var words = input.Split('_');
            var result = string.Join("", words.Select(w => char.ToUpper(w[0]) + w.Substring(1).ToLower()));
            return result;
        }
    }

    public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.Value = value.ToDateTime(TimeOnly.MinValue); // Chuyển sang DateTime
            parameter.DbType = DbType.Date;
        }

        public override DateOnly Parse(object value)
        {
            return DateOnly.FromDateTime((DateTime)value); // Chuyển từ DateTime sang DateOnly
        }
    }

    public class NullableDateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly?>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly? value)
        {
            parameter.Value = value.HasValue ? value.Value.ToDateTime(TimeOnly.MinValue) : DBNull.Value;
            parameter.DbType = DbType.Date;
        }

        public override DateOnly? Parse(object value)
        {
            return value == null || value == DBNull.Value ? null : DateOnly.FromDateTime((DateTime)value);
        }
    }
}
