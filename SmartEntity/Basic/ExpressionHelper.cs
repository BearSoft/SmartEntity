using System.Collections.Generic;
using System.Linq.Expressions;

namespace SmartEntity.Basic
{
    public class ExpressionHelper
    {
        #region Visitor

        private class ParameterReplacer : ExpressionVisitor
        {
            /// <summary>
            /// The new parameter name.
            /// </summary>
            private Expression replacementExpression;

            /// <summary>
            /// The parameter to replace.
            /// </summary>
            private ParameterExpression parameterToReplace;

            /// <summary>
            /// Visits the <see cref="T:System.Linq.Expressions.ParameterExpression" />.
            /// </summary>
            /// <param name="node">The expression to visit.</param>
            /// <returns>
            /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
            /// </returns>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                if ((this.parameterToReplace == null) || (node == this.parameterToReplace))
                {
                    return this.replacementExpression;
                }
                else
                {
                    return node;
                }
            }

            /// <summary>
            /// Visits the children of the <see cref="T:System.Linq.Expressions.UnaryExpression" />.
            /// </summary>
            /// <param name="node">The expression to visit.</param>
            /// <returns>
            /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
            /// </returns>
            protected override Expression VisitUnary(UnaryExpression node)
            {
                var initialParameter = node.Operand as ParameterExpression;
                if (initialParameter != null)
                {
                    var parameter = this.VisitParameter(initialParameter);
                    return Expression.Convert(parameter, node.Type);
                }
                else
                {
                    return base.VisitUnary(node);
                }
            }

            /// <summary>
            /// Replaces the parameter.
            /// </summary>
            /// <param name="expression">The expression.</param>
            /// <param name="toReplace">The parameter to replace.</param>
            /// <param name="replacement">The replacement.</param>
            /// <returns>The modified expression.</returns>
            public Expression ReplaceParameter(Expression expression, ParameterExpression toReplace, Expression replacement)
            {
                this.parameterToReplace = toReplace;
                this.replacementExpression = replacement;

                return this.Visit(expression);
            }

            /// <summary>
            /// Replaces the parameter.
            /// </summary>
            /// <param name="expression">The expression.</param>
            /// <param name="replacement">The replacement.</param>
            /// <returns>The modified expression.</returns>
            public Expression ReplaceAllParameters(Expression expression, Expression replacement)
            {
                this.parameterToReplace = null;
                this.replacementExpression = replacement;

                return this.Visit(expression);
            }
        }

        private class MembersVisitor : ExpressionVisitor
        {
            private HashSet<MemberExpression> members;

            protected override Expression VisitMember(MemberExpression node)
            {
                members.Add(node);
                return base.VisitMember(node);
            }

            /// <summary>
            /// Gets the member expressions that can be found in the provided expression.
            /// </summary>
            /// <param name="expression">The expression.</param>
            /// <returns></returns>
            public IEnumerable<MemberExpression> GetMembers(Expression expression)
            {
                members = new HashSet<MemberExpression>();
                this.Visit(expression);

                return members;
            }
        }
        #endregion

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <param name="memberExpression">The member expression.</param>
        /// <returns></returns>
        public static string GetMemberName(Expression memberExpression)
        {
            var body = ((LambdaExpression)memberExpression).Body;

            if (body is MemberExpression)
            {
                return ((MemberExpression)body).Member.Name;
            }

            return null;
        }

        /// <summary>
        /// Replaces the all the parameter instances in the expression with the specified one.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameterInstance">The new name.</param>
        /// <returns>The modified expression.</returns>
        public static Expression ReplaceParameters(Expression expression, ParameterExpression parameterInstance)
        {
            var replacer = new ParameterReplacer();
            return replacer.ReplaceAllParameters(expression, parameterInstance);
        }

        /// <summary>
        /// Replaces the all the parameter instances in the expression with the specified one.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parameterInstance">The new name.</param>
        /// <param name="replacer">The replacer.</param>
        /// <returns>
        /// The modified expression.
        /// </returns>
        public static Expression ReplaceParameter(Expression expression, ParameterExpression parameterInstance, Expression replacer)
        {
            var parameterReplacer = new ParameterReplacer();
            return parameterReplacer.ReplaceParameter(expression, parameterInstance, replacer);
        }

        public static IEnumerable<MemberExpression> ExtractMemberExpressions(Expression sourceExpression)
        {
            var memberVisitor = new MembersVisitor();
            return memberVisitor.GetMembers(sourceExpression);
        }
    }
}
