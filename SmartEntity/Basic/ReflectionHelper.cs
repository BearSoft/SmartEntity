using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SmartEntity.Basic
{
    public class ReflectionHelper
    {
        public static LambdaExpression GetLambda<TBaseType>(Expression<Func<TBaseType, dynamic>> memberPath)
        {
            return memberPath;
        }

        public static LambdaExpression GetLambda(PropertyInfo propertyInfo)
        {
            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
            var property = Expression.Property(instance, propertyInfo);
            var convert = Expression.TypeAs(property, propertyInfo.ReflectedType);
            return Expression.Lambda(convert, instance);
        }

        public static List<PropertyInfo> GetPropertyInfos<T>()
        {
            List<PropertyInfo> propertyInfos;

            propertyInfos = typeof(T).GetProperties().ToList<PropertyInfo>();

            return propertyInfos;
        }

        public static PropertyInfo GetPropertyInfo<T>(string propertyName)
        {
            return typeof (T).GetProperty(propertyName);
        }

        public static Type GetPropertyType(Type dtoType, string fieldName)
        {
            return dtoType.GetProperty(fieldName).PropertyType;
        }

        public static object GetPropertyValue<T>(T instance, string propertyName)
        {
            return GetPropertyInfo<T>(propertyName).GetValue(instance, null);
        }

        public static void SetPropertyValue<T>(T instance, string propertyName, object value)
        {
            GetPropertyInfo<T>(propertyName).SetValue(instance, value, null);
        }

        public static List<PropertyInfo> GetPropertyInfos(Type theType)
        {
            List<PropertyInfo> propertyInfos;

            propertyInfos = theType.GetProperties().ToList<PropertyInfo>();

            return propertyInfos;
        }

        public static IEnumerable<string> GetAllPropertyNames(Type theType)
        {
            List<string> fields = new List<string>();

            foreach (var fieldDescriptor in theType.GetProperties().ToList<PropertyInfo>())
            {
                fields.Add(fieldDescriptor.Name);
            }


            return fields;
        }

        public static IEnumerable<string> GetAllComplexProperties(Type theType)
        {
            List<string> fields = new List<string>();

            foreach (var fieldDescriptor in theType.GetProperties().ToList<PropertyInfo>())
            {
                if (!IsDirectValue(fieldDescriptor.PropertyType))
                {
                    fields.Add(fieldDescriptor.Name);
                }
            }


            return fields;
        }

        public static List<LambdaExpression> GetPropertyLambdas<T>()
        {
            List<PropertyInfo> propertyInfos = GetPropertyInfos<T>();
            List<LambdaExpression> propertyExpressions = new List<LambdaExpression>();

            foreach (var propertyInfo in propertyInfos)
            {
                propertyExpressions.Add(GetLambda(propertyInfo));
            }

            return propertyExpressions;
        }

        public static string GetPropertyName(LambdaExpression propertyLambda)
        {
            var body = propertyLambda.Body;

            if (body is MemberExpression)
            {
                return ((MemberExpression)body).Member.Name;
            }

            return null;
        }

        public static Action<T,object> GetPropertySetter<T>(Expression<Func<T, object>> propertyGetter)
        {
            var member = (MemberExpression)propertyGetter.Body;
            var param = Expression.Parameter(typeof(string), "value");
            var set = Expression.Lambda<Action<T, object>>(Expression.Assign(member, param), propertyGetter.Parameters[0], param);

            // compile it
            return set.Compile();

        }

        public static Action<TDto, TValue> GetPropertySetter<TDto, TValue>(Expression<Func<TDto, TValue>> propertyGetter)
        {
            var member = (MemberExpression)propertyGetter.Body;
            var param = Expression.Parameter(typeof(TValue), "value");
            var assigmentExpr = Expression.Assign(member, param);
            var set = Expression.Lambda<Action<TDto, TValue>>(assigmentExpr, propertyGetter.Parameters[0], param);

            // compile it
            return set.Compile();
        }

        public static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
            {
                return Activator.CreateInstance(t);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Determines whether the type represents a direct value.
        /// </summary>
        /// <param name="valueType">Type of the value.</param>
        /// <returns><c>True</c> is the specified type is a direct value.</returns>
        public static bool IsDirectValue(Type valueType)
        {
            return (valueType == typeof(string))
                || (valueType == typeof(DateTime))
                || (valueType == typeof(DateTime?))
                || (valueType == typeof(Guid))
                || (valueType == typeof(Guid?))
                || (valueType == typeof(Decimal))
                || (valueType == typeof(Decimal?))

                || (valueType.IsPrimitive)
                || (valueType.IsEnum);
        }

        /// <summary>
        /// Determines whether the specified value is empty or null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is empty or null] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmptyOrNull(object value)
        {
            if (value == null) return true;

            var valueAsString = value as string;
            if (valueAsString != null)
            {
                if (valueAsString == string.Empty) return true;
            }

            return false;
        }

        /// <summary>
        /// Creates the new instance for the specified type.
        /// </summary>
        /// <param name="dtoType">Type of the dto.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">The specified type has no public parameterless constructor, or is not a non-abstract class.</exception>
        public static object CreateNewInstance(Type dtoType)
        {
            try
            {
                var constructor = dtoType.GetConstructor(new Type[] { });
                var instance = constructor.Invoke(new object[] { });
                return instance;
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(string.Format("Cannot create empty instance of type <{0}>.", dtoType), ex);
            }
        }
    }
}
