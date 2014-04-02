using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HeptaSoft.SmartEntity.Basic;
using HeptaSoft.SmartEntity.DomainModel.Mapping.Accessors;

namespace HeptaSoft.SmartEntity.DomainModel.Identification
{
    internal class Finder : IFinder
    {
        /// <summary>
        /// The filter expression, with parameters for the key properties.
        /// </summary>
        private readonly Expression filterWithParams;

        /// <summary>
        /// The delegate for getting data from repository.
        /// </summary>
        private readonly Delegate getFromRepositoryDelegate;

        /// <summary>
        /// The required parameters
        /// </summary>
        private readonly Dictionary<IPropertyAccessor, ParameterExpression> requiredParameters;

        /// <summary>
        /// Gets the required properties.
        /// </summary>
        /// <value>
        /// The required properties.
        /// </value>
        public IEnumerable<IPropertyAccessor> RequiredProperties
        {
            get
            {
                return this.requiredParameters.Keys;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Finder" /> class.
        /// </summary>
        /// <param name="keyProperties">The key properties.</param>
        /// <param name="getFromRepositoryLambda">The get from repository lambda.</param>
        public Finder(IEnumerable<IPropertyAccessor> keyProperties, LambdaExpression getFromRepositoryLambda)
        {
            this.requiredParameters = new Dictionary<IPropertyAccessor, ParameterExpression>();
            this.filterWithParams = this.BuildFilterCondition(keyProperties, this.requiredParameters); 
            this.getFromRepositoryDelegate = getFromRepositoryLambda.Compile();
        }

        /// <summary>
        /// Identifies the specified parameters with values.
        /// </summary>
        /// <param name="keyPropertyValues">The key property values.</param>
        /// <returns></returns>
        public object Find(IDictionary<IPropertyAccessor, object> keyPropertyValues)
        {
            var parametersWithValues = this.GetParametersWithValues(keyPropertyValues, this.requiredParameters);
            var filterExpression = this.ReplaceParameteresWithValues(this.filterWithParams, parametersWithValues);
            
            var existingEntity = this.getFromRepositoryDelegate.DynamicInvoke(filterExpression);

            return existingEntity;
        }


        /// <summary>
        /// Builds the expression for the filter condition, with parameters.
        /// </summary>
        /// <param name="keyProperties">The key properties.</param>
        /// <param name="requiredParametersDictionary">The required parameters dictionary.</param>
        /// <returns></returns>
        private Expression BuildFilterCondition(IEnumerable<IPropertyAccessor> keyProperties, Dictionary<IPropertyAccessor, ParameterExpression> requiredParametersDictionary)
        {
            const string ValueParameterPrefix = "@p";
            const string InstanceSymbol = "x";

            List<ParameterExpression> parameters = new List<ParameterExpression>();
            Expression filterExpression = null;
            int k = 0;

            ParameterExpression entityInstance = null;
           
            foreach (var keyProperty in keyProperties)
            {
                if (entityInstance == null)
                {
                    entityInstance = Expression.Parameter(keyProperty.DtoType, InstanceSymbol);
                    parameters.Add(entityInstance);
                }

                k++;
                string parameterName = string.Format("{0}{1}", ValueParameterPrefix, k);
                var keyPropertyWithReplacedInstanceSymbol = ExpressionHelper.ReplaceParameters(keyProperty.MemberExpression, entityInstance);
                var propertyMemberExpression = ExpressionHelper.ExtractMemberExpressions(keyPropertyWithReplacedInstanceSymbol).FirstOrDefault();

                var valueParameter = Expression.Parameter(propertyMemberExpression.Type, parameterName);
                parameters.Add(valueParameter);
                requiredParametersDictionary.Add(keyProperty, valueParameter);
                var propertyEqualsValue = Expression.Equal(propertyMemberExpression, valueParameter);

                if (filterExpression == null)
                {
                    filterExpression = propertyEqualsValue;
                }
                else
                {
                    filterExpression = Expression.AndAlso(filterExpression, propertyEqualsValue);
                }
            }

            if (filterExpression != null)
            {
                var filterLambda = Expression.Lambda(filterExpression, entityInstance);
                return filterLambda;
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Replaces the parameteres with values.
        /// </summary>
        /// <param name="targetExpression">The target expression.</param>
        /// <param name="parametersWithValue">The parameters with value.</param>
        /// <returns></returns>
        private Expression ReplaceParameteresWithValues(
            Expression targetExpression, IDictionary<ParameterExpression, ConstantExpression> parametersWithValue)
        {
            var modifiedExpression = targetExpression;
            foreach (var parameterWithValue in parametersWithValue)
            {
                modifiedExpression = ExpressionHelper.ReplaceParameter(modifiedExpression, parameterWithValue.Key, parameterWithValue.Value);
            }

            return modifiedExpression;
        }

        /// <summary>
        /// Gets the parameters with values.
        /// </summary>
        /// <param name="keyPropertyValues">The key property values.</param>
        /// <param name="requiredParametersDictionary">The required parameters dictionary.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">No value provided for the required property.</exception>
        private IDictionary<ParameterExpression, ConstantExpression> GetParametersWithValues(IDictionary<IPropertyAccessor, object> keyPropertyValues, Dictionary<IPropertyAccessor, ParameterExpression> requiredParametersDictionary)
        {
            var parametersWithValues = new Dictionary<ParameterExpression, ConstantExpression>();

            foreach (var requiredParameter in requiredParametersDictionary)
            {
                var requiredParamPropertyAccessor = requiredParameter.Key;
                
                if (keyPropertyValues.ContainsKey(requiredParamPropertyAccessor))
                {
                    var propertyValue = keyPropertyValues[requiredParamPropertyAccessor];
                    parametersWithValues.Add(requiredParameter.Value, Expression.Constant(propertyValue));
                }
                else
                {
                    throw new InvalidOperationException(string.Format("No value provided for the required property '{0}'.", requiredParamPropertyAccessor.PropertyName));
                }
            }

            return parametersWithValues;
        }
    }
}
